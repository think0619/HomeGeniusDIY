from git import Repo
import git
import os
from datetime import datetime

def cloneRepo(repourl, localdir):
    repo = Repo.clone_from(repourl, localdir)

def gitpull(folderpath:str):
    if(os.path.exists(folderpath)):
        g = git.cmd.Git(folderpath)
        msg=g.pull()
        print(msg)
        f = open("logs.txt", "a+") 
        f.write(f"{datetime.now()} {folderpath} log:{msg}")
        f.write("\n")
        f.close()
    else:
        print('folder err') 

# repo_url="ssh://git@hw.hellolinux.cn:64722/gitlab-instance-70610347/testpythongit.git"
# localdir="D:\\logs\\test"
# # cloneRepo(repo_url,localdir)

# g = git.cmd.Git(localdir)
# msg=g.pull()
# print(msg)