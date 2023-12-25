import random
import time 
import cfghelper
from paho.mqtt import client as mqtt_client 
from apscheduler.schedulers.background import BackgroundScheduler  
from datetime import datetime, timedelta 
import json
from apscheduler.schedulers.background import BackgroundScheduler   
import subprocess
 
broker = cfghelper.readConfig("mqttbroker")
port = 1883
statusTopic="RaspStatus"
Receive_TOPIC = [("BLAudio",0),("Aircondition",0) ]
Send_Topic=""
client_id = f'raspberry-{random.randint(0, 1000)}'
username = cfghelper.readConfig("mqttuser")
password =  cfghelper.readConfig("mqttpwd")  
stopPlayerScheduler = BackgroundScheduler() 
mqttclient=None

#ir name
POLKIRName="polk"
AirconditionIRName="aircon"

def publish(client):
    msg_count = 1 
    topic = "RaspController" 
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
            client.subscribe(Receive_TOPIC) 
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

def subscribe(client: mqtt_client): 
    def on_message(client, userdata, msg): 
        msgtopic=msg.topic
        cmdmsg=msg.payload.decode() 
        #BLAudio",0),("Aircondition
        if(msgtopic=="BLAudio"):
            if(cmdmsg=="power"):
                IRSend(POLKIRName,"power")
            elif(cmdmsg=="mute"):
                IRSend(POLKIRName,"mute")
            elif(cmdmsg=="fiberoptic"):
                IRSend(POLKIRName,"fiberoptic")
            elif(cmdmsg=="aux"):
                IRSend(POLKIRName,"aux")
            elif(cmdmsg=="bluetooth"):
                IRSend(POLKIRName,"bluetooth")
            elif(cmdmsg=="bassplus"):
                IRSend(POLKIRName,"bassplus")
            elif(cmdmsg=="bassminus"):
                IRSend(POLKIRName,"bassminus")
            elif(cmdmsg=="volplus"):
                IRSend(POLKIRName,"volplus")
            elif(cmdmsg=="volminus"):
                IRSend(POLKIRName,"volminus")
            elif(cmdmsg=="modemovie"):
                IRSend(POLKIRName,"modemovie")
            elif(cmdmsg=="modenight"):
                IRSend(POLKIRName,"modenight")
            elif(cmdmsg=="modemusic"):
                IRSend(POLKIRName,"modemusic") 
        elif(msgtopic=="Aircondition"): 
            if(cmdmsg=="turnon"):
                IRSend(AirconditionIRName,"turnon")
            elif(cmdmsg=="turnoff"):
                IRSend(AirconditionIRName,"turnoff")
            elif(cmdmsg=="mode"):
                IRSend(AirconditionIRName,"mode")
            elif(cmdmsg=="tempplus"):
                IRSend(AirconditionIRName,"tempplus")
            elif(cmdmsg=="tempminus"):
                IRSend(AirconditionIRName,"tempminus")
              
       
    client.subscribe(Receive_TOPIC)
    client.on_message = on_message  

def run() : 
    client = connect_mqtt() 
    subscribe(client)  
    client.loop_forever() 

def aircondition(ops):
    #ops on /off
    print(f"irsend SEND_ONCE aircon {ops}")
    result1 = subprocess.run(f"irsend SEND_ONCE aircon  {ops} ", shell=True, capture_output=False, text=False)

def IRSend(name,ops):
    shellcmd=f"irsend SEND_ONCE {name} {ops}"
    print(shellcmd)
    result1 = subprocess.run(shellcmd, shell=True, capture_output=False, text=False)


    
 
 
  