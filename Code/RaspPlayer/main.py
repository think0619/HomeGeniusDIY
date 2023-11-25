# import os
# os.environ['PYTHON_VLC_MODULE_PATH'] = "./vlc"

import vlc
from datetime import datetime
from vlcplayer import Player 
import mqtthelper

def my_call_back(event):
    print("call:", player.get_time())


if "__main__" == __name__:
    player = Player()
    player.add_callback(vlc.EventType.MediaPlayerTimeChanged, my_call_back)
    # 在线播放流媒体视频
    player.play("http://satellitepull.cnr.cn/live/wx32jsyygb/playlist.m3u8")
    mqtthelper.run(player)

    # 播放本地mp3
    # player.play("D:/abc.mp3")

    # 防止当前进程退出
    while True:
        nowtime=datetime.now()
        if  nowtime.minute%2==0:
            player.stop()
            
        pass