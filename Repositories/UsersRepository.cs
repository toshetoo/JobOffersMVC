using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Models;

namespace JobOffersMVC.Repositories
{
    public class UsersRepository: BaseRepository<User>
    {
        public UsersRepository(): base("Users")
        {
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            this.command = this.connection.CreateCommand();

            this.command.Parameters.Add(new SqlParameter("@Username", username));
            this.command.Parameters.Add(new SqlParameter("@Password", password));
            this.command.CommandText = "Select * from Users where Username=@Username and Password=@Password";

            this.connection.Open();
            IDataReader reader = this.command.ExecuteReader();

            User u = new User();

            try
            {
                reader.Read();
                u.ID = (int) reader["ID"];
                u.Username = reader["Username"].ToString();
                u.Email = reader["Email"].ToString();
                u.Password = reader["Password"].ToString();
                u.FirstName = reader["FirstName"].ToString();
                u.LastName = reader["LastName"].ToString();

                return u;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
           
        }

        protected override void BuildInsertParameters(User item, SqlCommand command)
        {
            command.Parameters.Add(new SqlParameter("@Username", item.Username));
            command.Parameters.Add(new SqlParameter("@Password", item.Password));
            command.Parameters.Add(new SqlParameter("@Email", item.Email));
            command.Parameters.Add(new SqlParameter("@FirstName", item.FirstName));
            command.Parameters.Add(new SqlParameter("@LastName", item.LastName));
        }

        protected override string GetInsertCommandText()
        {
            return "INSERT INTO Users VALUES(@Username, @Password, @Email, @FirstName, @LastName)";
        }

        protected override string GetUpdateCommandText()
        {
            return "UPDATE Users SET Username=@Username, Password=@Password, Email=@Email, FirstName=@FirstName, LastName=@LastName where ID=@ID";
        }

        protected override User MapEntity(IDataReader reader)
        {
            User u = new User();
            u.ID = (int)reader["ID"];
            u.Username = reader["Username"].ToString();
            u.Email = reader["Email"].ToString();
            u.Password = reader["Password"].ToString();
            u.FirstName = reader["FirstName"].ToString();
            u.LastName = reader["LastName"].ToString();

            return u;
        }
    }
}
