import requests
import json  

def pushWechatMsg(msgcontent,msgsummary):  
    url = "https://wxpusher.zjiecode.com/api/send/message"
    payload = json.dumps({
        "appToken": "AT_mMnmWO9xb1UPNnrixOqQfZNunDNOAk5e",
        "content": msgcontent,
        "summary": msgsummary,
        "contentType": 1,
        "topicIds": [ ],
        "uids": ["UID_Oz43G2YWVwWb2Ipnc00PRMo0oVwZ"],
        "url": "",
        "verifyPay": False})
    headers = { 'Content-Type': 'application/json'}
    response = requests.request("POST", url, headers=headers, data=payload)

        
