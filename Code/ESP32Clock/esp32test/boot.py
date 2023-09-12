# This file is executed on every boot (including wake-boot from deepsleep)
#import esp
#esp.osdebug(None)
#import webrepl
#webrepl.start()
import _thread
import main
import time,utime 
import commonhelper  
from machine import Pin, SPI
import mqtthelper
import max7219 
import sys, os
 
def closeRelayAndReset():
    while True:
        if(commonhelper.needCloseRelay()):
            p1 = Pin(13, Pin.OUT)    # create output pin on GPIO4 
            p1.value(1)             # set pin to low level
            time.sleep(0.5)
            p1.value(0)
            commonhelper.resetRelayApiConfig()
        else:
            time.sleep(5*60)
def printtest1():
    while True:
        print("printtest111") 
 
#     5v  vcc
#     GND GND
#     D2 MOSI DIN
#     D5 CS CS
#     D4 SCK CLK      
def max72191():

    spi  = SPI(1, baudrate=10000000, polarity=1, phase=0, sck=Pin(4), mosi=Pin(2)) 
    ss = Pin(5, Pin.OUT)
    display = max7219.Matrix8x8(spi , ss, 4)
    display.brightness(1)
    display.text("12345678",0,0,1)
    display.show()
    display.fill(0)
    

def xxx():
    spi  = SPI(1, baudrate=10000000, polarity=1, phase=0, sck=Pin(4), mosi=Pin(2)) 
    ss = Pin(5, Pin.OUT)
    msg=""
    length = len(msg)
    length = (length*8)
    display = max7219.Matrix8x8(spi, ss, 4)
    display.brightness(1)   # adjust brightness 1 to 15
    display.fill(0)
    display.show()
    
    index=0
    while True:
        current_time=utime.localtime()
        hour=commonhelper.xfill(current_time[3],2)
        minute=commonhelper.xfill(current_time[4],2) 
        timestr=hour+minute
        display.fill(0)
        display.text(timestr ,0,0,1)
        if(index==0): 
#             display.rect (0, 7, 1, 1,1) 
#             display.rect (7, 7, 1, 1,1) 
#             display.rect (15, 7, 1, 1,1)  
#             display.rect (23, 7, 1, 1,1) 
            display.rect (31, 7, 1, 1,1)    
        display.show() 
        index = (index+1) % 2 #余数是1 
        time.sleep(1)  
    
#     while True:
#         for x in range(32, -length, -1):
#             display.text(msg ,x,0,1)
#             display.show()
#             time.sleep(0.10)
#             display.fill(0)
     


if(main.connectwifi_func()):
     if(commonhelper.needRun()):
#
 
          main.updatetime_func()
          xxx()
         #_thread.start_new_thread(mqtthelper.main, ())  
          
 
# if(main.connectwifi_func()):
#     if(commonhelper.needRun()): 
#         _thread.start_new_thread(mqtthelper.main(), ()) 
#         _thread.start_new_thread(printtest(), ()) 
#     else:
#         main.showinfo('stop')
# else:
#     main.showinfo('wifi')

