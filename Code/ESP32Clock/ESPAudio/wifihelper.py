import network 
import time 

_ssid=""
_password=""
wlan=network.WLAN(network.STA_IF)

def connectwifi(ssid,password):
    global wlan
    global _ssid
    global _password 
    wlan=network.WLAN(network.STA_IF)
    _ssid=ssid
    _password=password 
    wlan.active(True)
    time.sleep(3)
    if(wlan.isconnected()):
        print('ESP3266 has connected wifi.')
    else:
        print(ssid)
        wlan.connect(ssid,password)
        print('connecting to network...')
        while wlan.isconnected()==False:
            pass 
    if(wlan.isconnected()):
        print('network config:', wlan.ifconfig())
        return True;
    else:
        return False; 
 
#    _thread.start_new_thread(checkwifi, ())    

def checkwifi():
    time.sleep(60)
    global _ssid
    global _password 
    global wlan 
    while True:
        if not wlan.isconnected(): 
            try: 
                wlan.connect(_ssid,_password) 
                while not wlan.isconnected():
                    pass 
            except:
                print('connect error')
    time.sleep(60)
#
 


#do_connect()

#wlan = network.WLAN(network.STA_IF) # create station interface
#wlan.active(True)       # activate the interface
#wlan.scan()             # scan for access points
#wlan.isconnected()      # check if the station is connected to an AP
#wlan.connect('Huawei P40', '1qaz2wsx#EDC4rfv') # connect to an AP 
#wlan.ifconfig()         # get the interface's IP/netmask/gw/DNS addresses

