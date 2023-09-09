import tm1637
from machine import Pin
import utime 
import time
import commonhelper
import _thread
import ntptime
import wifihelper
import ntptimeh
 

clkPin=13
dioPin=14
 
def showtime_func(a,b):
    tm = tm1637.TM1637(clk=Pin(clkPin), dio=Pin(dioPin)) 
    tm.brightness(0) 
    showColon=True
    while True:
        current_time=utime.localtime()
        hour=commonhelper.xfill(current_time[3],2)
        minute=commonhelper.xfill(current_time[4],2) 
        timestr=hour+minute
        tm.show(timestr, showColon)
#        print(timestr+':'+str(showColon))
        showColon=not showColon 
        time.sleep(1) 
        
        
def updatetimeTimer_func():
    while True:
        current_time=utime.localtime() 
        if(current_time[3]==1 and current_time[4]==0):
            updatetime_func()
            time.sleep(90)
        else:
            time.sleep(60)
            
def connectwifi_func():
    ssid=commonhelper.readConfig('ssid');
    pwd=commonhelper.readConfig('password');
    connectR=False
    for index in range(100):
        connectR=wifihelper.connectwifi(ssid, pwd)
        if(connectR):
            break
    print("wifi conncet stutus:"+str(connectR))
    return connectR
    
def updatetime_func(): 
    ntptimeh.settime() 
        #      print('has sync time')
        
def showinfo(info):
    tm = tm1637.TM1637(clk=Pin(clkPin), dio=Pin(dioPin))
    tm.brightness(0) 
    tm.show(info)

 