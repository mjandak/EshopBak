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
using System.Data.SqlClient;

public partial class NotForAnon_OrderDelete : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string OrderId = Request.QueryString["OrderId"];
        object UserId = Membership.GetUser().ProviderUserKey;

        using (SqlConnection Connection = new SqlConnection())
        {
            Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

            SqlCommand Querry = new SqlCommand();
            Querry.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;
            Querry.Parameters.Add("@OrderId", SqlDbType.Int).Value = OrderId;
            Querry.CommandText = "DECLARE @Result bit EXEC DeleteOrder @OrderId, @UserId, @Result OUT SELECT @Result AS Result";
            Querry.Connection = Connection;

            Connection.Open();

            SqlDataReader reader = Querry.ExecuteReader();

            reader.Read();
            bool result = reader.GetBoolean(reader.GetOrdinal("Result"));
            if (result)
            {
                lblMessage.Text = "Objednávka byla zrušena.";
            }
            else
            {
                lblMessage.Text = "Objednávka nebyla zrušena. Buï neexistuje nebo je již vyøízena";
            }

        }
    }
}
