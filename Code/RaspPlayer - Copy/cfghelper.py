import json
import ujson
import os 

def xfill(number, width):
    numbers=str(number)
    return '{:0>{w}}'.format(numbers, w=width)  

def readConfig(key):
    f = open('config.json', 'r')
    c = ujson.loads(f.read())
    return c[key]


 