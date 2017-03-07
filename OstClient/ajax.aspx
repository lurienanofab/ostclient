<%@ Page Language="C#" AutoEventWireup="true" Async="true" %>

<%@ Import Namespace="Newtonsoft.Json" %>
<%@ Import Namespace="OstClient.Models" %>
<%@ Import Namespace="OstClient.Services" %>

<script runat="server">
    void Page_Load(object sender, EventArgs e)
    {
        string command = (!string.IsNullOrEmpty(Request["command"])) ? Request["command"] : string.Empty;
        string ticketID = string.Empty;
        string source = string.Empty;
        string queue = string.Empty;
        string name = string.Empty;
        string email = string.Empty;
        string message = string.Empty;
        string topic = string.Empty;
        string location = string.Empty;
        string subject = string.Empty;
        string pri = string.Empty;
        string search = string.Empty;
        int[] resources = null;
        int resource_id = 0;
        string cc = string.Empty;

        try
        {
            var service = new TicketService();

            switch (command)
            {
                case "select-tickets-by-email":
                    RegisterAsyncTask(new PageAsyncTask(async () =>
                    {
                        email = GetRequestVar("email");
                        WriteJson(await service.SelectTicketsByEmail(email));
                    }));
                    break;
                case "select-tickets-by-resource":
                    RegisterAsyncTask(new PageAsyncTask(async () =>
                    {
                        resource_id = GetResourceID();
                        WriteJson(await service.SelectTicketsByResource(resource_id));
                    }));
                    break;
                case "ticket-detail":
                    RegisterAsyncTask(new PageAsyncTask(async () =>
                    {
                        ticketID = GetRequestVar("ticketID");
                        WriteJson(await service.SelectTicketDetail(ticketID));
                    }));
                    break;
                case "post-message":
                    RegisterAsyncTask(new PageAsyncTask(async () =>
                    {
                        ticketID = GetRequestVar("ticketID");
                        message = GetRequestVar("message");
                        WriteJson(await service.PostMessage(ticketID, message));
                    }));
                    break;
                case "add-ticket":
                    RegisterAsyncTask(new PageAsyncTask(async () =>
                    {
                        resource_id = GetResourceID();
                        email = GetRequestVar("email");
                        name = GetRequestVar("name");
                        queue = GetRequestVar("queue");
                        subject = GetRequestVar("subject");
                        message = GetRequestVar("message");
                        pri = GetRequestVar("pri");
                        search = GetRequestVar("search");
                        cc = GetCc();
                        WriteJson(await service.AddTicket(new AddTicketArgs()
                        {
                            ResourceID = resource_id,
                            Email = email,
                            Name = name,
                            Queue = queue,
                            Subject = subject,
                            Message = message,
                            Priority = pri,
                            Cc = cc,
                            Search = TicketSearchTypeUtil.Parse(search)
                        }));
                    }));
                    break;
                case "summary":
                    RegisterAsyncTask(new PageAsyncTask(async () =>
                    {
                        resources = GetResources();
                        WriteJson(await service.GetSummary(resources));
                    }));
                    break;
                case "dump-server-vars":
                    WriteJson(DumpServerVars());
                    break;
                default:
                    if (!string.IsNullOrEmpty(command))
                        throw new Exception(string.Format("Invalid command: {0}", command));
                    break;
            }
        }
        catch (Exception ex)
        {
            WriteJson(new { error = true, errno = 500, message = ex.Message }, 500);
        }
    }

    private string GetRequestVar(string key)
    {
        return (!string.IsNullOrEmpty(Request[key])) ? Request[key] : string.Empty;
    }

    private string GetCc()
    {
        string raw = GetRequestVar("cc");
        string[] splitter = raw.Split(',');
        string result = string.Join(",", splitter.Where(x => !string.IsNullOrEmpty(x)));
        return result;
    }

    private int GetResourceID()
    {
        string raw = GetRequestVar("resource_id");
        int result;
        if (int.TryParse(raw, out result))
            return result;
        else
            return 0;
    }

    private int[] GetResources()
    {
        string raw = GetRequestVar("resources");
        string[] splitter = raw.Split(',');
        return splitter.Select(x => int.Parse(x)).ToArray();
    }

    private string DumpServerVars()
    {
        Dictionary<string, string> serverVars = new Dictionary<string, string>();
        foreach (string key in Request.ServerVariables.AllKeys)
            serverVars.Add(key, Request.ServerVariables[key].ToString());
        string result = JsonConvert.SerializeObject(serverVars);
        return result;
    }

    private void WriteJson(object value, int statusCode = 200)
    {
        Response.Clear();
        Response.StatusCode = statusCode;
        Response.ContentType = "application/json";
        Response.Write(JsonConvert.SerializeObject(value));
        Response.End();
    }
</script>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OST Client Ajax Handler</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            This page exists for backwards compatibility. The new API should be used instead.
        </div>
    </form>
</body>
</html>
