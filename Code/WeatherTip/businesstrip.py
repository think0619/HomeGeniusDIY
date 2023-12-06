import requests
import json 
from datetime import date 
 
def publishBusinessTripWXPusher():  
  d0 = date(2023, 12, 6)
  d1 = date.today()
  deltaDays = (d1 - d0).days+1 
  tip="出差第 "+ str(deltaDays)+" 天"
  content=tip+"\r\n"+"出差开始日期:2023年12月6日"
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