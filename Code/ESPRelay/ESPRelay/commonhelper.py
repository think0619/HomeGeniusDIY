import urequests
import json
import ujson
import os

def xfill(number, width):
    numbers=str(number)
    return '{:0>{w}}'.format(numbers, w=width)

def writeInitConfig():
    config = {'needbreak': 1, 'ssid': 'HuaweiP40','password':'1qaz2wsx#EDC4rfv'}
    f = open('config.json', 'w')
    f.write(ujson.dumps(config))
    f.close()

def readConfig(key):
    f = open('config.json', 'r')
    c = ujson.loads(f.read())
    return c[key]
 
def needRun(host,key):
    url="http://"+host+"/api/configservice/get?key="+key
    print(url)
    response = urequests.get(url)
    result=response.text
    response.close()
    return  result=="11111"

def updatePin(pinno,pinvalue):
    from machine import Pin
    p0 = Pin(pinno, Pin.OUT)
    p0.value(pinvalue)
    print(str(pinno),str(pinvalue))
