# This file is executed on every boot (including wake-boot from deepsleep)
#import esp
#esp.osdebug(None)
#import webrepl
#webrepl.start()
import _thread
import main
import time
import commonhelper  
from machine import Pin
import mqtthelper

 
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
 
 
if(main.connectwifi_func()):
    if(commonhelper.needRun()): 
        _thread.start_new_thread(mqtthelper.main(), ()) 
    else:
        main.showinfo('stop')
else:
    main.showinfo('wifi')
