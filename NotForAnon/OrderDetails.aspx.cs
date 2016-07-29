using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class OrderDetails : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			string OrderId = Request.QueryString["OrderId"];
			object UserId = Membership.GetUser().ProviderUserKey;

			using (SqlConnection Connection = new SqlConnection())
			{
				Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

				//dva dotazy:
				//prvni vrati produkty na objednavce
				//druhy vrati datum odeslani a celkovou cenu objednavky
				SqlCommand Querry = new SqlCommand();
				Querry.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;
				Querry.Parameters.Add("@OrderId", SqlDbType.Int).Value = OrderId;
				Querry.CommandText = @"
					SELECT Orders.OrderId, Orders.UserId, OrdersGoods.ProductId, OrdersGoods.ProductQuantity, OrdersGoods.ProductPrice, Goods.title
					FROM Orders AS Orders
					INNER JOIN OrdersGoods AS OrdersGoods ON Orders.OrderId = OrdersGoods.OrderId
					INNER JOIN Goods AS Goods ON OrdersGoods.ProductId = Goods.id
					WHERE Orders.UserId = @UserId AND Orders.OrderId = @OrderId

					SELECT CreateDate, SUM(ProductPrice*ProductQuantity) AS Total, State
					FROM Orders AS a
					INNER JOIN OrdersGoods AS b ON a.OrderId = b.OrderId
					WHERE a.UserId = @UserId AND a.OrderId = @OrderId
					GROUP BY CreateDate, State";
				Querry.Connection = Connection;

				Connection.Open();

				SqlDataReader reader = Querry.ExecuteReader();

				if (reader.HasRows)
				{
					repOrderDetails.DataSource = reader;
					repOrderDetails.DataBind();

					reader.NextResult();
					reader.Read();

					litOrderId.Text = OrderId;
					litOrderCreated.Text = reader.GetDateTime(reader.GetOrdinal("CreateDate")).ToString();
					litSum.Text = reader.GetDecimal(reader.GetOrdinal("Total")).ToString();

					if (reader.GetString(reader.GetOrdinal("State")) == "zpracovává se") //jaky je stav objednavky?
					{
						hypOrderDelete.NavigateUrl = "OrderDelete.aspx?OrderId=" + OrderId;
					}
					else
					{
						hypOrderDelete.Visible = false;
					}

				}
				else
				{
					Label1.Text = "Objednávka è. " + OrderId + " byla zrušena nebo nikdy neexistovala.";
					divOrderDetails.Visible = false;
				}
			}
		}
	}
}

