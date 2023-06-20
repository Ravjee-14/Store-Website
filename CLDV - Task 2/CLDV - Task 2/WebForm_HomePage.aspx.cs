using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Data;

namespace CLDV___Task_2
{
    public partial class WebForm_HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string photo = Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(Server.MapPath("~/" + photo));

            SqlConnection con = new SqlConnection("Data Source=ravjee;Initial Catalog=ABC_Supermarket;Integrated Security=True");

            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[Items]
                                                         ([Item_Image]
                                                         ,[Item_Name]
                                                         ,[Item_Description]
                                                         ,[Item_Price])
                                            VALUES('" + photo + "', '" + txtName.Text + "', '" + txtDescription.Text + "', " + double.Parse(txtPrice.Text) + ");", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Write("Data Successfully uploaded");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string photo = Path.GetFileName(FileUpload1.FileName);
                FileUpload1.SaveAs(Server.MapPath("~/" + photo));

                SqlConnection con = new SqlConnection("Data Source=ravjee;Initial Catalog=ABC_Supermarket;Integrated Security=True");

                SqlCommand cmd = new SqlCommand(@"UPDATE [dbo].[Items]
                                            SET [Item_Image] = '" + photo +
                                                "' ,[Item_Name] = '" + txtName.Text +
                                                "' ,[Item_Description] = '" + txtDescription.Text +
                                                "' ,[Item_Price] = " + double.Parse(txtPrice.Text) +
                                                "   WHERE Item_Name = '" + txtName.Text + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Response.Write("Data Updated Successfully");
            }
            catch(Exception)
            {
                Response.Write("Please make sure all fields are filled");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=ravjee;Initial Catalog=ABC_Supermarket;Integrated Security=True");

            SqlCommand cmd = new SqlCommand(@"DELETE FROM [dbo].[Items]
                                                WHERE Item_Name = '" + txtName.Text + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            Response.Write("Data Deleted Successfully");

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=ravjee;Initial Catalog=ABC_Supermarket;Integrated Security=True");

            con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM Items", con);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                txtName.Text = reader.GetValue(2).ToString();
                txtDescription.Text = reader.GetValue(3).ToString();
            }
            con.Close();
        }
    }
}