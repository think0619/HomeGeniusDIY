from pathlib import Path
from githelper import gitpull,cloneRepo
from apscheduler.schedulers.background import BackgroundScheduler  
import os
 
def reposPull():
    f = open("gitfolder.txt", "r")
    lines=f.readlines()
    for li in lines:
        try:
            gitpath = Path(li.strip()) 
            gitpull(gitpath)
        except:
            pass

def gitclone():
    gitSaveFolder="/home/think/outerdisk/gitbak"
    gitSSHHost="192.168.2.100:4722" 
    #Check if the folder exists to determine if the external hard drive is mounted correctly.
    if(os.path.exists(gitSaveFolder)):
        f = open("gitsrc.txt", "r")
        lines=f.readlines()
        for li in lines:
            try:
                #git clone ssh://git@192.168.2.100:4722/root/bakuangxr2022.git
                gitpath = f"ssh://git@{gitSSHHost}/root/{li.strip()}" 
                print(gitpath)
                savepath=f"{gitSaveFolder}/{li.strip().replace('.git','')}"
                print(savepath)
                cloneRepo(gitpath,savepath)
                f = open("logs.txt", "a+") 
                f.write(f"Clone Successfully.{gitpath}")
                f.write("\n")
                f.close()
            except:
                pass 

if "__main__" == __name__:
    # gitclone()
    gitpullScheduler = BackgroundScheduler() 
    gitpullScheduler.add_job(reposPull, 'cron',day_of_week ="0-6", hour=3,minute=0) 
    gitpullScheduler.start()
    while True:
        pass
    
