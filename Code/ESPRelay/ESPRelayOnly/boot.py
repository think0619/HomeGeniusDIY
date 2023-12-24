# This file is executed on every boot (including wake-boot from deepsleep)
#import esp
#esp.osdebug(None)
#import webrepl
#webrepl.start()
import _thread
import main 
import commonhelper   
 

 
if(main.connectwifi_func()): 
     if(commonhelper.needRun()): 
         _thread.start_new_thread(main.connectmqtt, ())   
  