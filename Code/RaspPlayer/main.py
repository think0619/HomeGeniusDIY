# import os
# os.environ['PYTHON_VLC_MODULE_PATH'] = "./vlc"

import vlc
from datetime import datetime
from vlcplayer import Player 
import mqtthelper
from datetime import datetime
from apscheduler.schedulers.blocking import BlockingScheduler 
from apscheduler.schedulers.background import BackgroundScheduler  
from threading import Thread 
import relayhandler

def playPlayer():
    relayhandler.controlrelay(5,1)
    player.play() 
        
def stopPlayer():
    relayhandler.controlrelay(5,0)
    player.stop()




def my_call_back(event):
    pass
    # print("call:", player.get_time()) 

if "__main__" == __name__:
    relayhandler.controlrelay(5,0)
    player = Player()
    player.add_callback(vlc.EventType.MediaPlayerTimeChanged, my_call_back)
    player.set_uri("http://satellitepull.cnr.cn/live/wx32jsyygb/playlist.m3u8") 
    player.set_uri("http://ngcdn001.cnr.cn/live/zgzs/index.m3u8") 
    player.set_uri("http://play-radio-stream3.hndt.com/now/JxkyN5mR/playlist.m3u8")  
    
    thread=Thread(target=mqtthelper.run,args=(player,)) 
    thread.start()
    # mqtthelper.run(player) 
    #player run
    clockPlayScheduler = BackgroundScheduler()
    # trigger_cron = '0 30 10 * * *'  # 设置为每天的10:30:00执行任务
    clockPlayScheduler.add_job(playPlayer, 'cron',  hour=6,minute='40,54,58'  ) 
    clockPlayScheduler.add_job(playPlayer, 'cron',  hour=7,minute='30,35,45'  ) 
    clockPlayScheduler.add_job(stopPlayer, 'cron',  hour=6,minute='42,56,59') 
    clockPlayScheduler.add_job(stopPlayer, 'cron',  hour=7,minute='32,37,47'  )  
    clockPlayScheduler.start()  
    while True:
        pass