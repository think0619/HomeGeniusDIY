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
    gitSaveFolder="D:\\logs"
    gitSSHHost="192.168.2.100:4722" 
    #Check if the folder exists to determine if the external hard drive is mounted correctly.
    if(os.path.exists(gitSaveFolder)):
        f = open("gitsrc.txt", "r")
        lines=f.readlines()
        for li in lines:
            try:
                gitpath = f"git clone ssh://git@{gitSSHHost}/{li.strip()}" 
                cloneRepo(gitpath,gitSaveFolder)
                f = open("logs.txt", "a+") 
                f.write(f"Clone Successfully.{gitpath}")
                f.write("\n")
                f.close()
            except:
                pass 

if "__main__" == __name__:
    gitclone()
    # gitpullScheduler = BackgroundScheduler() 
    # gitpullScheduler.add_job(reposPull, 'cron',day_of_week ="0-6", hour=3,minute=1) 
    # gitpullScheduler.start()
    
