import os
import requests
from bs4 import BeautifulSoup
from wechatpush import pushWechatMsg

def sign(cookie):
   # cookie="htVC_2132_atarget=1; htVC_2132_saltkey=q2beFgGJ; htVC_2132_lastvisit=1700550945; htVC_2132_auth=b574lf3QvBZuQ60qz%2B6B6DIDSOc0RWYfuEAbbZUupMMuf2cQGhw2LoDOP7h1negfHmcY8J7K82%2FLfke6weOecczvvvOl; KF4=o1ugOj; htVC_2132_connect_is_bind=0; htVC_2132_nofavfid=1; wzws_sessionid=gWY1MjlhM6Blb81lgmRiMWNhYYA0OS43Ny4xMjMuOTY=; htVC_2132_sid=0; htVC_2132_noticonf=1949890D1D3_3_1; htVC_2132_ulastactivity=1702090086%7C0; Hm_lvt_46d556462595ed05e05f009cdafff31a=1700412986,1700703844,1701825893; wzws_sid=3a0b5afca7db1bea8d3dec967e0ff48b279ecdc31756238c245a6e1b7dca02b9b6cc1e0db562035c98aed376659bf04af8075658579b551642bffa2e009ad85339502d349668c37cf5a967c5705b0e00f94094d8340675308bc40b93bfb4244a25364b35fff400cbb1f673f51d8b0dda; htVC_2132_ttask=1949890%7C20231209; htVC_2132_smile=1D1; htVC_2132_st_p=1949890%7C1702090382%7C708610107440fa9a82b9aab3748fc7a8; htVC_2132_visitedfid=65D16D24D4; htVC_2132_viewid=tid_1865310; htVC_2132_lastcheckfeed=1949890%7C1702090383; htVC_2132_checkfollow=1; Hm_lpvt_46d556462595ed05e05f009cdafff31a=1702090394; htVC_2132_secqaaqS0=7347317.4faec34e2ece101daf; htVC_2132_lastact=1702090384%09misc.php%09seccode; htVC_2132_seccodecS0=7347316.ca5d70f16c68e4d713"
    result = ""
    headers = {
        "Cookie": cookie,
        "ContentType": "text/html;charset=gbk",
        "User-Agent": "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36",
    }
    requests.session().put(
        "https://www.52pojie.cn/home.php?mod=task&do=apply&id=2", headers=headers
    )
    fa = requests.session().put(
        "https://www.52pojie.cn/home.php?mod=task&do=draw&id=2", headers=headers
    )
    fb = BeautifulSoup(fa.text, "html.parser")
    fc = fb.find("div", id="messagetext").find("p").text
    if "您需要先登录才能继续本操作" in fc:
        result += "Cookie 失效"
    elif "恭喜" in fc:
        result += "签到成功"
    elif "不是进行中的任务" in fc:
        result += "今日已签到"
    else:
        result += "签到失败"
    pushWechatMsg(result,'吾爱破解签到')
    return result 

 
 