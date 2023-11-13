# This file is executed on every boot (including wake-boot from deepsleep)
#import esp
#esp.osdebug(None)
#import webrepl
#webrepl.start()
import _thread
import main 
import commonhelper
import utime
import urequests 

 
if(main.connectwifi_func()): 
       if(commonhelper.needRun()):
#            _thread.start_new_thread(main.updatetimeTimer_func, ())
           _thread.start_new_thread(main.connectmqtt, ())
           main.updatetime_func()
#            response2 = urequests.get("https://hw.hellolinux.cn:8888/api/configservice/timenow?zonehour=0")      
#            tm=response2.text.split(',')  
#            import machine
# #     2023,11,10,12,45,23,26,5
#            machine.RTC().datetime((int(tm[0]), int(tm[1]), int(tm[2]), int(tm[6]), int(tm[3]), int(tm[4]), int(tm[5]), 0))           
#          import machine   
#          machine.RTC().datetime((2023, 12, 3, 1, 13, 13, 13, 0))
           _thread.start_new_thread(main.show1637, ())
           _thread.start_new_thread(main.showTimeOnMax7219, ())
       else:
          main.showMsgOnMax7219("Task deadline") 
else:
    main.showMsgOnMax7219("WiFI error")
    
    

          
 


