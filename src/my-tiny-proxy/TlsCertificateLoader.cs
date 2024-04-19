using System.Security.Cryptography.X509Certificates;

namespace MyTinyProxy;

public class TlsCertificateLoader
{
    private TlsCertificateLoader(X509Certificate2 certificate, X509Certificate2Collection chain)
    {
        Certificate = certificate;
        Chain = chain;
    }

    public X509Certificate2 Certificate { get; }

    public X509Certificate2Collection Chain { get; }

    public static TlsCertificateLoader FromPemFile(string certificatePath, string keyPath)
    {
        var pemCertificate = X509Certificate2.CreateFromPemFile(certificatePath, keyPath);
        var tlsCertificate = new X509Certificate2(pemCertificate.Export(X509ContentType.Pkcs12));
        var chain = new X509Certificate2Collection();
        chain.ImportFromPemFile(certificatePath);
        return new TlsCertificateLoader(tlsCertificate, chain);
    }
}