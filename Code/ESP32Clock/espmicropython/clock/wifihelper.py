import network
import _thread
import time

wlan=network.WLAN(network.STA_IF)
_ssid=''
_password=''

def connectwifi(ssid,password):
    _ssid=ssid
    _password=password
    wlan.active(True)
    if(wlan.isconnected()):
        print('ESP32 has connected wifi.')
    else: 
        if not wlan.isconnected():
            print('connecting to network...')
            try: 
                wlan.connect(ssid,password) 
                while not wlan.isconnected():
                    pass 
            except:
                print('connect error')
    if(wlan.isconnected()):
        print('network config:', wlan.ifconfig())
        return True
    else:
        return False
    _thread.start_new_thread(checkwifi, ())    

def checkwifi():
    while True:
        if not wlan.isconnected():
            wlan.active(True)
            try:
                wlan.connect(_ssid,_password) 
                while not wlan.isconnected():
                    pass 
            except:
                print('connect error')
    time.sleep(3600)
#
 


#do_connect()

#wlan = network.WLAN(network.STA_IF) # create station interface
#wlan.active(True)       # activate the interface
#wlan.scan()             # scan for access points
#wlan.isconnected()      # check if the station is connected to an AP
#wlan.connect('Huawei P40', '1qaz2wsx#EDC4rfv') # connect to an AP 
#wlan.ifconfig()         # get the interface's IP/netmask/gw/DNS addresses

