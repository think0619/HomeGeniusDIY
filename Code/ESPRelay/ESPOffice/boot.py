# This file is executed on every boot (including wake-boot from deepsleep)
#import esp
#esp.osdebug(None)
#import webrepl
#webrepl.start() 
import main 
import commonhelper
import _thread
from machine import UART
from machine import Pin
import time
import utime 
import _thread
import mqtthelper

def notifyFunc(): 
    try:
        uart = UART(2, baudrate=9600, rx=16, tx=17)
        stopdata=[0x7E,0xFF,0x06,0x17,0x00,0x00,0x02,0xEF]
        morningdata=[0x7E, 0xFF, 0x06, 0x03, 0x00, 0x00, 0x01, 0xEF]
        noondata=[0x7E, 0xFF, 0x06, 0x03, 0x00, 0x00, 0x02, 0xEF]
        afternoondata=[0x7E, 0xFF, 0x06, 0x03, 0x00, 0x00, 0x03, 0xEF]
        midvolumndata=[0x7E, 0xFF, 0x06, 0x06, 0x00, 0x00, 0x14, 0xEF]
        
        time.sleep(3)
        uart.write(bytes(midvolumndata))
        key = Pin(12, Pin.IN)
        while True:
            print("GPIO_12:", key.value())
            if key.value()==1:
                print(time.localtime())
                current_time=utime.localtime()
                
                if(current_time[3]<8 or (current_time[3]==8 and current_time[4]< 40)):
#                     print("1")
                    uart.write(bytes(morningdata))
                elif((current_time[3]==11 and current_time[4]>= 30) or (current_time[3]==12 and current_time[4]< 30)):
#                     print("2")
                    uart.write(bytes(noondata))
                elif((current_time[3]==17 and current_time[4]>= 1) or (current_time[3]>=18)):
#                     print("4")
                    uart.write(bytes(afternoondata))
                else:
                    print('invalid')
                time.sleep(5)
            else:
                pass
                #uart.write(bytes(stopdata))
            time.sleep(0.5) 
    except:
        print('notifyFunc err')
        pass

if(main.connectwifi_func()):  
     #_thread.start_new_thread(main.updatetime_func, ()) 
     main.updatetime_func()
     _thread.start_new_thread(main.connectmqtt, ())
     #_thread.start_new_thread(notifyFunc, ())
     
 

    
#     if(commonhelper.needRun()): 
#    _thread.start_new_thread(main.connectmqtt, ())



 





