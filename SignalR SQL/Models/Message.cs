using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SignalR_SQL.Models
{
    public class Message
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }

    public class MessageRepository
    {
        public IEnumerable<Message> GetData()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT [ID], [Date], [Message] FROM [dbo].[Messages]", connection))
                {
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += DependencyOnChange;

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new Message()
                            {
                                ID = x.GetInt32(0),
                                Date = x.GetDateTime(1),
                                Text = x.GetString(2)
                            }).ToList();
                }
            }
        }

        public void DependencyOnChange(object sender, SqlNotificationEventArgs e)
        {
            MessageHub.Show();
        }
    }
}