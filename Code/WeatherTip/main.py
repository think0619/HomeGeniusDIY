import requests
import json
import urllib.parse 
import datetime
import time  
from datetime import date

downloadFileHour =8
downloadFileMin=0  
 # 
def publishWeatherWechat(): 
    weatherRequest = requests.get('https://restapi.amap.com/v3/weather/weatherInfo?key=a50192af99bdbfc75df83077162c9b80&city=320111&extensions=base')
    if(weatherRequest.status_code==200):
        wobj = json.loads(weatherRequest.text)
        if(wobj["status"]=='1'):
          livedata=wobj["lives"][0]
          place_r=livedata["city"]
          weather_r=livedata["weather"]
          temperature_r=livedata["temperature"]
          humidity_r=livedata["humidity"]
          weatherinfo=place_r+" "+weather_r+"; 温:"+temperature_r+"℃; 湿:"+humidity_r+"% ." 
          info=urllib.parse.quote(weatherinfo)
          url="https://sctapi.ftqq.com/SCT211581TiLpK8QgVjoiCPbDz7QAf6pE3.send?title="+info 
          requests.get(url)

def publishWeatherForecastWechat(): 
    weatherRequest = requests.get('https://restapi.amap.com/v3/weather/weatherInfo?key=a50192af99bdbfc75df83077162c9b80&city=320111&extensions=all')
    if(weatherRequest.status_code==200):
        wobj = json.loads(weatherRequest.text)
        if(wobj["status"]=='1'):
          livedata=wobj["forecasts"][0]
          place_r=livedata["city"]
          cast_r=livedata["casts"][0]
          date_r=cast_r["date"]
          day_r=cast_r["dayweather"]
          night_r=cast_r["nightweather"]
          templow_r=cast_r["daytemp_float"]
          temphight_r=cast_r["nighttemp_float"] 

          weatherinfo=day_r+"~"+night_r+";"+temphight_r+"-"+templow_r+"℃"+" "+date_r
          info=urllib.parse.quote(weatherinfo)
          url="https://sctapi.ftqq.com/SCT211581TiLpK8QgVjoiCPbDz7QAf6pE3.send?title="+info 
          requests.get(url)

def publishWeatherForecastWXPusher(): 
    weatherRequest = requests.get('https://restapi.amap.com/v3/weather/weatherInfo?key=a50192af99bdbfc75df83077162c9b80&city=320111&extensions=all')
    if(weatherRequest.status_code==200):
        wobj = json.loads(weatherRequest.text)
        if(wobj["status"]=='1'):
          livedata=wobj["forecasts"][0]
          place_r=livedata["city"]
          cast_r=livedata["casts"][0]
          date_r=cast_r["date"]
          day_r=cast_r["dayweather"]
          night_r=cast_r["nightweather"]
          templow_r=cast_r["daytemp_float"]
          temphight_r=cast_r["nighttemp_float"]  
          weatherinfo=day_r+"~"+night_r+";"+temphight_r+"-"+templow_r+"℃"+" "+date_r 
          weatherinfo2=place_r+" "+weatherinfo
           
          url = "https://wxpusher.zjiecode.com/api/send/message"
          payload = json.dumps({
               "appToken": "AT_mMnmWO9xb1UPNnrixOqQfZNunDNOAk5e",
               "content": weatherinfo2,
               "summary": weatherinfo,
               "contentType": 1,
               "topicIds": [ ],
               "uids": ["UID_Oz43G2YWVwWb2Ipnc00PRMo0oVwZ"],
               "url": "",
               "verifyPay": False})
          headers = { 'Content-Type': 'application/json'}
          response = requests.request("POST", url, headers=headers, data=payload)

        



def publishBusinessTripWXPusher():  
  d0 = date(2023, 8, 3)
  d1 = date.today()
  deltaDays = (d1 - d0).days+1 
  tip="出差第 "+ str(deltaDays)+" 天"
  content=tip+"\r\n"+"出差开始日期:2023年8月3日"
  url = "https://wxpusher.zjiecode.com/api/send/message"
  payload = json.dumps({
     "appToken": "AT_64romn9ALIEkRLr3XzqOQXgQq1vklnS7",
     "content": content,
     "summary": tip,
     "contentType": 1,
     "topicIds": [ ],
     "uids": ["UID_UrrCtZV0ovUoiZm5kuBBMgze1B2I","UID_Oz43G2YWVwWb2Ipnc00PRMo0oVwZ"],
     "url": "",
     "verifyPay": False})
  headers = { 'Content-Type': 'application/json'}
  response = requests.request("POST", url, headers=headers, data=payload)
  print(response.text)

        



          # info=urllib.parse.quote(weatherinfo)
          # url="https://sctapi.ftqq.com/SCT211581TiLpK8QgVjoiCPbDz7QAf6pE3.send?title="+info 
          # requests.get(url)

 
publishWeatherForecastWXPusher()
publishBusinessTripWXPusher()
 

while True:
  now = datetime.datetime.now()  
  if(now.hour==downloadFileHour and now.minute==downloadFileMin):    
    time.sleep(70)
    publishWeatherForecastWXPusher() 
  else:
    time.sleep(60)


     

