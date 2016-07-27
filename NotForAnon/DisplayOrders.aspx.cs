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

public partial class DisplayOrders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Orders";
        object UserId = Membership.GetUser().ProviderUserKey;

        using (SqlConnection Connection = new SqlConnection())
        {
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

            //vrat informace (OrderId, CreateDate) o vsech objednavkach zakaznika
            SqlCommand Querry = new SqlCommand();
            Querry.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;
            Querry.CommandText = @"
            SELECT a.OrderId, a.CreateDate, SUM(ProductPrice*ProductQuantity) AS Total, a.State
            FROM Orders AS a INNER JOIN OrdersGoods AS b ON a.OrderId = b.OrderId
            WHERE a.UserId = @UserId
            GROUP BY a.UserId, a.OrderId, a.CreateDate, a.State";
            Querry.Connection = Connection;

            Connection.Open();

            SqlDataReader reader = Querry.ExecuteReader();

            repOrders.DataSource = reader;
            repOrders.DataBind();
        }
    }
}
