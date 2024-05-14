from machine import Pin, SPI
import utime 
import time
import commonhelper 
import wifihelper  
from umqttsimple import MQTTClient

OPSRelay=0    

            
def connectwifi_func():
    ssid=commonhelper.readConfig('ssid');
    pwd=commonhelper.readConfig('password'); 
    connectR =wifihelper.connectwifi(ssid, pwd)
    print("WiFi conncet stutus:"+str(connectR))
    return connectR

def settimefromserver():
    datetimeresult=commonhelper.getdatetimefromserver()
    tm=datetimeresult.split(',')  
    import machine
#     2023,11,10,12,45,23,26,5
    machine.RTC().datetime((int(tm[0]), int(tm[1]), int(tm[2]), int(tm[6]), int(tm[3]), int(tm[4]), int(tm[5]), 0))
    
            
def updatetime_func(): 
    try:
#        getNZtime.getNZtime()
#        micropythonntp.SyncNTPTime()
#        print(micropythonntp.Gettime())
         settimefromserver()
         print("1")
#        print("sync time success")
    except:
        pass
        #showMsgOnMax7219('sync time error')   
 
   
 