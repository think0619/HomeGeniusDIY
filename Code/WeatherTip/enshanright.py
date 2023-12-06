#!/usr/bin/python3
# -- coding: utf-8 -- 
# -------------------------------
# @Author : github@wd210010 https://github.com/wd210010/only_for_happly
# @Time : 2023/10/4 16:23
# -------------------------------
# cron "0 0 2 * * *" script-path=xxx.py,tag=匹配cron用
# const $ = new Env('恩山签到')
import requests,re,os
from wechatpush import pushWechatMsg

# #配置恩山的cookie 到配置文件config.sh export enshanck='' 需要推送配置推送加token export plustoken=''
# enshanck = "ttwid=1%7CdZIWnvJl2ebDQvGUusV3yd1lszDZkbjTJ9qrCgH2U5A%7C1700443115%7C586f9404cbcad0c022d212f18759fab8989f94362a315b5468a187b08941248f"

# #推送加 token
# plustoken = os.getenv("plustoken")

# def Push(contents):
#     # 推送加
#     # headers = {'Content-Type': 'application/json'}
#     # json = {"token": plustoken, 'title': '恩山签到', 'content': contents.replace('\n', '<br>'), "template": "json"}
#     # resp = requests.post(f'http://www.pushplus.plus/send', json=json, headers=headers).json()

#     pushWechatMsg(contents,'恩山签到')
#     # print('push+推送成功' if resp['code'] == 200 else 'push+推送失败')

# headers = {
#     "User-Agent": "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.5359.125 Safari/537.36",
#     "Cookie": enshanck,
# }
# session = requests.session()
# response = session.get('https://www.right.com.cn/FORUM/home.php?mod=spacecp&ac=credit&showcredit=1', headers=headers)
# try:
#     coin = re.findall("恩山币: </em>(.*?)nb &nbsp;", response.text)[0]
#     point = re.findall("<em>积分: </em>(.*?)<span", response.text)[0]
#     res = f"恩山币：{coin}\n积分：{point}"
#     print(res)
#     Push(contents=res)
# except Exception as e:
#     res = str(e)



def enshanSignin():
    enshanck = "_dx_captcha_cid=73159651; _dx_uzZo5y=756f42c4f4c6d3a1978912500ef9010e844e96e4b3d83ac52a59040e625ef477a014399b; __gads=ID=b6c49031bcdf592a:T=1699953043:RT=1700450995:S=ALNI_MbkINI62pOZ3L-Q5O1uKwZeX5bhTQ; __gpi=UID=00000c8624fb3855:T=1699953043:RT=1700450995:S=ALNI_MaEUwDw6qYWI4Hz4l9XhlxmlCe1Sg; FCNEC=%5B%5B%22AKsRol-sKJ8cncHkaOYhU7XeGhVpESML8d11F2tUsKGy9X8ukbFp8wpK_TT_1RfPLBJUqv06Zx2PDZg8PRTEM8B0g1vK61nkEc3YptFxZ-qrXFD80mWAGTktPAqfLL8Ssc_oAaPXe8uGnSkC-MkCQk8PkZes4zGFEA%3D%3D%22%5D%2Cnull%2C%5B%5D%5D; TWcq_2132_saltkey=qA8zo27r; TWcq_2132_lastvisit=1700551612; _dx_FMrPY6=655c69cdDB4Kcoa2Ikt6CjFBY2uHlCnZqDD9Ejn1; _dx_app_captchadiscuzpluginbydingxiang2017=655c69cdDB4Kcoa2Ikt6CjFBY2uHlCnZqDD9Ejn1; TWcq_2132_connect_is_bind=0; TWcq_2132_nofavfid=1; TWcq_2132_atarget=1; TWcq_2132_smile=1D1; _dx_captcha_vid=D2B140821D08EB2C2678BCA4C7889CA79BBBA72063D21AEA33DCBB04998B7A20B9D8A6BAF7D1F439B27D7D00E670561259B18F27180F221E7709C1E9DB4BF683A26F6D3BABEBD4D26A52C4FCD6C83F97; TWcq_2132_auth=7a09jGh1Ci0YE1Ri0Bru1BvD55cX%2Br3IITcbKbw4kNSqGje2eY%2BEnqCRw9RkJLm%2FVnLn5ZzmATSsg7TrwCrS9KdXezw; TWcq_2132_lastcheckfeed=329847%7C1700827351; TWcq_2132_pc_size_c=0; TWcq_2132_viewid=tid_4076037; TWcq_2132_ulastactivity=23c2KpPm9X2CalWDiGEMm2%2BP8Gn%2B0bSmDPFlVpHdmKZxb4X7VA9o; Hm_lvt_4fd617216d6743edf4851b17882cdd82=1700827322,1701047499,1701676335,1701760775; TWcq_2132_st_p=329847%7C1701761198%7C60f00cafead78c53f9c82a543e99278f; TWcq_2132_home_diymode=1; TWcq_2132_sid=u2gGZQ; TWcq_2132_lip=49.77.123.96%2C1701761545; TWcq_2132_checkpm=1; TWcq_2132_sendmail=1; Hm_lpvt_4fd617216d6743edf4851b17882cdd82=1701761547; TWcq_2132_lastact=1701761547%09misc.php%09patch"
    headers = {
    "User-Agent": "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.5359.125 Safari/537.36",
    "Cookie": enshanck,
    }
    session = requests.session()
    response = session.get('https://www.right.com.cn/FORUM/home.php?mod=spacecp&ac=credit&showcredit=1', headers=headers)
    try:
        coin = re.findall("恩山币: </em>(.*?)nb &nbsp;", response.text)[0]
        point = re.findall("<em>积分: </em>(.*?)<span", response.text)[0]
        res = f"恩山币：{coin}\n积分：{point}" 
        pushWechatMsg(res,'恩山签到')
    except Exception as e:
        res = str(e)
        print(res)
       

