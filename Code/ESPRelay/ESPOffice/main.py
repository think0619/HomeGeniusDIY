from machine import Pin, SPI
import utime 
import time
import commonhelper
import _thread
import ntptime
import wifihelper 
from umqtt.simple import MQTTClient 
import machine
import mqtthelper
  
            
def connectwifi_func():
    ssid=commonhelper.readConfig('ssid');
    pwd=commonhelper.readConfig('password'); 
    connectR =wifihelper.connectwifi(ssid, pwd)
    # print("WiFi conncet stutus:"+str(connectR))
    return connectR
 
def settimefromserver():
    datetimeresult=commonhelper.getdatetimefromserver()
    tm=datetimeresult.split(',')  
    import machine
#     2023,11,10,12,45,23,26,5
    machine.RTC().datetime((int(tm[0]), int(tm[1]), int(tm[2]), int(tm[6]), int(tm[3]), int(tm[4]), int(tm[5]), 0))
    
            
def updatetime_func(): 
    try:
#        getNZtime.getNZtime()
#        micropythonntp.SyncNTPTime()
#        print(micropythonntp.Gettime())
         settimefromserver()
         print("1")
#        print("sync time success")
    except: 
        pass


def updatetimeTimer_func(): 
    updatetime_func() 
    while True:
        current_time=utime.localtime()
#         current_time=micropythonntp.Gettime()
        if(current_time[4]==5):
            updatetime_func()
            time.sleep(60)
        else:
            time.sleep(60)
      
            
TOPIC = b"OtherEquip"
OPSRelay=18
def mqttsub_cb(topic, msg):
    global OPSRelay 
    if(topic==TOPIC):
        if(msg.decode('utf-8')=='officedooropen'):
              commonhelper.updatePin(23,1)
              time.sleep(0.5)
              commonhelper.updatePin(23,0)
              print("hi")
        elif(msg.decode('utf-8')=='aircondition'):
            mqtthelper.pressmock_pmw() 

#                            


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
    MQTT_BROKER = commonhelper.readConfig('mqttbroker');
#    global TOPIC = b"ShowClockTime"
    User=commonhelper.readConfig('mqttuser');
    Password=commonhelper.readConfig('mqttpwd');
    mqttClient = MQTTClient("esp32_office", MQTT_BROKER,port=1883,ssl=False,user=User,password =Password,   keepalive=60)
#     mqttClient.set_callback(mqttsub_cb)
    autoconnectmqtt(mqttClient,True)
#     mqttClient.subscribe(TOPIC)
    print(f"Connected to MQTT  Broker :: {MQTT_BROKER}, and waiting for callback function to be called!")
    while True:
        try:
            if False: 
                mqttClient.wait_msg()
                 # Blocking wait for message
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
                    # print(f"Pinging MQTT Broker, last ping :: {now[0]}/{now[1]}/{now[2]} {now[3]}:{now[4]}:{now[5]}")
                time.sleep(1)
        except OSError as e:   
            autoconnectmqtt(mqttClient,False) 

        
    
            
#    print("Disconnecting...")
#    mqttClient.disconnect()

 
  