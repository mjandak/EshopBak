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

public partial class NotForAnon_Profile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Profile";
        if (!IsPostBack)
        {
            object UserId = Membership.GetUser().ProviderUserKey;

            using (SqlConnection Connection = new SqlConnection())
            {
                Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

                //tento dotaz vybere vsechny detaily o uzivateli z tabulky UserDetails
                SqlCommand Querry = new SqlCommand();
                Querry.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;
                Querry.CommandText = "SELECT * FROM UsersDetails WHERE UserId = @UserId";
                Querry.Connection = Connection;

                Connection.Open();

                SqlDataReader reader = Querry.ExecuteReader();

                //FormView1.DataSource = reader;
                //FormView1.DataBind();

                reader.Read();

                txtFirstName.Text = reader.GetString(reader.GetOrdinal("FirstName"));
                txtLastName.Text = reader.GetString(reader.GetOrdinal("LastName"));
                txtStreet.Text = reader.GetString(reader.GetOrdinal("Street"));
                txtCity.Text = reader.GetString(reader.GetOrdinal("City"));
                txtZipCode.Text = reader.GetString(reader.GetOrdinal("ZipCode"));

                txtEmail.Text = Membership.GetUser().Email;
            } 
        }
    }

  protected void LinkButton1_Click(object sender, EventArgs e)
  {
    object UserId = Membership.GetUser().ProviderUserKey;

    string FirstName = txtFirstName.Text;
    string LastName = txtLastName.Text;
    string Street = txtStreet.Text;
    string City = txtCity.Text;
    string ZipCode = txtZipCode.Text;
    string Email = txtEmail.Text;

    using (SqlConnection Connection = new SqlConnection())
    {
      Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

      //tento prikaz ulozi zmeny detalu uzivatele do tabulky UsersDetails
      SqlCommand Cmd = new SqlCommand();
      Cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = FirstName;
      Cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = LastName;
      Cmd.Parameters.Add("@Street", SqlDbType.NVarChar, 50).Value = Street;
      Cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = City;
      Cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 50).Value = ZipCode;
      Cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;
      Cmd.CommandText = "UPDATE UsersDetails SET FirstName = @FirstName, LastName = @LastName, Street = @Street, City = @City, ZipCode = @ZipCode WHERE UserId = @UserId";
      Cmd.Connection = Connection;

      Connection.Open();

      Cmd.ExecuteNonQuery();
    }

    MembershipUser CurrentUser = Membership.GetUser();
    CurrentUser.Email = Email;
    Membership.UpdateUser(CurrentUser);

  }
  //protected void FormView1_ItemUpdating(object sender, FormViewUpdateEventArgs e)
  //{
  //  ICollection c = e.NewValues.Values;
  //}
}