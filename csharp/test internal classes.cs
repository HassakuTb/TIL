# internalクラスのテスト
テスト対象のアセンブリ内の適当なファイルに以下を書き加える。

```cs
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("NameOfTestAssembly")]
```
