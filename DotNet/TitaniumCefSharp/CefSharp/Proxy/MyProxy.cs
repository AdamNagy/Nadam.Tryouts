using System.Net;
using System.Threading.Tasks;

using Titanium.Web.Proxy;
using Titanium.Web.Proxy.EventArguments;
using Titanium.Web.Proxy.Models;

namespace CefSharp.WpfApp;

public class MyProxy
{
    private readonly ProxyServer proxyServer;

    private MyProxyEventHandler handler;

    public MyProxy()
    {
        handler = new MyProxyEventHandler("H:\\Documents\\MIV\\app_data\\Adam_Nagy\\Babes\\temp");
        handler.Init();

        proxyServer = new ProxyServer();

        proxyServer.ForwardToUpstreamGateway = true;

        var explicitEndPoint = new ExplicitProxyEndPoint(IPAddress.Any, 8000);

        proxyServer.AddEndPoint(explicitEndPoint);

        proxyServer.BeforeRequest += ProxyServer_BeforeRequest;
        proxyServer.BeforeResponse += ProxyServer_BeforeResponse;

        proxyServer.Start();
    }

    private void ProxyServer_BeforeTunnelConnectRequest(object sender, TunnelConnectSessionEventArgs e)
    {
        var hostname = e.HttpClient.Request.RequestUri.Host;
        if (hostname.EndsWith("webex.com"))
        {
            e.DecryptSsl = false;
        }
    }

    private async Task ProxyServer_BeforeRequest(object sender, SessionEventArgs e)
    {
        if (e.HttpClient.Request.HasBody)
        {
            e.HttpClient.Request.KeepBody = true;
            await e.GetRequestBody();
        }
    }

    private async Task ProxyServer_BeforeResponse(object sender, SessionEventArgs e)
    {
        if (e.HttpClient.Response.HasBody)
        {
            e.HttpClient.Response.KeepBody = true;
            await e.GetResponseBody();

            handler.Handle(e.HttpClient.Request, e.HttpClient.Response);
        }        
    }
}