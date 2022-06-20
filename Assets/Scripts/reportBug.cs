using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;

public class reportBug : MonoBehaviour {
    public Text bugBox;
    public void sendBug()
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress("reportbugemail@gmail.com");
        mail.To.Add("reportbugemail@gmail.com");
        mail.Subject = "BUGS!!";
        mail.Body = bugBox.text;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = (ICredentialsByHost) new NetworkCredential("reportbugemail@gmail.com", "m4t7AB$Ya");
        smtpServer.EnableSsl = true;
        smtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        smtpServer.Send(mail);
    }
}
