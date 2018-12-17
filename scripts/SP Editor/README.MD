# SP Editor. Samples and code snippets 

### Set Alternate CSS. Simple way
```cs
// Adding Alternate CSS to the web site:
import { sp } from "@pnp/sp";

sp.web.update({
    AlternateCssUrl: _spPageContextInfo.siteServerRelativeUrl + "/Style Library/global.css"
});

```

### Set Alternate CSS. Addition checks:
```js
// Same example as before, but with additional checks. 
// applying global.css file to the web.AlternateCssUrl using PnpJs:
import { sp } from "@pnp/sp";

async function Main() {

    let list = await sp.web.lists.getByTitle("Style Library");
    let items: any[] = await list.items.select("LinkFilename").getAll();
    items = items.map(item => item.LinkFilename);
    if (items.includes("global.css")) {
        await sp.web.update({
            AlternateCssUrl: _spPageContextInfo.siteServerRelativeUrl + "/Style Library/global.css"
        });
    } else {
        alert('global.css does not exist in the Style Library!');
    }
}

Main();
```

### Create a sample list of users and populates it with data
```js
// demonstrates the power of the SP Editor chrome extension
// creates a sample list of users and populates it with data

// warning: make sure this script is running when console is enabled.
// othwerwise, it might fail to create the data:
import { sp } from "@pnp/sp";

let lists = sp.web.lists;
let listName = "Users";
let list = sp.web.lists.getByTitle(listName);
async function Main() {

    let filteredLists: any[] = await lists.filter("Title eq '" + listName + "'").get();
    if (filteredLists.length > 0) {
        await list.delete();
        console.log(listName + " deleted");
    }
    await lists.add(listName);
    console.log(listName + " created");

    await list.fields.addNumber("Skill");
    await list.fields.addBoolean("Expert");
    await list.fields.addNumber("Grade");
    console.info("columns added");

    // display all columns in the defailt view:
    const view = list.defaultView;
    const batch = sp.web.createBatch();
    const fields = ['ID', 'LinkTitle', 'Skill', 'Expert', 'Grade', 'Created', 'Editor', 'Modified'];
    view.fields.inBatch(batch).removeAll();
    fields.forEach(fieldName => {
        view.fields.inBatch(batch).add(fieldName);
    });
    await batch.execute();
    console.log("defailt view modified");

    // get mock data from the mockaroo service:
    let response = await fetch("https://api.mockaroo.com/api/generate.json?key=fc5788f0&schema=Users");
    let users = await response.json();

    // add mock data to the list:
    let entityTypeFullName = await list.getListItemEntityTypeFullName();
    let itemsBatch = sp.web.createBatch();
    for (var i = 0; i < users.length; i++) {
        list.items.inBatch(itemsBatch).add(users[i], entityTypeFullName);
    }

    await itemsBatch.execute();
    console.log("items added!");
}

Main();

```