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

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Registration";
    }
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
      try
      {
        //string UserName = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("UserName")).Text;
        object UserId = Membership.GetUser(CreateUserWizard1.UserName).ProviderUserKey;
        string FirstName = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("FirstName")).Text;
        string LastName = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("LastName")).Text;
        string Street = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Street")).Text;
        string City = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("City")).Text;
        string ZipCode = ((TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("ZipCode")).Text;

        using (SqlConnection Connection = new SqlConnection())
        {
          Connection.ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

          SqlCommand Cmd = new SqlCommand();
          Cmd.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = UserId;
          Cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = FirstName;
          Cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = LastName;
          Cmd.Parameters.Add("@Street", SqlDbType.NVarChar, 50).Value = Street;
          Cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = City;
          Cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 50).Value = ZipCode;
          Cmd.CommandText = "INSERT INTO UsersDetails (UserId, FirstName, LastName, Street, City, ZipCode) VALUES (@UserId, @FirstName, @LastName, @Street, @City, @ZipCode)";
          //Querry.CommandText = "EXEC AddUserDetails '" + UserName + "', '" + FirstName + "', '" + LastName + "', '" + Street + "', '" + City + "', '" + ZipCode + "'";
          Cmd.Connection = Connection;

          Connection.Open();

          Cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
          Membership.DeleteUser(CreateUserWizard1.UserName, true);
          Response.Redirect("~/Error.aspx");
      }
    }

    protected void CreateUserWizard1_CreateUserError(object sender, CreateUserErrorEventArgs e)
    {
    }
}
