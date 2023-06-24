
# GCollect

Se você está buscando coletar cookies, bem-vindo! 


#### - Navegadores disponíveis:
- Brave 
- Opera
- Chrome 
- Firefox
- Edge


#### - Envia as informações ao WebHook do discord:
- Arquivos com os cookies, vale lembrar que o navegador firefox separa os cookies de cada site acessado, assim vai mais arquivos!
- Na versão feita em Python, envia o endereço IP, quantia de ram, arquitetura, sistema operacional, etc.
- Na versão feita em C-Sharp, envia o sistema operacional, informações do processador, username na rede, usuário logado na máquina e discos de armazenamento.
- Ambas versões pedem para executar como administrador.

### Rate de detecção?
A parte de ofuscação já é com o usuário, porém no Python, eu tentei ofuscar de diversas maneiras e não obtive resultados bons, já no C-Sharp, compilei para x64, passou no Virus Total e passou limpo, segue as taxas de rate:

Python:
- Black Veil + PyInstaller: 2/71
- Outras ferramentas de ofuscação + PyInstaller: 12/71
- Black Veil + Nuitka: 20/71
- Outras ferramentas de ofuscação + Nuitka: 20/71

C#:
- Nenhuma técnica x64: 0/71
- Nenhuma técnica x86: 2/71

