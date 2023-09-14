from machine import Pin, SPI
import utime 
import time
import commonhelper
import _thread
import ntptime
import wifihelper
import ntphelper
import max7219 

clkPin=13
dioPin=14 
        

            
def connectwifi_func():
    ssid=commonhelper.readConfig('ssid');
    pwd=commonhelper.readConfig('password');
    connectR=True
    connectR=wifihelper.connectwifi(ssid, pwd)
    print("WiFi conncet stutus:"+str(connectR))
    return connectR

#     5v  vcc
#     GND GND
#     D2 MOSI DIN
#     D5 CS CS
#     D4 SCK CLK   
def showTimeOnMax7219():
    spi  = SPI(1, baudrate=10000000, polarity=1, phase=0, sck=Pin(4), mosi=Pin(2)) 
    ss = Pin(5, Pin.OUT)
    msg="Good"
    length = len(msg)
    length = (length*8)
    display = max7219.Matrix8x8(spi, ss, 4)
    display.brightness(1)   # adjust brightness 1 to 15
    display.fill(0)
    display.show()
    time.sleep(3)
    
    index=0
    while True:
        current_time=utime.localtime()
        hour=commonhelper.xfill(current_time[3],2)
        minute=commonhelper.xfill(current_time[4],2)  
        display.fill(0)
        display.text(hour+minute,0,0,1)
        if(index==0): 
#             display.rect (0, 7, 1, 1,1) 
#             display.rect (7, 7, 1, 1,1) 
#             display.rect (15, 7, 1, 1,1)  
#             display.rect (23, 7, 1, 1,1) 
            display.rect (31, 7, 1, 1,1)    
        display.show() 
        index = (index+1) % 2 
        time.sleep(1)  

def showMsgOnMax7219(msg):
    spi  = SPI(1, baudrate=10000000, polarity=1, phase=0, sck=Pin(4), mosi=Pin(2)) 
    ss = Pin(5, Pin.OUT) 
    length = len(msg)
    length = (length*8)
    display = max7219.Matrix8x8(spi, ss, 4)
    display.brightness(1)   # adjust brightness 1 to 15
    display.fill(0)
    display.show()
       
def updatetime_func(): 
    try:
        ntphelper.settime()
        print("sync time success")
    except:
        showMsgOnMax7219('sync time error')

def updatetimeTimer_func():
    updatetime_func()
    while True:
        current_time=utime.localtime() 
        if(current_time[3]==2 and current_time[4]==0):
            updatetime_func()
            time.sleep(90)
        else:
            time.sleep(60)

#flow effect
#     while True:
#         for x in range(32, -length, -1):
#             display.text(msg ,x,0,1)
#             display.show()
#             time.sleep(0.10)
#             display.fill(0)
     




 