using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mailtest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string to = "mirek.jandak@gmail.com";
        string from = "postmaster@mjandak.cz";
        string subject = "Using the new SMTP client.";
        string body = @"Using this new feature, you can send an e-mail message from an application very easily.";
        MailMessage message = new MailMessage(from, to, subject, body);
        SmtpClient client = new SmtpClient("mail.mjandak.cz");
        client.Credentials = new NetworkCredential("postmaster@mjandak.cz", ConfigurationManager.AppSettings["MailPwd"]);
        client.Timeout = 10000;
        client.Send(message);
    }
}
