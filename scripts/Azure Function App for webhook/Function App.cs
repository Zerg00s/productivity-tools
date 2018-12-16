#r "Newtonsoft.Json"
#r "Microsoft.WindowsAzure.Storage"
#r "Microsoft.SharePoint.Client.Runtime.dll" 
#r "Microsoft.SharePoint.Client.dll"

using System;
using System.Net;
using Newtonsoft.Json;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Utilities;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
public static async Task<object> Run(HttpRequestMessage req, ILogger log)
{
    //log.LogInformation($"Webhook was triggered!");
    //await ConnectToSharePointAndSendEmail(log);
    string validationToken = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "validationtoken", true) == 0).Value; 
    string message = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "message", true) == 0).Value;

    // This is just for registration:
    if (validationToken != null)
    {
        await ConnectToSharePointAndAddListItem(log, "webhook registered! " + message);
        log.LogInformation($"Validation token {validationToken} received");
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Content = new StringContent(validationToken);
        return response;
    }
    // this is for normal runs:
    var content = await req.Content.ReadAsStringAsync();
    log.LogInformation($"Received payload: {content}");
    await ConnectToSharePointAndAddListItem(log, message);
    return new HttpResponseMessage(HttpStatusCode.OK);
}
public static async Task<bool> ConnectToSharePointAndAddListItem(ILogger log, string message)
{
    string password = GetEnvironmentVariable("Password");
    string login = GetEnvironmentVariable("Login");
    System.Security.SecureString securePassword = new System.Security.SecureString();
    foreach (char pass in password)
    {
        securePassword.AppendChar(pass);
    }
    SharePointOnlineCredentials spOnlineCredentials = new SharePointOnlineCredentials(login, securePassword);
    try
    {
        log.LogInformation($"Conneciton to SharePoint...");
        using (var SPClientContext = new ClientContext("https://zergs.sharepoint.com/sites/demo"))
        {
            log.LogInformation($"[Success] Connected to SharePoint");
            SPClientContext.Credentials = spOnlineCredentials;
            Web spWeb = SPClientContext.Site.RootWeb;
            List spList = spWeb.Lists.GetByTitle("AzureLog");
            SPClientContext.Load(spList);
            SPClientContext.ExecuteQuery();           
            log.LogInformation($"[Success] spList items: {spList.Title}");

            
            ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
            ListItem oListItem = spList.AddItem(itemCreateInfo);
            oListItem["Title"] = message + " | " + DateTime.UtcNow.ToLongTimeString();

            oListItem.Update();
            SPClientContext.ExecuteQuery();


        }
    }
    catch (Exception ex)
    {
        log.LogInformation($"Azure Function Exception: {ex.Message}");
    }
    return true;
}


public static string GetEnvironmentVariable(string name)
{
    return  System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
}