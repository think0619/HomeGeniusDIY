from machine import Pin, SPI
import utime 
import time
import commonhelper 
import wifihelper
import machine 
from umqttsimple import MQTTClient

OPSRelay=15    

            
def connectwifi_func():
    ssid=commonhelper.readConfig('ssid');
    pwd=commonhelper.readConfig('password'); 
    connectR =wifihelper.connectwifi(ssid, pwd)
    print("WiFi conncet stutus:"+str(connectR))
    return connectR
 
            
TOPIC = b"OtherEquip"

# poweron  poweroff  
def mqttsub_cb(topic, msg):
    global OPSRelay
    global DiskRelay 
    if(topic==TOPIC):
        if(msg.decode('utf-8')=='officedooropen'):
              commonhelper.updatePin(OPSRelay,1)
              time.sleep(1)
              commonhelper.updatePin(OPSRelay,0)
              print("hi")
        elif(msg.decode('utf-8')=='officeopendoornon'):
            commonhelper.updatePin(OPSRelay,1)
        elif(msg.decode('utf-8')=='officeopendoornoff'):
            commonhelper.updatePin(OPSRelay,0)
        elif(msg.decode('utf-8')=='opendoorother'):
            pass

def autoconnectmqtt(mqttclient,clearsession):
    while True:
        try:
            mqttclient.set_callback(mqttsub_cb) 
            mqttclient.connect()
            mqttclient.subscribe(TOPIC)
            break
        except OSError as e:
            print(e)
            time.sleep(30)
        
last_ping = time.time()
ping_interval = 60
def connectmqtt():
    MQTT_BROKER = commonhelper.readConfig('mqttbroker')
#    global TOPIC = b"ShowClockTime"
    User=commonhelper.readConfig('mqttuser')
    Password=commonhelper.readConfig('mqttpwd')
    mqttid=commonhelper.readConfig('mqttid')
    mqttClient = MQTTClient(mqttid, MQTT_BROKER,port=1883,ssl=False,user=User,password =Password,   keepalive=60)
    autoconnectmqtt(mqttClient,True)
 
    pin = machine.Pin(15, machine.Pin.IN)
    while True:
        try:
            if False: 
                mqttClient.wait_msg()
                 # Blocking wait for message
            else:
                 # Non-blocking wait for message
                 pin_value = pin.value()
                 if pin_value == 1:
                     mqttClient.publish(TOPIC, 'officedooropen')
                     print('mqtt sent')
                 else:
                     pass 
        except OSError as e: 
            autoconnectmqtt(mqttClient,False)
        time.sleep(1)
 