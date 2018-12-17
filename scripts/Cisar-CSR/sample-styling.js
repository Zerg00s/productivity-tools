SP.SOD.executeFunc("clienttemplates.js", "SPClientTemplates", function () {

    function getBaseHtml(ctx) {
        return SPClientTemplates["_defaultTemplates"].Fields.default.all.all[ctx.CurrentFieldSchema.FieldType][ctx.BaseViewID](ctx);
    }
    function init() {
        SPClientTemplates.TemplateManager.RegisterTemplateOverrides({
            Templates: {
                Fields: {
                    "Skill": {
                        View: function (ctx) {
                            return `<div style='border:solid 1px grey;background-color:#0CC; padding:3px; width:${ctx.CurrentItem[ctx.CurrentFieldSchema.Name]}px'>${ctx.CurrentItem[ctx.CurrentFieldSchema.Name]}% </div> `
                        }
                    },
                    "Grade": {
                        View: function (ctx) {
                            //console.log(ctx.CurrentItem[ctx.CurrentFieldSchema.Name], ctx.CurrentFieldSchema.Name);
                          	
                            return `<div class='pieContainer'><div class='pieBackground'></div><div id='pieSlice${ctx.CurrentItem[ctx.CurrentFieldSchema.Name]}' class='hold'><div class='pie'></div></div><div class='innerCircle'><div class='content'>${ctx.CurrentItem[ctx.CurrentFieldSchema.Name]}</div></div></div> `
                        },
                    },
                    "Expert": {
                        View: function (ctx) {
                            //console.log(ctx.CurrentItem[ctx.CurrentFieldSchema.Name], ctx.CurrentFieldSchema.Name);
                          if (ctx.CurrentItem[ctx.CurrentFieldSchema.Name] === 'Yes'){
                            return `<input type='checkbox' checked disabled>`
                          }else{
                            return `<input type='checkbox' disabled>`
                          }

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
