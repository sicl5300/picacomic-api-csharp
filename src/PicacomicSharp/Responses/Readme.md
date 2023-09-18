## 响应命名规则

所有以`Response`结尾的，里面都只有一个属性，那个属性才是需要的数据。因此`PicaClient`不会返回它，而是会返回里面有用的内容。

反之同理，不以`Response`结尾的，都是有用的数据。

以`Result`结尾的，有点用，但是不是很重要。

## 命名空间

`Responses`：API直接返回的内容。

`Responses.Common`：可重用类，很多API的返回内容中都有同样的东西，因此提取出来了。

`Responses.Abstraction`：无需关注，用于标注。