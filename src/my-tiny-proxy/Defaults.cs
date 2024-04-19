using Yarp.ReverseProxy.Configuration;

namespace MyTinyProxy;

public static class Defaults
{
    public static RouteConfig DefaultRouteConfig()
    {
        return new RouteConfig
        {
            RouteId = "defaultRoute",
            ClusterId = "defaultCluster",
            Match = new RouteMatch
            {
                Path = "{**catch-all}"
            }
        };
    }

    public static ClusterConfig DefaultClusterConfig(string destinationUri)
    {
        return new ClusterConfig
        {
            ClusterId = "defaultCluster",
            Destinations = new Dictionary<string, DestinationConfig>
            {
                {
                    "cluster1/destination", new DestinationConfig
                    {
                        Address = destinationUri
                    }
                }
            }
        };
    }
}