import os

from datetime import datetime
import mqtthelper
from datetime import datetime
from apscheduler.schedulers.blocking import BlockingScheduler 
from apscheduler.schedulers.background import BackgroundScheduler  
from threading import Thread 
import urllib.request 
import subprocess

pinnum=5

def checkNetwork():
    try:
        urllib.request.urlopen("https://www.baidu.com").getcode() 
    except IOError:  
        result1 = subprocess.run("sudo ip link set wlan0 down", shell=True, capture_output=False, text=False)
        result2 = subprocess.run("sudo ip link set wlan0 up", shell=True, capture_output=False, text=False) 
    else: 
        pass

def aircondition():
    #ops on /off 
    result1 = subprocess.run(f"irsend SEND_ONCE aircon on", shell=True, capture_output=False, text=False)
    
     

def my_call_back(event):
    pass
    # print("call:", player.get_time()) 

if "__main__" == __name__:  
    synctimecmd="sudo date -s \"$(wget -qSO- --max-redirect=0 baidu.com 2>&1 | grep Date: | cut -d' ' -f5-8)Z\""
    syncresult1 = subprocess.run(synctimecmd, shell=True, capture_output=False, text=False)
     
    thread=Thread(target=mqtthelper.run,args=( )) 
    thread.start()
    
    clockPlayScheduler = BackgroundScheduler()
    # trigger_cron = '0 30 10 * * *'  # 设置为每天的10:30:00执行任务 
    clockPlayScheduler.add_job(aircondition, 'cron',day_of_week ="0-5", hour=7,minute='0'  )   
    clockPlayScheduler.add_job(checkNetwork, 'interval', minutes =15) 
    clockPlayScheduler.start()  

    while True:
        pass