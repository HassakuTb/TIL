## スクリプトからUIナビゲーションを行う

### フォーカスする
```cs
EventSystem.current.SetSelectedGameObject(targetGameObject);
```

### ナビゲーション
現在選択中のGameObjectのSelectbleコンポーネントからFindSelectableOnXXX()で次のオブジェクトを探し、
SetSelectedGameObjectで遷移先のGameObjectを指定する。
```cs
Selectable selectable = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
Selectable next = selectable.FindSelectableOnDown();
EventSystem.current.SetSelectedGameObject(next.gameObject);
```
これをTabキーを押したときとかに設定してやればよい。
