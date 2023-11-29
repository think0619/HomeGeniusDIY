import random
import time 
import cfghelper
from paho.mqtt import client as mqtt_client
from vlcplayer import Player as VlcPlayer 
from apscheduler.schedulers.background import BackgroundScheduler  
from datetime import datetime, timedelta
import relayhandler
 
broker = cfghelper.readConfig("mqttbroker")
port = 1883
topic = "RaspController" 
client_id = f'publish-{random.randint(0, 1000)}'
username = cfghelper.readConfig("mqttuser")
password =  cfghelper.readConfig("mqttpwd") 
stopPlayerScheduler = BackgroundScheduler() 

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


def connect_mqtt() -> mqtt_client:
    def on_connect(client, userdata, flags, rc):
        if rc == 0:
            print("Connected to MQTT Broker!")
        else:
            print("Failed to connect, return code %d\n", rc) 
    client = mqtt_client.Client(client_id)
    client.username_pw_set(username, password)
    client.on_connect = on_connect
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
       
    client.subscribe(topic)
    client.on_message = on_message  

def run(vlcplayer:VlcPlayer,pinnum):
    stopPlayerScheduler = BackgroundScheduler() 
    
    client = connect_mqtt()
    subscribe(client,vlcplayer,pinnum)
    client.loop_forever() 
 