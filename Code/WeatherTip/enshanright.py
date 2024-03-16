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
from lxml import etree

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
    enshanck = "_dx_captcha_cid=73159651; _dx_uzZo5y=756f42c4f4c6d3a1978912500ef9010e844e96e4b3d83ac52a59040e625ef477a014399b; __gads=ID=b6c49031bcdf592a:T=1699953043:RT=1700450995:S=ALNI_MbkINI62pOZ3L-Q5O1uKwZeX5bhTQ; __gpi=UID=00000c8624fb3855:T=1699953043:RT=1700450995:S=ALNI_MaEUwDw6qYWI4Hz4l9XhlxmlCe1Sg; FCNEC=%5B%5B%22AKsRol-sKJ8cncHkaOYhU7XeGhVpESML8d11F2tUsKGy9X8ukbFp8wpK_TT_1RfPLBJUqv06Zx2PDZg8PRTEM8B0g1vK61nkEc3YptFxZ-qrXFD80mWAGTktPAqfLL8Ssc_oAaPXe8uGnSkC-MkCQk8PkZes4zGFEA%3D%3D%22%5D%2Cnull%2C%5B%5D%5D; TWcq_2132_connect_is_bind=0; TWcq_2132_nofavfid=1; TWcq_2132_smile=1D1; TWcq_2132_home_readfeed=1702260386; TWcq_2132_saltkey=Tt5K8Gq8; TWcq_2132_lastvisit=1703487373; TWcq_2132_atarget=1; TWcq_2132_pc_size_c=0; _dx_FMrPY6=65966322TzwMxFAQb93rZu77W4uOHikeWIkb9i61; _dx_app_captchadiscuzpluginbydingxiang2017=65966322TzwMxFAQb93rZu77W4uOHikeWIkb9i61; TWcq_2132_sendmail=1; Hm_lvt_4fd617216d6743edf4851b17882cdd82=1702366005,1703490974,1704354594,1704421521; _dx_captcha_vid=42FD2E0A2B01FC71A0E879FAEB4BB4B5C6484F0D63005D2B7914B24BF31C8620BD28D4F34FFD768F62700DF9E03F1C0E7BCCA0CAFE72A96DB7C3B3DCCE5970758339CD6AD16DFFD33A0D9C8BFAC0BD99; TWcq_2132_sid=UeIMiv; TWcq_2132_ulastactivity=36b5TrvFd2ooMolN%2FUiSQ%2F65v9L9X%2FpaDvc85ToYBZfm2K4vfYgD; TWcq_2132_auth=4782hAktdmPr3eCnTRKHRPeThBT1ufHdB6ExyClcdH4REZSDXraUwWNSg53bcWllDnagYJg6wcr7q2Zel7afZPPaiUo; TWcq_2132_lastcheckfeed=329847%7C1704421543; TWcq_2132_st_p=329847%7C1704421566%7C6b94a89a04d1c605e1292fd778d7c727; TWcq_2132_viewid=tid_8303344; Hm_lpvt_4fd617216d6743edf4851b17882cdd82=1704421567; TWcq_2132_lastact=1704421592%09forum.php%09ajax"
    headers = {
    "User-Agent": "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.5359.125 Safari/537.36",
    "Cookie": enshanck,
    }
    session = requests.session()
    response = session.get('https://www.right.com.cn/FORUM/home.php?mod=spacecp&ac=credit&showcredit=1', headers=headers)
    try:
        print( response.text)
        coin = re.findall("恩山币: </em>(.*?)nb &nbsp;", response.text)[0]
        point = re.findall("<em>积分: </em>(.*?)<span", response.text)[0]
        res = f"恩山币：{coin}\n积分：{point}" 
        pushWechatMsg(res,'恩山签到')
    except Exception as e:
        res = str(e)
        print(res)

def enshanSignIn2():
    #enshancookie = "_dx_captcha_cid=73159651; _dx_uzZo5y=756f42c4f4c6d3a1978912500ef9010e844e96e4b3d83ac52a59040e625ef477a014399b; __gads=ID=b6c49031bcdf592a:T=1699953043:RT=1700450995:S=ALNI_MbkINI62pOZ3L-Q5O1uKwZeX5bhTQ; __gpi=UID=00000c8624fb3855:T=1699953043:RT=1700450995:S=ALNI_MaEUwDw6qYWI4Hz4l9XhlxmlCe1Sg; FCNEC=%5B%5B%22AKsRol-sKJ8cncHkaOYhU7XeGhVpESML8d11F2tUsKGy9X8ukbFp8wpK_TT_1RfPLBJUqv06Zx2PDZg8PRTEM8B0g1vK61nkEc3YptFxZ-qrXFD80mWAGTktPAqfLL8Ssc_oAaPXe8uGnSkC-MkCQk8PkZes4zGFEA%3D%3D%22%5D%2Cnull%2C%5B%5D%5D; TWcq_2132_connect_is_bind=0; TWcq_2132_nofavfid=1; TWcq_2132_smile=1D1; TWcq_2132_home_readfeed=1702260386; TWcq_2132_saltkey=R24T9v6w; TWcq_2132_lastvisit=1708300103; TWcq_2132_pc_size_c=0; Hm_lvt_4fd617216d6743edf4851b17882cdd82=1708303704; _dx_FMrPY6=65d2a558BprzhAvX6RyA6rUyy0iN5EtSS11l5no1; _dx_app_captchadiscuzpluginbydingxiang2017=65d2a558BprzhAvX6RyA6rUyy0iN5EtSS11l5no1; _dx_captcha_vid=1404B507CF2CCE41A06BCA1FC85B97F952E00A98784A65ED21DC738CF484A048084728CF44A95E42633BE13E171DF71CA88FDDDF0585B94940BF9B7307C2DAF2CCCF92D9E2E4FBD4816F029854730F8A; TWcq_2132_auth=0fe2CDApSdUp5nXlcJgZewlEc5qhdtq3HqqOmTp56pcJGGcdrrt%2Bgb7N3CeAt8qRydM3AJwcIRe7B6OQOy%2B8V4GJVNs; TWcq_2132_lastcheckfeed=329847%7C1708303717; TWcq_2132_lip=49.77.122.93%2C1708303717; TWcq_2132_sid=0; TWcq_2132_ulastactivity=1708306569%7C0; TWcq_2132_home_diymode=1; TWcq_2132_st_p=329847%7C1708306719%7Cc2aa646768c0ebf97da13757225c260d; TWcq_2132_viewid=tid_1811791; TWcq_2132_checkpm=1; Hm_lpvt_4fd617216d6743edf4851b17882cdd82=1708306720; TWcq_2132_lastact=1708306728%09forum.php%09ajax"
    enshancookie ="_dx_captcha_cid=73159651; _dx_uzZo5y=756f42c4f4c6d3a1978912500ef9010e844e96e4b3d83ac52a59040e625ef477a014399b; __gads=ID=b6c49031bcdf592a:T=1699953043:RT=1700450995:S=ALNI_MbkINI62pOZ3L-Q5O1uKwZeX5bhTQ; __gpi=UID=00000c8624fb3855:T=1699953043:RT=1700450995:S=ALNI_MaEUwDw6qYWI4Hz4l9XhlxmlCe1Sg; FCNEC=%5B%5B%22AKsRol-sKJ8cncHkaOYhU7XeGhVpESML8d11F2tUsKGy9X8ukbFp8wpK_TT_1RfPLBJUqv06Zx2PDZg8PRTEM8B0g1vK61nkEc3YptFxZ-qrXFD80mWAGTktPAqfLL8Ssc_oAaPXe8uGnSkC-MkCQk8PkZes4zGFEA%3D%3D%22%5D%2Cnull%2C%5B%5D%5D; TWcq_2132_connect_is_bind=0; TWcq_2132_nofavfid=1; TWcq_2132_smile=1D1; TWcq_2132_home_readfeed=1702260386; TWcq_2132_saltkey=R24T9v6w; TWcq_2132_lastvisit=1708300103; Hm_lvt_4fd617216d6743edf4851b17882cdd82=1708303704; _dx_FMrPY6=65d2a558BprzhAvX6RyA6rUyy0iN5EtSS11l5no1; _dx_app_captchadiscuzpluginbydingxiang2017=65d2a558BprzhAvX6RyA6rUyy0iN5EtSS11l5no1; _dx_captcha_vid=1404B507CF2CCE41A06BCA1FC85B97F952E00A98784A65ED21DC738CF484A048084728CF44A95E42633BE13E171DF71CA88FDDDF0585B94940BF9B7307C2DAF2CCCF92D9E2E4FBD4816F029854730F8A; TWcq_2132_auth=0fe2CDApSdUp5nXlcJgZewlEc5qhdtq3HqqOmTp56pcJGGcdrrt%2Bgb7N3CeAt8qRydM3AJwcIRe7B6OQOy%2B8V4GJVNs; TWcq_2132_lastcheckfeed=329847%7C1708303717; TWcq_2132_lip=49.77.122.93%2C1708303717; TWcq_2132_sid=0; TWcq_2132_home_diymode=0; TWcq_2132_st_t=329847%7C1708322994%7C7bf364837dfbe9cdf0c93e83cc92fdc0; TWcq_2132_atarget=1; TWcq_2132_forum_lastvisit=D_182_1708322994; TWcq_2132_pc_size_c=0; TWcq_2132_viewid=tid_4076037; TWcq_2132_ulastactivity=1708390538%7C0; TWcq_2132_st_p=329847%7C1708390550%7Cacb23c8bc1008a8b2e3b65a870017ec6; TWcq_2132_checkpm=1; TWcq_2132_lastact=1708390593%09forum.php%09; Hm_lpvt_4fd617216d6743edf4851b17882cdd82=1708390593"
    headers = {
        "User-Agent": "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/108.0.5359.125 Safari/537.36",
         "Cookie": enshancookie,
    }
    msg=""
    session = requests.session()
    url = "https://www.right.com.cn/forum/home.php?mod=spacecp&ac=credit&op=log&suboperation=creditrulelog"
    try:
        r = session.get(url, headers=headers, timeout=120)
        # print(r.text)
        if '每天登录' in r.text:
            h = etree.HTML(r.text)
            data = h.xpath('//tr/td[6]/text()')
            msg += f'签到成功或今日已签到，最后签到时间：{data[0]}'
        else:
            msg += '签到失败，可能是cookie失效了！' 
    except:
        msg = '签到脚本出异常了'
    pushWechatMsg(msg,'恩山签到') 
       

