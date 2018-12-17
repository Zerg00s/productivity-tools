# [CamlJs console](https://chrome.google.com/webstore/detail/camljs-console/ohjcpmdjfihchfhkmimcbklhjdphoeac/related?hl=en). Samples and code snippets 

![](images/camljs-live-preview.png)

### Selecting items where skill > 60
```js
var query = new CamlBuilder().Where().NumberField("Skill").GreaterThan(60).ToString()
```

![](images/camljs-console-sample.png)


### Selecting all items where expert is true
```js
var query = new CamlBuilder().Where().BooleanField("Expert").EqualTo(true).ToString()
```
