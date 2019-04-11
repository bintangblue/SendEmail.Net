using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Dapper;

namespace TestDB1
{
    public class SendEmail
    {
        SqlConnection con = new SqlConnection("Server=DESKTOP-IM658HL;Database=rsys_survey;Trusted_Connection=True;");

        public IEnumerable<MOrder> GetAll()
        {
            var q = from item in con.Query("SELECT * FROM [email_order] WHERE status != 'SEND' ORDER BY [id] ASC")
                    select new MOrder {
                        id = item.id,
                        contents = item.contents,
                        em_from = item.email_from,
                        em_target = item.email_target,
                        status = item.status,
                        type_id = item.type_id
                    };
            return q;
        }

        public void Sender()
        {
            DBTools tool = new DBTools();
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("85476fb664a30b", "01dc397d69fc86"),
                EnableSsl = true
            };
            var datas = GetAll();
            int jum = datas.Count();
            var data = datas.ToList();
            //var qa = datas.First();
            
                foreach (MOrder item in data)
                {
                    var q = from items in con.Query("SELECT * FROM [email_template] WHERE [id] = @ID", new {ID=item.type_id})
                            select new MTemplate
                            {
                                type = items.type,
                                template = items.template
                            };
                    var tm = q.First();
                    try
                    {
                        client.Send(item.em_from, //email from
                                            "bim-f43a00@inbox.mailtrap.io", //email target
                                            tm.type, //subject
                                            tm.template //content
                                            );
                        tool.update(Convert.ToInt16(item.id), "SEND");
                    Console.WriteLine( "Success");
                }
                catch(Exception err)
                    {
                    tool.update(Convert.ToInt16(item.id), "FAIL");
                    Console.WriteLine("Fail");
                }
                };
        }
    }

}
