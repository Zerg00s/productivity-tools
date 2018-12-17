import { sp } from "@pnp/sp";

sp.web.update({
    AlternateCssUrl: _spPageContextInfo.siteServerRelativeUrl + "/Style Library/global.css"
});
