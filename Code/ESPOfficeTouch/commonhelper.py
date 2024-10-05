import urequests
import json
import ujson
import os

def xfill(number, width):
    numbers=str(number)
    return '{:0>{w}}'.format(numbers, w=width)

def needRun():
    response = urequests.get("https://hw.hellolinux.cn:8888/api/configservice/get?key=espopsrelay")
    result=response.text
    response.close()
    return  result=="11111"  
 

def readConfig(key):
    f = open('config.json', 'r')
    c = ujson.loads(f.read())
    return c[key]

def updatePin(pinno,pinvalue):
    from machine import Pin
    p0 = Pin(pinno, Pin.OUT)
    p0.value(pinvalue)
    print(str(pinno),str(pinvalue))


