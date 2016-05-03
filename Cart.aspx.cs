using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Cart";
        if (!IsPostBack)
        {
          if (Profile.ShoppingCart.CartItems.Count == 0) //kdyz je kosik prazdny
          {
            litCartIsEmptyMessage.Visible = true;
            LoginView1.FindControl("hypCreateOrder").Visible = false;
            LoginView1.FindControl("lblOrderCreatePermission").Visible = false;
          }
          else
          {
            repCart.DataSource = Profile.ShoppingCart.CartItems;
            repCart.DataBind();
            litCartIsEmptyMessage.Visible = false;
          }
        }
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    foreach (RepeaterItem item in repCart.Items)
    //    {
    //        bool Checked = ((CheckBox)item.FindControl("chckToDelete")).Checked;
    //        string ProductId = ((Literal)item.FindControl("litProductId")).Text;
    //        if (Checked)
    //        {
    //            Profile.ShoppingCart.RemoveItem(ProductId);
    //        }
    //    }

    //    repCart.DataSource = Profile.ShoppingCart.CartItems;
    //    repCart.DataBind();
    //}
    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            foreach (RepeaterItem item in repCart.Items)
            {
                string ProductId = ((HyperLink)item.FindControl("litProductId")).Text;
                int ProductQuantity = Convert.ToInt32(((TextBox)item.FindControl("txtProductQuantity")).Text);
                Profile.ShoppingCart.ItemQuantityChange(ProductId, ProductQuantity);
            }

            if (Profile.ShoppingCart.CartItems.Count == 0) //kdyz je kosik prazdny
            {
                litCartIsEmptyMessage.Visible = true;
                LoginView1.FindControl("hypCreateOrder").Visible = false;
                LoginView1.FindControl("lblOrderCreatePermission").Visible = false;
            }
            else
            {
                repCart.DataSource = Profile.ShoppingCart.CartItems;
                repCart.DataBind();
                litCartIsEmptyMessage.Visible = false;
            }
        }        
    }

    protected void btnEmptyCart_Click(object sender, EventArgs e)
    {
        Profile.ShoppingCart.Empty();
        litCartIsEmptyMessage.Visible = true;
        LoginView1.FindControl("hypCreateOrder").Visible = false;
        LoginView1.FindControl("lblOrderCreatePermission").Visible = false;
        repCart.Visible = false;
    }

    protected void Page_PreRender()
    {
        if (Session.IsNewSession)
        {
            HttpCookie c = FormsAuthentication.GetAuthCookie("p", false);
            Response.Cookies["_xa"].Expires = c.Expires;
        }
    }
}
