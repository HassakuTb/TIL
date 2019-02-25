## マルチバイト文字を扱うInputField for Windows
* Unity 2017.4.5f1
* Windows Standalone / Unity Editor

InputFieldでマルチバイト文字を入力中(確定せずに)フィールド外をクリックするとKeyDownイベントが2重に発生するバグがある。  
これの対応。

### 対応方法
InputFieldの拡張でOnUpdateSelectedをオーバーライドする。  
入力中にクリックするとOnUpdateSelectedのタイミングでMouseDownイベントとKeyDownイベントが複数流れて来るので、
KeyDownイベントを半分スキップする。

```csusing System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// マルチバイト入力で入力中にクリックでフォーカスを外すと倍化する現象を抑制する
/// </summary>
public class MultiByteInputField : InputField
{
    private static readonly IEnumerable<KeyCode> allowedKeyCodes = new List<KeyCode>
    {
        KeyCode.A,
        KeyCode.B,
        KeyCode.C,
        KeyCode.D,
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z,
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
        KeyCode.Keypad0,
        KeyCode.Keypad1,
        KeyCode.Keypad2,
        KeyCode.Keypad3,
        KeyCode.Keypad4,
        KeyCode.Keypad5,
        KeyCode.Keypad6,
        KeyCode.Keypad7,
        KeyCode.Keypad8,
        KeyCode.Keypad9,
    };
        
    //  選択されたUIの更新処理
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        List<Event> events = new List<Event>();

        Event e = new Event();
        while (Event.PopEvent(e))
        {
            //  扱いにくいので一度リストにコピーする
            events.Add(new Event(e));
        }

        //  文字イベントの数を数える
        int keyDownListCount = events
            .Count(x => x.rawType == EventType.KeyDown && x.character != '\0');

        //  通常モード条件
        if(!events.Any(x => x.rawType == EventType.MouseDown)   //  マウスダウンが無い
            || keyDownListCount == 0        //  KeyDownのイベントが無い
            || keyDownListCount % 2 != 0)   //  KeyDownのイベントが奇数
        {
            //  通常モード
            ProcessEvents(events);
        }
        else
        {
            //  倍化モード
            
            List<Event> eventsForRepeat = new List<Event>();    //  倍化モード用イベントリスト

            int keydownIndex = -1;
            Debug.Log("Repeat Mode");
            foreach (Event ev in events)
            {
                if(ev.rawType == EventType.KeyDown && ev.character != '\0')
                {
                    //  後ろ半分をスキップ
                    keydownIndex++;
                    if (keydownIndex >= keyDownListCount / 2)
                    {
                        Debug.Log("skipped" + ev.ToString());
                        continue;
                    }
                }
                eventsForRepeat.Add(ev);
            }

            ProcessEvents(eventsForRepeat);
        }

        //  見た目の更新
        UpdateLabel();

        //  イベントを使用済みにする
        eventData.Use();
    }

    /// <summary>
    /// イベントリストを処理する
    /// </summary>
    /// <param name="events"></param>
    private void ProcessEvents(IEnumerable<Event> events)
    {
        foreach (Event ev in events)
        {
            //  KeyUpを無視する(Ctrl＋Vとかで2倍コピーされるのを防止)
            //  ShiftやCtrlなどのKeyUpは無視できないので無視できるものをホワイトリストにしておいて
            //  ホワイトリストのKeyCodeのKeyUpイベントを無視するようにする
            if (ev.rawType == EventType.KeyUp && allowedKeyCodes.Contains(ev.keyCode)) continue;

            //  ベースクラスに処理を投げる
            ProcessEvent(ev);
        }
    }
}
```
