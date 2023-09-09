import _thread
import time
from machine import Pin
def th_func(delay, id):
    while True:
        time.sleep(delay)
        print('Running thread:')


#for i in range(2):
#    _thread.start_new_thread(th_func, (i + 1, i))

def light_func(delay,id):
    p1 = Pin(1, Pin.OUT)    # create output pin on GPIO4
    p1.value(0)             # set pin to high level
    time.sleep(1)           # sleep for 1 second
    p1.value(1)             # set pin to low level
    time.sleep(1)
    p1.value(0)
    time.sleep(1)
    p1.value(1)
 
 

threadinf=_thread.start_new_thread(light_func, (1, 1))
print(threadinf)
