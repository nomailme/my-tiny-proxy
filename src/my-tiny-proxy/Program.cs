using System.Net;
using MyTinyProxy;
using Yarp.ReverseProxy.Configuration;

var builder =
#if IS_NATIVE_AOT
    WebApplication.CreateSlimBuilder(args);
#else
    WebApplication.CreateBuilder(args);
#endif

var proxyOptions = new MyLovelyProxyOptions();
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.Bind(proxyOptions);

var certificateLoader = TlsCertificateLoader.FromPemFile(proxyOptions.Certificate, proxyOptions.CertificateKey);


builder.WebHost.ConfigureKestrel(x =>
{
    
    x.Listen(IPAddress.Any, proxyOptions.Port, listenOptions =>
    {
        listenOptions.UseHttps(certificateLoader.Certificate, httpsOptions =>
        {
            httpsOptions.ServerCertificate = certificateLoader.Certificate;
            httpsOptions.ServerCertificateChain = certificateLoader.Chain;
        });
    });
});

RouteConfig[] routes = [Defaults.DefaultRouteConfig()];
ClusterConfig[] clusters = [Defaults.DefaultClusterConfig(proxyOptions.Destination)];

builder.Services.AddReverseProxy()
    .LoadFromMemory(routes, clusters);

var app = builder.Build();

app.MapReverseProxy();
app.UseRouting();
app.Run();