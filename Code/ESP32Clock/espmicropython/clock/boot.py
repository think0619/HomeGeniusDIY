# This file is executed on every boot (including wake-boot from deepsleep)
#import esp
#esp.osdebug(None)
#import webrepl
#webrepl.start()
import _thread
import main
import time
import commonhelper

#main.updatetime_func()
#main.showtime_func(0,0)
#updatetimeTimer_func()
if(main.connectwifi_func()):
    if(commonhelper.needRun()):
        main.updatetime_func()
        _thread.start_new_thread(main.showtime_func, (1,1))
        _thread.start_new_thread(main.updatetimeTimer_func, ())
    else:
        main.showinfo('stop')
else:
    main.showinfo('wifi')
    
