import time
import ubinascii
#from umqtt.simple import MQTTClient
import machine
from umqttsimple import MQTTClient
from machine import Pin 

# Default MQTT MQTT_BROKER to connect to
# const url = 'ws://hw.hellolinux.cn:8083/';
MQTT_BROKER = "hw.hellolinux.cn"
CLIENT_ID = ubinascii.hexlify(machine.unique_id())
TOPIC = b"OtherEquip"
User="homediskrelay"
Password="@Xiongsen1994!+"


# Ping the MQTT broker since we are not publishing any message
last_ping = time.time()
ping_interval = 60

# Received messages from subscriptions will be delivered to this callback
def sub_cb(topic, msg):
    print((topic, msg))
    if(topic==TOPIC):
        if(msg.decode('utf-8')=='aircondition'):
            pressmock_pmw()
        elif(msg.decode('utf-8')=='officedooropen'):
            pin_air=Pin(18,Pin.OUT) 
            pin_air.value(1)
            time.sleep(0.5)
            pin_air.value(0)


def reset():
    print("Resetting...")
    time.sleep(5)
    machine.reset()
    
def main():
    mqttClient = MQTTClient(CLIENT_ID, MQTT_BROKER,port=1883,ssl=False,user="homediskrelay",password ="@Xiongsen1994!+",   keepalive=60)
    mqttClient.set_callback(sub_cb)
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
            
    print("Disconnecting...")
    mqttClient.disconnect()


def pressmock_pmw():
    pwmpin = machine.Pin(19)
    pwmkey= machine.PWM(pwmpin)
    pwmkey.freq(50)
    
    pressdeg= 40
    zerodeg= 0
    ts_press = int((pressdeg/180*2 + 0.5) /20 *1023)
    ts_init = int((zerodeg/180*2 + 0.5) /20 *1023)
    pwmkey.duty(ts_init) 
    pwmkey.duty(pressdeg)
    time.sleep(0.3)
    pwmkey.duty(ts_init)
    time.sleep(2)
 
   

 
