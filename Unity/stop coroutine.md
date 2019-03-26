# StopCoroutineでコルーチンを止めて再開

再開するためにはCoroutineオブジェクトを再度取り直す
```cs
Coroutine routine = StartCoroutine(SomeCoroutine());
StopCoroutine(routine);
routine = StartCoroutine(SomeCoroutine());
```
