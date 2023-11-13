import urequests
import json
import ujson
import os
import time

def xfill(number, width):
    numbers=str(number)
    return '{:0>{w}}'.format(numbers, w=width)

def needRun():
    response = urequests.get("https://hw.hellolinux.cn:8888/api/configservice/get?key=espclockrun")
    result=response.text 
    return  result=="11111"  

def writeInitConfig():
    config = {'needbreak': 1, 'ssid': 'HuaweiP40','password':'1qaz2wsx#EDC4rfv'}
    f = open('config.json', 'w')
    f.write(ujson.dumps(config))
    f.close()

def readConfig(key):
    f = open('config.json', 'r')
    c = ujson.loads(f.read())
    return c[key]



def readResetConfig(key):
    f = open('reset.json', 'r')
    c = ujson.loads(f.read())
    return c[key]

def changeResetConfig(value):
    config = {'needreset': value}
    f = open('reset.json', 'w')
    f.write(ujson.dumps(config))
    f.close()
    
def getdatetimefromserver(): 
    response2 = urequests.get("https://hw.hellolinux.cn:8888/api/configservice/timenow?zonehour=0")
    return  response2.text  