# This file is executed on every boot (including wake-boot from deepsleep)
#import esp
#esp.osdebug(None)
#import webrepl
#webrepl.start() 
import main 
import commonhelper   
 

print("1") 
if(main.connectwifi_func()):
    main.connectmqtt()
 

    
#     if(commonhelper.needRun()): 
#    _thread.start_new_thread(main.connectmqtt, ())   
  