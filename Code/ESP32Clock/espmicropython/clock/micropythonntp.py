from machine import RTC
from ntp import Ntp
import time

def ntp_log_callback(msg: str):
    print(msg)

def SyncNTPTime():
    _rtc = RTC()

    # Initializing
    Ntp.set_datetime_callback(_rtc.datetime)
    Ntp.set_logger_callback(ntp_log_callback) 
    # Set a list of valid hostnames/IPs
    Ntp.set_hosts(('0.cn.pool.ntp.org','1.cn.pool.ntp.org','2.cn.pool.ntp.org','3.cn.pool.ntp.org','ntp.ntsc.ac.cn','cn.ntp.org.cn','edu.ntp.org.cn','hk.ntp.org.cn','sgp.ntp.org.cn','ntp1.nim.ac.cn','time.pool.aliyun.com','time1.aliyun.com','time2.aliyun.com','ntp.tencent.com','ntp1.tencent.com','ntp.neu.edu.cn'))
    # Network timeout set to 1 second
    Ntp.set_ntp_timeout(30)
    # Set timezone to 2 hours and 0 minutes
    Ntp.set_timezone(8, 0) 
    # Set epoch to 1970. All time calculations will be according to this epoch
    Ntp.set_epoch(Ntp.EPOCH_1970) 
    # Syncing the RTC with the time from the NTP servers
    Ntp.rtc_sync()

def Gettime():
    return Ntp.time(False)