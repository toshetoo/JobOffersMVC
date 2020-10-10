using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JobOffersMVC.Models;

namespace JobOffersMVC.Repositories
{
    public class UsersRepository
    {
        // CRUD 
        // Create
        // Read
        // Update
        // Delete
        private readonly string connectionString = @"Data Source=localhost;
Initial Catalog=JobOffersDB;
Integrated Security=True;
Connect Timeout=30;
Encrypt=False;
TrustServerCertificate=False;
ApplicationIntent=ReadWrite;
MultiSubnetFailover=False";

        private SqlConnection connection;
        private SqlCommand command;
        public UsersRepository()
        {
            this.connection = new SqlConnection(this.connectionString);
        }

        public List<User> GetAll()
        {
            this.command = connection.CreateCommand();
            this.command.CommandText = "Select * from Users";
            this.connection.Open();

            IDataReader reader = this.command.ExecuteReader();

            List<User> users = new List<User>();
            while (reader.Read())
            {
                User u = new User();
                u.ID = (int)reader["ID"];
                u.Username = reader["Username"].ToString();
                u.Email = reader["Email"].ToString();
                u.Password = reader["Password"].ToString();
                u.FirstName = reader["FirstName"].ToString();
                u.LastName = reader["LastName"].ToString();

                users.Add(u);
            }

            return users;
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

        public void Insert(User user)
        {

            this.command = this.connection.CreateCommand();
            command.Parameters.Add(new SqlParameter("@Username", user.Username));
            command.Parameters.Add(new SqlParameter("@Password", user.Password));
            command.Parameters.Add(new SqlParameter("@Email", user.Email));
            command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
            command.Parameters.Add(new SqlParameter("@LastName", user.LastName));

            command.CommandText
                = "INSERT INTO Users VALUES(@Username, @Password, @Email, @FirstName, @LastName)";

            connection.Open();
            command.ExecuteNonQuery();

            connection.Close();
        }

        public void Update(User user)
        {

        }

        public void Delete(User user)
        {

        }
    }
}
