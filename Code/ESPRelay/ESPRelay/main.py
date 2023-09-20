from machine import Pin, SPI
import time
import commonhelper
import _thread 
import wifihelper 
from umqtt.simple import MQTTClient 

OPSRelay=13
DiskRelay=14  
            
def connectwifi_func():
    ssid=commonhelper.readConfig('ssid');
    pwd=commonhelper.readConfig('password');
    
    connectR =wifihelper.connectwifi(ssid, pwd)
    print("WiFi conncet stutus:"+str(connectR))
    return connectR
  
  
TOPIC = b"OPSRelayController"

# poweron  poweroff diskpower
def mqttsub_cb(topic, msg):
    global OPSRelay
    global DiskRelay 
    if(topic==TOPIC):
        if(msg.decode('utf-8')=='poweron'):
              commonhelper.updatePin(OPSRelay,1)
        elif(msg.decode('utf-8')=='poweroff'): 
              commonhelper.updatePin(OPSRelay,0) 
        elif(msg.decode('utf-8')=='diskpower'):
              commonhelper.updatePin(DiskRelay,1)
              time.sleep_ms(200)
              commonhelper.updatePin(DiskRelay,0)  
        
        
last_ping = time.time()
ping_interval = 180
def connectmqtt():
    MQTT_BROKER = commonhelper.readConfig('mqttbroker');
#    global TOPIC = b"ShowClockTime"
    User=commonhelper.readConfig('mqttuser');
    Password=commonhelper.readConfig('mqttpwd');
    mqttClient = MQTTClient("esp32_relay", MQTT_BROKER,port=1883,ssl=False,user=User,password =Password,   keepalive=180)
    mqttClient.set_callback(mqttsub_cb)
    mqttClient.connect()
    mqttClient.subscribe(TOPIC)
    print(f"Connected to MQTT  Broker :: {MQTT_BROKER}, and waiting for callback function to be called!")
    while True:
        if False:
            # Blocking wait for message
            mqttClient.wait_msg()
        else:
            # Non-blocking wait for message
            mqttClient.check_msg()
            # Then need to sleep to avoid 100% CPU usage (in a real
            # app other useful actions would be performed instead)
            global last_ping
            if (time.time() - last_ping) >= ping_interval:
                mqttClient.ping()
                last_ping = time.time()
                now = time.localtime()
                print(f"Pinging MQTT Broker, last ping :: {now[0]}/{now[1]}/{now[2]} {now[3]}:{now[4]}:{now[5]}")
            time.sleep(1) 