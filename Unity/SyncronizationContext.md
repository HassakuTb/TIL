## Unityでasync関数を使う
基本的には以下の形でOK
```cs
SomeFunctionAsyncAsync().ContinueWith(task =>
{
    if (task.IsFaulted)
    {
        Debug.LogException(task.Exception);
    }
    else if(task.IsCancelled)
    {
        Debug.Log("cancelled");
    }
    else
    {
        Debug.Log("completed")
    }
}, TaskScheduler.FromCurrentSynchronizationContext());
```

`FromCurrentSynchronizationContext()`はWaitForFixedUpdateの後に待ち合わせをスケジュールする。
これを使ってメインスレッドに処理を戻してやらないとエラーログも吐かずにUnityEngineにかかわる処理が失敗する。

情報元[https://forum.unity.com/threads/continuations-and-synchronization-context.514727/](https://forum.unity.com/threads/continuations-and-synchronization-context.514727/)
