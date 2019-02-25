## UnityでUnhandledExceptionをHandleする
.NETでは、`AppDomain.CurrentDomain.UnhandledException`イベントハンドラを用いてUnhandledExceptionを取り扱える。  
しかし、Unityは全例外をCatchしてるっぽくて例外が流れてこない。

### Application.RegisterLogCallback
`Application.RegisterLogCallback`を使うとExceptionの**ログ**をキャプチャできる。Exceptionそのものをキャプチャできるわけじゃないのがちょっと...
```cs
private void OnEnable()
{
    Application.logMessageReceived += HandleException;
}

private void OnDisable()
{
    Application.logMessageReceived -= HandleException;
}


private void HandleException(string condition, string stackTrace, LogType type)
{
    if (type == LogType.Exception)
    {
        //  handle exception
    }
}
```

### Debug.unityLogger
[https://docs.unity3d.com/ja/2017.4/ScriptReference/Logger.html](https://docs.unity3d.com/ja/2017.4/ScriptReference/Logger.html)
ILogHandlerを実装したLoggerを`Debug.unityLogger`に設定することでExceptionをキャプチャできる。  
デフォルトのロガーを書き換えてしまうので、デフォルト挙動を何とかしてやる必要がある。

```cs
class LogHandler : ILogHandler
{
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        HandleException(exception);
        
        //  log
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        //  log
    }
    
    private void HandleException(Exception e)
    {
        //  handle exception
    }
}

class Boot : MonoBehaviour
{
    void Awake()
    {
        debug.unityLogger = new Logger(new LogHandler());
    }
}     
```

ログの仕組みはログの仕組みなので例外処理に使うべきじゃないよなあ...
