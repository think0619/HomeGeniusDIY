import random
import time 
import cfghelper
from paho.mqtt import client as mqtt_client
from vlcplayer import Player as VlcPlayer
 
broker = cfghelper.readConfig("mqttbroker")
port = 1883
topic = "OPSRelayController" 
client_id = f'publish-{random.randint(0, 1000)}'
username = cfghelper.readConfig("mqttuser")
password =  cfghelper.readConfig("mqttpwd") 

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

def subscribe(client: mqtt_client,player:VlcPlayer):
    def on_message(client, userdata, msg):
        # msg.topic msg.payload.decode()
        cmdmsg=msg.payload.decode()
        if(cmdmsg=="play"):
            player.play()
        elif(cmdmsg=="stop"):
            player.stop() 
        if(cmdmsg=="pause"):
            player.pause()
        elif(cmdmsg=="changesrc"):
            pass
            # player.play()  
       
    client.subscribe(topic)
    client.on_message = on_message 


def run(vlcplayer:VlcPlayer):
    client = connect_mqtt()
    subscribe(client,vlcplayer)
    client.loop_forever() 
 