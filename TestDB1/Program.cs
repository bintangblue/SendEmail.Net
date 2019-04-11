using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Dapper;
using System.Linq;

namespace TestDB1
{
    class Program : DBTools
    {
        static void Main(string[] args)
        {

            DBTools dbcon = new DBTools();

            for(int io = 0; io < 1;)
            {
                var x = dbcon.menus();

                switch (x)
                {
                    case "0":
                        io++;
                        Console.WriteLine("bye Thanos");
                        Environment.Exit(0);
                        break;

                    case "1":
                        //Console.Clear();
                        Console.WriteLine("List Data :");
                        dbcon.showdata();
                        break;

                    case "2":
                        Console.WriteLine("Input Data :");
                        for(int xz = 0; xz < 6; xz++)
                        {
                            var data_ins = new MTemplate
                            {
                                type = "Register",
                                template = Faker.Lorem.Paragraph().ToString()
                            };
                            dbcon.insert(data_ins);
                        }
                        break;

                    case "3":
                        Console.WriteLine("Delete Data :");
                        Console.WriteLine("Id : ");
                        var delid = Console.ReadLine();
                        int delidnya = Convert.ToInt16(delid);
                        dbcon.delete(delidnya);
                        break;

                    case "7":
                        var apiKey = Environment.GetEnvironmentVariable("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");
                        var clienta = new SendGridClient(apiKey);
                        var from = new EmailAddress("test@example.com", "Example User");
                        var subject = "Sending with SendGrid is Fun";
                        var to = new EmailAddress("test@example.com", "Example User");
                        var plainTextContent = $"and easy to do anywhere, even with C#";
                        var htmlContent = $"<strong>and easy to do anywhere, even with C#</strong>";
                        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                        clienta.SendEmailAsync(msg);
                        break;

                    case "5":
                        Console.WriteLine("Insert Order :");
                        for (int u = 0; u < 10; u++)
                        {
                            var data_ord = new MOrder
                            {
                                em_from = Faker.User.Email().ToLower(),
                                em_target = Faker.User.Email().ToLower(),
                                type_id = Faker.Number.RandomNumber(5, 10).ToString(),
                                contents = Faker.Lorem.Paragraph().ToString(),
                                status = "PENDING"
                            };
                            dbcon.insertOrder(data_ord);
                        }
                        break;

                    case "4":
                        SendEmail se = new SendEmail();
                        se.Sender();
                        break;
                }
            }
            Console.ReadKey();
        }
    }

}
