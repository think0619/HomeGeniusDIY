import requests
import json
import urllib.parse 
import datetime
import time  
from datetime import date
from wechatpush import pushWechatMsg
from weather import publishWeatherForecastWXPusher
from aliyunpan.main import signin
from businesstrip import publishBusinessTripWXPusher
from enshanright import enshanSignin

downloadFileHour =7
downloadFileMin=25     
aliyunpantoken='c92845b3943b4c77bc7255f94f7672bd' 

publishWeatherForecastWXPusher()  
signin(aliyunpantoken)
publishBusinessTripWXPusher()
enshanSignin()

while True:
  now = datetime.datetime.now()  
  if(now.hour==downloadFileHour and now.minute==downloadFileMin):    
    time.sleep(70)
    publishWeatherForecastWXPusher()  
    signin(aliyunpantoken)
    publishBusinessTripWXPusher()
    enshanSignin()
  else:
    time.sleep(60)


     

