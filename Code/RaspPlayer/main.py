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

def my_call_back(event):
    pass
    # print("call:", player.get_time()) 

if "__main__" == __name__:
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
    clockPlayScheduler.add_job(player.play, 'cron',  hour=16,minute=40, second=59, ) 
    clockPlayScheduler.add_job(player.stop, 'cron', hour=16,minute=38, second=35,) 
    clockPlayScheduler.start()  
    while True:
        pass