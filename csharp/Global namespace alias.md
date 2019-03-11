## グローバル名前空間エイリアス
```cs
namespace Hoge.Fuga
{
  ...
}

namespace Fuga
{
  ...
  class Piyo {}
}
```

なとき、`Hoge.Fuga`名前空間内で`Piyo`を参照るには、
`global::Fuga.Piyo`とする。
