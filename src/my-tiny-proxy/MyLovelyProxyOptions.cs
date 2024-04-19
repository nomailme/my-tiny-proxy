namespace MyTinyProxy;

public record MyLovelyProxyOptions
{
    public string Certificate { get; set; } = "certificate.crt";
    public string CertificateKey { get; set; } = "certificate.key";
    public int Port { get; set; } = 5000;
    public string Destination { get; set; } = "https://google.com";
}