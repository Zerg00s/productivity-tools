# SPPP + PnP PowerShell Demo

This sample project was generated using [generator-sppp](https://github.com/koltyakov/generator-sppp)


---

## Prerequisites to using [Pnp PowerShell](https://github.com/SharePoint/PnP-PowerShell)
Run one of the following PowerShell commands depending on your SharePoint version:

| SharePoint version | PowerShell command to run |
| ------ | ------ |
| SharePoint Online | `Install-Module SharePointPnPPowerShellOnline` |
| SharePoint 2016 | `Install-Module SharePointPnPPowerShell2016` |
| SharePoint 2013 | `Install-Module SharePointPnPPowerShell2013` |

It will allow you to run `DEPLOY_ARTIFACTS.bat` deployment batch file.

---

### Deployment steps:
Create a new team site. Example URL: `https://portal.sharepoint.com/TestDeploy`

```sh
git clone https://github.com/Zerg00s/customHome.git
```
navigate to the PowerShell subfolder and run this command to deploy custom home page:
```sh
DEPLOY_ARTIFACTS.bat
```
When asked to enter site URL - enter team site's URL. Example: `https://portal.sharepoint.com/TestDeploy`

install gulp:
```sh
npm install gulp -g
```

After customizations were deployed - run `npm install` to install all dev dependencies:
```sh
npm install
```

then type `gulp config` to configure connection to SharePoint:
```sh
gulp config
```
Enter your site URL, for example `https://portal.sharepoint.com/TestDeploy`, your login and password

To start watching local changes type:

```sh
gulp watch
```

Now, any time you make any changes in the `\distr` or `\src` folder - the file will be automatically uploaded to SharePoint

---