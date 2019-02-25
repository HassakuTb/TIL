## ZeroFormatter

[https://github.com/neuecc/ZeroFormatter](https://github.com/neuecc/ZeroFormatter)
爆速(らしい)シリアライザ  
必要になるまでパースを先送りする戦略で速度を実現している  
バージョニングやDictionary、LookUpテーブル実装があるのが素敵

以下公式からの引用
> ```csharp
> [ZeroFormattable]
> public class MyClass
> {
>     [Index(0)]
>     public virtual ILazyDictionary<int, int> LazyDictionary { get; set; }
> 
>     [Index(1)]
>     public virtual ILazyLookup<int, int> LazyLookup { get; set; }
> }
> 
> // there properties can set from `AsLazy***` extension methods. 
> 
> var mc = new MyClass();
> 
> mc.LazyDictionary = Enumerable.Range(1, 10).ToDictionary(x => x).AsLazyDictionary();
> mc.LazyLookup = Enumerable.Range(1, 10).ToLookup(x => x).AsLazyLookup();
> ```
