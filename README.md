# GCollect

If you are looking to collect cookies, welcome!

#### Available Browsers:
- Brave
- Opera
- Chrome
- Firefox
- Edge

#### Sends information to Discord WebHook:
- Files with the cookies. Please note that Firefox separates cookies for each accessed website, so there will be multiple files.
- In the Python version, it sends the IP address, amount of RAM, architecture, operating system, etc.
- In the C# version, it sends the operating system, processor information, network username, logged-in user on the machine, and storage disks.
- Both versions require running as an administrator.

### Detection Rate?
The obfuscation part is up to the user. However, in Python, I tried various obfuscation methods and didn't achieve good results. In C#, I compiled it for x64, passed Virus Total, and came out clean. Here are the detection rates:

Python:
- Black Veil + PyInstaller: 2/71
- Other obfuscation tools + PyInstaller: 12/71
- Black Veil + Nuitka: 20/71
- Other obfuscation tools + Nuitka: 20/71

C#:
- No technique x64: 0/71
- No technique x86: 2/71
