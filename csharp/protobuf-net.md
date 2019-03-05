## protobuf-net
protobuf-netはProtocolBuffersのC#実装  
バイナリシリアライズする場合はこれを選択するのが良さそう  
爆速らしい

ライセンスは Apache License 2.0

### usage
シリアライズしたいクラスにProtoContractAttributeを付加する。  
メンバにProtoMemberを付加する。  
デフォルトコンストラクタを用意する。
```cs
/// <summary>
/// 単語
/// </summary>
[ProtoContract]
public class Word
{
    /// <summary>
    /// かな表記
    /// </summary>
    [ProtoMember(1)]
    public string Kana { get; private set; }

    /// <summary>
    /// 表示文字列
    /// ひとつのかな表記に対して複数の表記を含むのでListになっている
    /// </summary>
    [ProtoMember(2)]
    public List<string> DisplayWords { get; private set; }

    //  protobuf用
    private Word() { }

    /// <summary>
    /// 単語を構築する
    /// </summary>
    /// <param name="kana">かな表記</param>
    /// <param name="displayStrings">表記リスト</param>
    public Word(string kana, IEnumerable<string> displayStrings)
    {
        Kana = kana;
        DisplayWords = displayStrings.ToList();
    }
}

/// <summary>
/// 単語辞書
/// </summary>
[ProtoContract]
public class WordDictionary
{
    [ProtoMember(1)]
    private Dictionary<string, Word> dictionary;

    //  protobuf用
    private WordDictionary() { }

    /// <summary>
    /// 辞書を構築する
    /// </summary>
    /// <param name="words">登録する単語リスト</param>
    public WordDictionary(IEnumerable<Word> words)
    {
        dictionary = words.ToDictionary(keySelector: w => w.Kana);
    }
}
```

シリアライズとデシリアライズ
```cs
/// <summary>
/// Dictionaryをローカルに保存する
/// </summary>
/// <param name="dictionary">保存対象の辞書</param>
/// <param name="filePath">保存先</param>
/// <returns></returns>
public static async Task SaveDictionaryAsync(WordDictionary dictionary, string filePath)
{
    await Task.Run(() =>
    {
        using (FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
        {
            Serializer.Serialize(fs, dictionary);
        }
    }).ConfigureAwait(false);
}

/// <summary>
/// Dictionaryをローカルからロードする
/// 対象ファイルが存在しないとき、nullが返却される
/// </summary>
/// <param name="filePath"></param>
/// <returns></returns>
public static async Task<WordDictionary> LoadDictionaryAsync(string filePath)
{
    return await Task.Run(() =>
    {
        if (!File.Exists(filePath)) return null;

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            return Serializer.Deserialize<WordDictionary>(fs);
        }
    }).ConfigureAwait(false);
}
```
