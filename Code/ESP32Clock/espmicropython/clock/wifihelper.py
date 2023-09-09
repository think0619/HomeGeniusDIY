import network

def connectwifi(ssid,password):    
    wlan = network.WLAN(network.STA_IF)
    wlan.active(True)
    if(wlan.isconnected()):
        print('has connected wifi')
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
        return True;
    else:
        return False;

#do_connect()

#wlan = network.WLAN(network.STA_IF) # create station interface
#wlan.active(True)       # activate the interface
#wlan.scan()             # scan for access points
#wlan.isconnected()      # check if the station is connected to an AP
#wlan.connect('Huawei P40', '1qaz2wsx#EDC4rfv') # connect to an AP 
#wlan.ifconfig()         # get the interface's IP/netmask/gw/DNS addresses

