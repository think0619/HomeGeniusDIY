# This file is executed on every boot (including wake-boot from deepsleep)
#import esp
#esp.osdebug(None)
#import webrepl
#webrepl.start()
import _thread
import main
import time,utime 
import commonhelper   
 
if(main.connectwifi_func()):
     if(commonhelper.needRun()):
         _thread.start_new_thread(main.updatetimeTimer_func, ())
         _thread.start_new_thread(main.showTimeOnMax7219, ())
     else:
         main.showMsgOnMax7219("Task deadline") 
else:
    main.showMsgOnMax7219("WiFI error") 
          
 
# if(main.connectwifi_func()):
#     if(commonhelper.needRun()): 
#         _thread.start_new_thread(mqtthelper.main(), ()) 
#         _thread.start_new_thread(printtest(), ()) 
#     else:
#         main.showinfo('stop')
# else:
#     main.showinfo('wifi')

