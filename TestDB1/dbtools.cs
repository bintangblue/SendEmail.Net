using System;
using System.Data;
using System.Data.SqlClient;

namespace TestDB1
{
    public class DBTools
    {
        SqlConnection con = new SqlConnection(
               "Server=DESKTOP-IM658HL;Database=rsys_survey;Trusted_Connection=True;"
               ); // connect to db

        public string menus()
        {
            Console.WriteLine("Pilih Menu (1.Read data | 2.Input Template| 3.Delete | 4. Send Email | 5.Input Email Order | 0. Exit)");
            var x = Console.ReadLine();
            return x;
        }

        public void insert(MTemplate param)
        {
            if (string.IsNullOrEmpty(param.type) || string.IsNullOrEmpty(param.template))
            {
                Console.WriteLine("data tidak boleh kosong!");
            }
            else
            {
                con.Open(); //execute db
                SqlCommand cmd = con.CreateCommand();
                cmd.Parameters.Add("@type", param.type);
                cmd.Parameters.Add("@template", param.template);
                cmd.CommandText = $"INSERT INTO [email_template] ([type],[template]) VALUES (@type ,@template)";
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public void insertOrder(MOrder param)
        {
            con.Open(); //execute db
            SqlCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("@target", param.em_target);
            cmd.Parameters.Add("@from", param.em_from);
            cmd.Parameters.Add("@type_id", param.type_id);
            cmd.Parameters.Add("@contents", param.contents);
            cmd.Parameters.Add("@status", param.status);
            cmd.CommandText = $"INSERT INTO [email_order] ([target],[from],[type_id],[contents],[status]) VALUES (@target ,@from,@type_id,@contents,@status)";
            cmd.ExecuteNonQuery();
            con.Close();
            
        }

        public void update(int id, string status)
        {
            con.Open(); //execute db
            SqlCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("@id", id);
            cmd.Parameters.Add("@status", status);
            cmd.CommandText = $"UPDATE [email_order] SET status = @status WHERE id = @id";
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public void delete(int id)
        {
            con.Open(); //execute db
            SqlCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("@id", id);
            cmd.CommandText = $"DELETE FROM [email_template] WHERE [id] =@id";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void showdata()
        {
            con.Open(); //execute db
            SqlCommand cmd = con.CreateCommand();

            cmd.CommandText = $"SELECT * FROM [email_template] ORDER BY  id ASC";
            var table = new DataTable();
            table.Load(cmd.ExecuteReader());
            var jum_row = table.Rows.Count; // jum row

            for (int i = 0; i < jum_row; i++){
                Console.WriteLine("Id : "+table.Rows[i]["id"] + ", Type : " + table.Rows[i]["type"] + ", Template : " + table.Rows[i]["template"]);
            }
            con.Close();
        }

        public MTemplate search(string id)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.Parameters.Add("@id", id);
            cmd.CommandText = $"SELECT * FROM [email_template] WHERE [id] = @id ORDER BY [id] ASC";
            var table = new DataTable();
            table.Load(cmd.ExecuteReader());
            var jum_row = table.Rows.Count; // jum row
            var res = new MTemplate { type=table.Rows[0]["type"].ToString(), template= table.Rows[0]["template"].ToString() };
            con.Close();
            return res;
        }

    }

}
