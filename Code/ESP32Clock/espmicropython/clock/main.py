from machine import Pin, SPI
import utime 
import time
import commonhelper
import _thread
import ntptime
import wifihelper
import ntphelper
import max7219
import tm1637
from umqtt.simple import MQTTClient
import micropythonntp

clkPin=13
dioPin=14 
showClock=True       

            
def connectwifi_func():
    ssid=commonhelper.readConfig('ssid');
    pwd=commonhelper.readConfig('password');
    
    connectR =wifihelper.connectwifi(ssid, pwd)
    print("WiFi conncet stutus:"+str(connectR))
    return connectR

#     5v  vcc
#     GND GND
#     D2 MOSI DIN
#     D5 CS CS
#     D4 SCK CLK   
def showTimeOnMax7219():
    spi  = SPI(1, baudrate=10000000, polarity=1, phase=0, sck=Pin(4), mosi=Pin(2)) 
    ss = Pin(5, Pin.OUT)
    msg="Good"
    length = len(msg)
    length = (length*8)
    display = max7219.Matrix8x8(spi, ss, 4)
    display.brightness(1)   # adjust brightness 1 to 15
    display.fill(0)
    display.show()
    time.sleep(3)
    
    index=0
    while True:
        current_time=utime.localtime()
#         current_time=micropythonntp.Gettime()
        hour=commonhelper.xfill(current_time[3],2)
        minute=commonhelper.xfill(current_time[4],2)  
        display.fill(0)
        global showClock
        if( showClock):
            display.text(hour+minute,0,0,1)
            if(index==0): 
#               display.rect (0, 7, 1, 1,1) 
#               display.rect (7, 7, 1, 1,1) 
#               display.rect (15, 7, 1, 1,1)  
#               display.rect (23, 7, 1, 1,1) 
                display.rect (31, 7, 1, 1,1)    
        display.show() 
        index = (index+1) % 2 
        time.sleep(1)  

def showMsgOnMax7219(msg):
    spi  = SPI(1, baudrate=10000000, polarity=1, phase=0, sck=Pin(4), mosi=Pin(2)) 
    ss = Pin(5, Pin.OUT) 
    length = len(msg)
    length = (length*8)
    display = max7219.Matrix8x8(spi, ss, 4)
    display.brightness(1)   # adjust brightness 1 to 15
    display.fill(0)
    display.show()
       
def updatetime_func(): 
    try:
#        micropythonntp.SyncNTPTime()
#        print(micropythonntp.Gettime())
        ntphelper.settime() 
        print("sync time success")
    except: 
        showMsgOnMax7219('sync time error')  
    

def updatetimeTimer_func(): 
    updatetime_func()
    time.sleep(60)  
    updatetime_func() 
    time.sleep(60)   
    updatetime_func() 
    time.sleep(60)   
    while True:
        current_time=utime.localtime()
#         current_time=micropythonntp.Gettime()
        if(current_time[4]==0):
            updatetime_func()
            time.sleep(60)
        else:
            time.sleep(60)
      
            
TOPIC = b"ShowClockTime"

def mqttsub_cb(topic, msg):
    global showClock
    if(topic==TOPIC):
        if(msg.decode('utf-8')=='on'):
              showClock=True 
        elif(msg.decode('utf-8')=='off'):
              showClock=False 
        elif(msg.decode('utf-8')=='synctime'):
              updatetime_func() 
        print(str(showClock))
        
        
last_ping = time.time()
ping_interval = 60
def connectmqtt():
    MQTT_BROKER = commonhelper.readConfig('mqttbroker');
#    global TOPIC = b"ShowClockTime"
    User=commonhelper.readConfig('mqttuser');
    Password=commonhelper.readConfig('mqttpwd');
    mqttClient = MQTTClient("esp32_clock", MQTT_BROKER,port=1883,ssl=False,user=User,password =Password,   keepalive=60)
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
            
#    print("Disconnecting...")
#    mqttClient.disconnect()


#flow effect
#     while True:
#         for x in range(32, -length, -1):
#             display.text(msg ,x,0,1)
#             display.show()
#             time.sleep(0.10)
#             display.fill(0)

def days_between(d1, d2):
    d1 += (1, 0, 0, 0, 0)  # ensure a time past midnight
    d2 += (1, 0, 0, 0, 0)
    diffdays=(utime.mktime(d1) // (24*3600) - utime.mktime(d2) // (24*3600))+1
    return diffdays

      
def show1637(): 
    clkPin=33
    dioPin=32 
    tm = tm1637.TM1637(clk=Pin(clkPin), dio=Pin(dioPin)) 
    tm.brightness(0) 
    showColon=True
    while True: 
        current_time=utime.localtime()
#         current_time=micropythonntp.Gettime()
        cursec=current_time[5] % 20  
        showstr=''
        if(cursec<10): 
            diffdays=days_between((current_time[0],current_time[1],current_time[2]), (2022, 1, 26)) 
            showstr=commonhelper.xfill(diffdays,4)
        else:
            showstr=commonhelper.xfill(current_time[1],2)+commonhelper.xfill(current_time[2],2)
        tm.show(showstr , showColon) 
        showColon=not showColon 
        time.sleep(1) 

  