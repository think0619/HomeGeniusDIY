import datetime
import RPi.GPIO as GPIO 
import time 
from relayhandler import controlrelay 
GPIOPin= 7 
controlrelay(GPIOPin,0)
controlrelay(GPIOPin,1)
time.sleep(0.4)
controlrelay(GPIOPin,0)
#while True:
#    now = datetime.datetime.now()  
#    day=int(time.strftime("%w"))   
#    if (day==1 or day==3  or day ==5  ): 
#        if now.hour ==10  and now.minute == 24 and now.second < 8 :  
#     controlrelay(GPIOPin,1)
#        else: 
#          controlrelay(GPIOPin,0)
    #time.sleep(0.2)







