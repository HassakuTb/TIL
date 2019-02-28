## Firebase SDK for Unity から直接functionを呼び出す
公式ドキュメントは[https://firebase.google.com/docs/functions/callable?hl=ja](https://firebase.google.com/docs/functions/callable?hl=ja)  
`functions.https.onCall`を利用して呼び出し可能関数をindex.jsに記述してデプロイする。

### SDKのインストール
[https://firebase.google.com/docs/unity/setup](https://firebase.google.com/docs/unity/setup)からSDKをダウンロード  
UnityPackage形式なので通常通りimportを実行する。  
プロジェクトの設定からDLできるgoogle-services.jsonをAssets内の適当なフォルダに入れておく。

Unity側雛形  
```cs
FirebaseFunctions functions = FirebaseFunctions.GetInstance("asia-northeast1");

object data = new Dictionary<object, object>
{
    { "count", count },
};

functions.GetHttpsCallable("getTopEntries").CallAsync(data)
    .ContinueWith(task =>
    {
        var result = (Dictionary<object, object>)task.Result.Data;
        return ((List<object>)result["entries"])
            .Cast<Dictionary<object, object>>()
            .Select(e => new RankingEntry(
                name: (string)e["name"],
                score: Convert.ToInt32(e["score"])
            ));
    })
    .ContinueWith(task =>
    {
        if(task.IsFaulted)
        {
            //  handle task.Exception
        }
        else
        {
            //  completed
        }
    };
```
* <font color=Red>リージョンをデフォルトから変えている場合はリージョンを指定しないとダメ、DefaultInstanceでは動かない</font>
* jsのobject型はDictionary<object, object>としてデシリアライズされている
