import random
import time 
import cfghelper
from paho.mqtt import client as mqtt_client
from vlcplayer import Player as VlcPlayer 
from apscheduler.schedulers.background import BackgroundScheduler  
from datetime import datetime, timedelta
import relayhandler 
import json
from apscheduler.schedulers.background import BackgroundScheduler   
import subprocess
 
broker = cfghelper.readConfig("mqttbroker")
port = 1883
topic = "RaspController" 
statusTopic="RaspStatus"
client_id = f'raspberry-{random.randint(0, 1000)}'
username = cfghelper.readConfig("mqttuser")
password =  cfghelper.readConfig("mqttpwd")  
stopPlayerScheduler = BackgroundScheduler() 
mqttclient=None

def publish(client):
    msg_count = 1
    while True:
        time.sleep(1)
        msg = f"messages: {msg_count}"
        result = client.publish(topic, msg)
        # result: [0, 1]
        status = result[0]
        if status == 0:
            print(f"Send `{msg}` to topic `{topic}`")
        else:
            print(f"Failed to send message to topic {topic}")
        msg_count += 1
        if msg_count > 5:
            break

def sendVlcStatus(client: mqtt_client,player:VlcPlayer): 
    # 创建包含变量的字典
    data ={
        "Status":1,
        "Data":{"state": player.get_state(),
                "streamurl": player.get_stream_uri(),
                "volume": player.get_volume()
                } 
           }
    json_str = json.dumps(data)   
    msg = f"{json_str}"
    result = client.publish(statusTopic, msg) 

def connect_mqtt() -> mqtt_client:
    def on_connect(client, userdata, flags, rc):
        if rc == 0:
            client.subscribe(topic,1)
            client.subscribe("Aircondition",2)
            print("Connected to MQTT Broker!")
        else:
            print("Failed to connect, return code %d\n", rc) 
    # def on_disconnect(client, userdata,  rc):
    #     pass
        # client.loop_stop(force=True)
    client = mqtt_client.Client(client_id)
    client.username_pw_set(username, password)
    client.on_connect = on_connect
    # client.on_disconnect = on_disconnect
    client.connect(broker, port)
    return client 

def subscribe(client: mqtt_client,player:VlcPlayer,pinnum):
    def stopPlay():
        relayhandler.controlrelay(pinnum,0)
        player.stop()  

    def on_message(client, userdata, msg): 
        cmdmsg=msg.payload.decode()
        if(cmdmsg=="play"):
            # player.set_volume(50)
            relayhandler.controlrelay(pinnum,1)
            player.play()
        elif(cmdmsg=="stop"):
            relayhandler.controlrelay(pinnum,0)
            player.stop() 
        elif(cmdmsg=="pause"):
            player.pause() 
        elif(cmdmsg=="getstatus"):
            sendVlcStatus(client,player) 
        elif(cmdmsg=="turnon"):
            aircondition('on')
        elif(cmdmsg=="turnoff"):
            aircondition('off')
        else:
            if(cmdmsg.find('changesrc')==0):
                #`changesrc|${that.vlcSrcResultValue}`
               src=cmdmsg.split("|")[1]
               if src!=None:
                   relayhandler.controlrelay(5,1)
                   player.set_uri(src) 
                   player.play()  
            elif(cmdmsg.find('volume')==0):
                #`volume|up` `volume|down`
               action=cmdmsg.split("|")[1]
               if action!=None: 
                   if(action=="up"):
                       player.set_volume(player.get_volume()+5)  
                   elif(action=="down"):
                       player.set_volume(player.get_volume()-5)   
                   elif(action=="half"):
                       player.set_volume(50)   
                   else:
                       if action.isdigit():
                           newVolume=eval(action)
                           if(newVolume<=100 and newVolume>=0):
                               player.set_volume(newVolume)   

            elif(cmdmsg.find('schedule')==0):
                scheduleTime=cmdmsg.split("|")[1]
                if scheduleTime!=None:
                    scheduleTime=eval(scheduleTime) 
                    jobs=stopPlayerScheduler.get_jobs()
                    for job in jobs:
                        if(job.id=="stopsheduleid"):
                            stopPlayerScheduler.remove_job('stopsheduleid') 
                    # if(stopPlayerScheduler.running):
                    #     stopPlayerScheduler.shutdown()
                    executetime = datetime.now()+timedelta(minutes=scheduleTime) 
                    stopPlayerScheduler.add_job(stopPlay, 'date', run_date=executetime,id='stopsheduleid') 
                    if stopPlayerScheduler.running!=True :
                      stopPlayerScheduler.start()
       
       # sendVlcStatus(client,player)
    client.subscribe(topic)
    client.on_message = on_message  

def run(vlcplayer:VlcPlayer,pinnum) : 
    client = connect_mqtt() 
    subscribe(client,vlcplayer,pinnum) 
    # stopPlayerScheduler = BackgroundScheduler() 
    # msgScheduler = BackgroundScheduler()    
    # msgScheduler.add_job(lambda:sendVlcStatus(client,vlcplayer), 'interval', seconds =30) 
    # msgScheduler.start()  
    client.loop_forever() 

def aircondition(ops):
    #ops on /off
    print(f"irsend SEND_ONCE aircon {ops}")
    result1 = subprocess.run(f"irsend SEND_ONCE aircon  {ops} ", shell=True, capture_output=False, text=False)
    
    
 
 
  