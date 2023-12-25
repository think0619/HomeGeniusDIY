import subprocess

def synctime():
    synctimecmd="sudo date -s \"$(wget -qSO- --max-redirect=0 baidu.com 2>&1 | grep Date: | cut -d' ' -f5-8)Z\""
    result1 = subprocess.run(synctimecmd, shell=True, capture_output=False, text=False)

