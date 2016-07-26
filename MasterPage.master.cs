using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
  protected void Page_Load(object sender, EventArgs e)
  {
      switch (Page.Title)
      {
          case "Category Browser":
              ((HyperLink)LoginView1.FindControl("hypProductBrowser")).Enabled = false;
              break;
          case "Cart":
              ((HyperLink)LoginView1.FindControl("hypCart")).Enabled = false;
              break;
          case "Orders":
              ((HyperLink)LoginView1.FindControl("hypOrders")).Enabled = false;
              break;
          case "Profile":
              ((HyperLink)LoginView1.FindControl("hypProfile")).Enabled = false;
              break;
          case "Registration":
              ((HyperLink)LoginView1.FindControl("hypRegistration")).Enabled = false;
              ((System.Web.UI.WebControls.Login)LoginView1.FindControl("Login1")).DestinationPageUrl = "~/CtgrBrowser.aspx";
              break;
      }
  }
}
