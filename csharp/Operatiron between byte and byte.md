# C#におけるbyte演算
32bit演算しかできないので(そもそもCPU的には32bitしか演算できないので)int型として演算される。  
ちなみにintは32bit
```cs
byte a;
byte b;
var r = a +b;
```
これは
```cs
byte a;
byte b;
int r = (int)a + (int)b 
```
