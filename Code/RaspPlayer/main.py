import os
os.environ['PYTHON_VLC_MODULE_PATH'] = "./vlc"

import vlc
from datetime import datetime
from vlcplayer import Player 
import mqtthelper
from datetime import datetime
from apscheduler.schedulers.blocking import BlockingScheduler 
from apscheduler.schedulers.background import BackgroundScheduler  
from threading import Thread 
import urllib.request
import subprocess
# import relayhandler

def playPlayer():
    # relayhandler.controlrelay(5,1)
    player.play() 
        
def stopPlayer():
    # relayhandler.controlrelay(5,0)
    player.stop()

def checkNetwork():
    try:
        urllib.request.urlopen("https://www.baidu.com").getcode() 
    except IOError:  
        result1 = subprocess.run("sudo ip link set wlan0 down", shell=True, capture_output=False, text=False)
        result2 = subprocess.run("sudo ip link set wlan0 up", shell=True, capture_output=False, text=False) 
    else: 
        pass


def my_call_back(event):
    pass
    # print("call:", player.get_time()) 

if "__main__" == __name__: 
    # relayhandler.controlrelay(5,0)
    player = Player( "--no-video" )
    player.add_callback(vlc.EventType.MediaPlayerTimeChanged, my_call_back)  
    thread=Thread(target=mqtthelper.run,args=(player,)) 
    thread.start()
    # mqtthelper.run(player) 
    #player run
    clockPlayScheduler = BackgroundScheduler()
    # trigger_cron = '0 30 10 * * *'  # 设置为每天的10:30:00执行任务
    clockPlayScheduler.add_job(playPlayer, 'cron',day_of_week ="0-5", hour=7,minute='10,20,40,50'  ) 
    clockPlayScheduler.add_job(playPlayer, 'cron',day_of_week ="0-5", hour=8,minute='0'  )  
    clockPlayScheduler.add_job(stopPlayer, 'cron',day_of_week ="0-5", hour=8,minute=20 )   
    clockPlayScheduler.add_job(checkNetwork, 'interval', minutes =15) 
    clockPlayScheduler.start()  

    while True:
        pass