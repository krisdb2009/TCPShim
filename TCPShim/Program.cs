using System.Net;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/{IPAddress}/{Port:int}", async (Context) => {
    try
    {
        TcpClient tcpClient = new TcpClient();
        await tcpClient.ConnectAsync(new(IPAddress.Parse((string)Context.Request.RouteValues["IPAddress"]), int.Parse((string)Context.Request.RouteValues["Port"])));
        await Context.Request.Body.CopyToAsync(tcpClient.GetStream());
        tcpClient.Close();
    }
    catch (Exception e)
    {
        await Context.Response.WriteAsync(e.Message);
    }
});

app.Run();