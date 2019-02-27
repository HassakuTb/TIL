## Cloud Functionをデプロイする
* windows 10 Home
* PowerShell 5.1.17134.590
* chocolatey 0.10.11
* nvm 1.1.7
* node.js 6.16.0
* npm 6.1.0
* firebase-tools 6.4.0

基本的には[チュートリアル](https://firebase.google.com/docs/functions/get-started?hl=ja)に従う

### nodeのインストール
* Node.js/npmをインストール  
  * [nvm](https://github.com/HassakuTb/TIL/blob/master/nodejs/nvm.md)を利用すると良い  
  * nodeのバージョンはv6かv8  v6がデフォルトらしいのでv6にしておく
* firebase-toolsをnodeにインストール
  * `npm install -g firebase-tools`

### firebaseの初期化
ログインする
```ps1
firebase login
```
でブラウザが展開するのでgoogleアカウントでログインする

プロジェクトディレクトリを作成し、powershellのカレントをそこに移動する。

プロジェクトを初期化する
```ps1
firebase init functions
```
を実行する。実行するといろいろ聞かれる。
* 関連付けるFirebaseのプロジェクト
  * これはブラウザであらかじめプロジェクトを作成しておく
* 使用する言語
  * JavaScriptまたはTypeScript
  * 今回はJavaScriptを選択
  
git管理下に置く場合、このタイミングでリポジトリを作っておくと良さそう
