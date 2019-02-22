## gitによる改行コード自動変換
git for Windowsは改行コードを自動変換するため、差分が無限に出続けることがある。

以下のようにgitconfigにgitで改行コードを制御しないようにする
```bash
git config --global core.autoCRLF false
```
ちなみにcore.autoCRLFのデフォルト値はtrueでchocorateyとかで入れるとtrueになるので注意する。
