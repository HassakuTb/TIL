## git submodule
* リポジトリ内で別のgitリポジトリの特定commitを参照する仕組み
* .gitmodulesファイルにサブモジュール側のコミットIDが記録される模様

## usage
まずはサブモジュール側のリポジトリを作成
```bash
git init
git commit --allow-empty -m "initial commit"
git remote add origin https://inner.git
git push -u origin master
```

メインモジュール側をつくる
```
git init
git commit --allow-empty -m "initial commit"
git submodule add https://inner.git Inner
git add .
git commit -m "add submodule Inner"
git remote add origin https://outer.git
git push -u origin master
```

サブモジュール側に変更がある場合
```bash
git submodule update
```


* 別のデバイスで作業する場合
```bash
git clone https://outer.git
git submodule init
git submodule update
```
