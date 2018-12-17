# Cisar. Chrome extension. Samples and code snippets 

### Styling the list view form using JSLink and Cisar
```js
SP.SOD.executeFunc("clienttemplates.js", "SPClientTemplates", function() {

  function getBaseHtml(ctx) {
    return SPClientTemplates["_defaultTemplates"].Fields.default.all.all[ctx.CurrentFieldSchema.FieldType][ctx.BaseViewID](ctx);
  }

  function init() {

    SPClientTemplates.TemplateManager.RegisterTemplateOverrides({
      Templates: {
           Fields: {
              "Skill": {
                  View: function(ctx) { 
                    console.log(ctx.CurrentItem["Skill"] ); 
                    return ` <b>${ctx.CurrentItem["Skill"]} </b> `
                  
                  }, 
              }
           },

      },
      ListTemplateType: 100
    });
  }

  RegisterModuleInit(SPClientTemplates.Utility.ReplaceUrlTokens("~siteCollection/Style Library/Users.js"), init);
  init();

});

```
