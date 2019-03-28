# Sequenceの途中に処理を挟む

`Sequence.AppendCallback()`で処理を挟める  
`Sequence.AppendInterval()`でウェイトを入れることもできる

```cs
FadeSequence = DOTween.Sequence();

//  フェードイン
FadeSequence.Append(group.DOFade(transparency, fadeInDuration).SetEase(Ease.Linear));
FadeSequence.AppendCallback(() => IsShowKeeping = true);

//  表示状態で維持
FadeSequence.AppendInterval(activeDuration);
FadeSequence.AppendCallback(() => IsShowKeeping = false);

//  フェードアウト
FadeSequence.Append(group.DOFade(0f, fadeOutDuration).SetEase(Ease.Linear));
FadeSequence.AppendCallback(() => IsShowing = false);
```
