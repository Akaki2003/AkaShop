$cert = New-SelfSignedCertificate -Type CodeSigningCert -Subject "CN=AkaShop API" -KeyAlgorithm RSA -KeyLength 2048 -CertStoreLocation "Cert:\CurrentUser\My" -NotAfter 01/01/2033
$pwd = ConvertTo-SecureString -String 'akashop123' -Force -AsPlainText
Export-PfxCertificate -cert $cert -FilePath  C:\Temp\akashopcert.pfx -Password $pwd
$cert