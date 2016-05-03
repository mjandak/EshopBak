using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;
using System.Linq;
using System.Xml.Xsl;
using System.IO;
using System.Xml.Serialization;
using System.Xml;


public partial class CreateOrder : System.Web.UI.Page
{
    private string NewOrderId;

    protected void Page_Load(object sender, EventArgs e)
    {
        //ASP.NET pages are instances of the Page class, which inherits from the Control class, and that handle requests for files that have an .aspx extension.

        object UserId = Membership.GetUser().ProviderUserKey;

        Dictionary<string, CartItem>.ValueCollection CartItems = Profile.ShoppingCart.CartItems;

        if (CartItems.Count == 0) //je kosik prazdny?
        {
            EmptyCartMsg.Visible = true;
            OrderCreatedMsg.Visible = false;
        }
        else
        {
            using (SqlConnection Connection = new SqlConnection())
            {
                Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

                Connection.Open();

                SqlTransaction transaction = Connection.BeginTransaction();

                try
                {
                    //vytvoøí záznam o nové objednávce v tabulce Orders a vratí identifikátor této obejdnávky
                    SqlCommand Command = new SqlCommand();
                    Command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;
                    Command.Parameters.Add("@Total", SqlDbType.Decimal).Value = Profile.ShoppingCart.Total;
                    Command.CommandText = "DECLARE @NewOrderId int EXEC CreateOrder @UserId, @NewOrderId OUT SELECT @NewOrderId";
                    Command.Connection = Connection;
                    Command.Transaction = transaction;

                    NewOrderId = Command.ExecuteScalar().ToString();

                    Command.Parameters.Add("@NewOrderId", SqlDbType.Int);
                    Command.Parameters.Add("@ProductId", SqlDbType.Int);
                    Command.Parameters.Add("@ProductPrice", SqlDbType.Decimal);
                    Command.Parameters.Add("@ProductQuantity", SqlDbType.Int);

                    //pro každý produkt v košíku je vytvoøen záznam v tabulce OrdersGoods
                    foreach (CartItem Item in CartItems)
                    {
                        object ProductId = Item.Id;
                        int ProductQuantity = Item.Quantity;
                        decimal ProductPrice = Item.Price;
                        Command.Parameters["@NewOrderId"].Value = NewOrderId;
                        Command.Parameters["@ProductId"].Value = ProductId;
                        Command.Parameters["@ProductPrice"].Value = ProductPrice;
                        Command.Parameters["@ProductQuantity"].Value = ProductQuantity;
                        Command.CommandText = @"
                            INSERT INTO OrdersGoods (OrderId, ProductId, ProductPrice, ProductQuantity)
                            VALUES (@NewOrderId, @ProductId, @ProductPrice, @ProductQuantity)";
                        Command.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    string to = Membership.GetUser().Email;
                    string from = "admin@eshopbak.aspone.cz";
                    string subject = String.Format("Objednávka è. {0}", NewOrderId);

                    StringBuilder body = new StringBuilder();

                    //body.AppendFormat("Byla vytvoøena objednávka è. {0}", NewOrderId);
                    //body.AppendLine();
                    //body.AppendLine("Položky:");

                    //int col1length = Math.Max(CartItems.Max(c => c.Title.Length), "pøedmìt:".Length) + 10;
                    //body.Append("pøedmìt:".PadRight(col1length));
                    //body.Append("množství:".PadRight(20));
                    //body.AppendLine("cena:");
                    //foreach (CartItem item in CartItems)
                    //{
                    //    body.Append(item.Title.PadRight(col1length));
                    //    body.Append(item.Quantity.ToString().PadRight(20));
                    //    body.AppendLine(item.Price.ToString());
                    //}

                    body.Append("<p>Byla vytvoøena nová objednávka</p>");
                    body.AppendFormat("<p>Èíslo objednávky: {0}</p>", NewOrderId);
                    body.AppendLine("<p>Položky:</p>");
                    body.Append(@"<table style=""width:100%; border-collapse:collapse;"">");
                    body.AppendFormat(@"
<tr>
    <td style=""border:solid 1px black;padding:3px;"">pøedmìt:</td>
    <td style=""border:solid 1px black;padding:3px;"">množství:</td>
    <td style=""border:solid 1px black;padding:3px;"">cena:</td>
</tr>", "style=\"font-weight:bold\"");
                    foreach (CartItem item in CartItems)
                    {
                        body.Append("<tr>");
                        body.AppendFormat(@"<td style=""border:solid 1px black;padding:3px;"">{0}</td>", item.Title);
                        body.AppendFormat(@"<td style=""border:solid 1px black;padding:3px;"">{0}</td>", item.Quantity.ToString());
                        body.AppendFormat(@"<td style=""border:solid 1px black;padding:3px;"">{0}</td>", item.Price.ToString());
                        body.Append("</tr>");
                    }
                    body.AppendFormat(@"
<tr>
    <td style=""border:solid 1px black;padding:3px;"" colspan=""2"">cena celkem:</td>
    <td style=""border:solid 1px black;padding:3px;"">{0}</td>
</tr>", Profile.ShoppingCart.Total);
                    body.Append("<table>");



                    //XmlSerializer xs = new XmlSerializer(Profile.ShoppingCart.GetType());
                    //var xd = new XmlDocument();
                    //using (StringWriter swr = new StringWriter())
                    //{
                    //    xs.Serialize(swr, Profile.ShoppingCart);
                    //    xd.LoadXml(swr.ToString());
                    //}
                    //string body;
                    //XslCompiledTransform trans = new XslCompiledTransform();
                    //trans.Load("InfoOObjednavce.xslt");
                    //using (StringWriter swr2 = new StringWriter())
                    //{
                    //    trans.Transform(xd, null, swr2);
                    //    body = swr2.ToString();
                    //}

                    MailMessage message = new MailMessage(from, to, subject, body.ToString());
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("localhost");
                    client.Timeout = 10000;
                    client.Send(message);

                }
                catch (Exception)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception)
                    {
                        //Response.Redirect("~/Error.aspx");
                    }
                    //Response.Redirect("~/Error.aspx");
                    throw;
                }
            }

            hypNewOrderDetails.NavigateUrl = "~/NotForAnon/OrderDetails.aspx?OrderId=" + NewOrderId;
            OrderCreatedMsg.Visible = true;
            EmptyCartMsg.Visible = false;
        }
    }
}
