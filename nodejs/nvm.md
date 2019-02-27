## Node Version Manager
[https://github.com/creationix/nvm](https://github.com/creationix/nvm)  
複数のバージョンのNode.jsを切り替えることができるbashスクリプト

### nvm-windows
[https://github.com/coreybutler/nvm-windows](https://github.com/coreybutler/nvm-windows)  
上記のWindows実装

Chocolateyでインストール
```ps1
choco install nvm -y
```

### usage

インストール確認
```ps1
nvm list
```

[https://nodejs.org/download/release/](https://nodejs.org/download/release/)でバージョンを確認する  
最近のものならば
```ps1
nvm list available
```
で確認できる。

インストール
```ps1
nvm install 6.16.0
```

確認
```ps1
nvm list
```

切り替え
```ps1
nvm use 6.16.0
```
