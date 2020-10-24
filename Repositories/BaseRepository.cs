using JobOffersMVC.Models;
using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Repositories
{
    public abstract class BaseRepository<T> where T: BaseModel
    {
        private readonly string connectionString = @"Data Source=localhost;
Initial Catalog=JobOffersDB;
Integrated Security=True;
Connect Timeout=30;
Encrypt=False;
TrustServerCertificate=False;
ApplicationIntent=ReadWrite;
MultiSubnetFailover=False";

        protected SqlConnection connection;
        protected SqlCommand command;
        private string tableName;

        public BaseRepository(string tableName)
        {
            this.connection = new SqlConnection(this.connectionString);
            this.tableName = tableName;
        }

        public List<T> GetAll()
        {
            this.command = connection.CreateCommand();
            this.command.CommandText = $"Select * from {this.tableName}";

            this.connection.Open();

            List<T> items = new List<T>();

            try
            {
                IDataReader reader = this.command.ExecuteReader();
                while(reader.Read())
                {
                    T item = MapEntity(reader);
                    items.Add(item);
                }
                
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.connection.Close();
            }

            return items;
        }

        public T GetById(int id)
        {
            this.command = this.connection.CreateCommand();
            this.command.Parameters.Add(new SqlParameter("@ID", id));
            this.command.CommandText = $"Select * from {tableName} WHERE ID=@ID";

            this.connection.Open();
            try
            {
                IDataReader reader = this.command.ExecuteReader();
                reader.Read();
                return MapEntity(reader);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                this.connection.Close();
            }
        }

        private void Insert(T item)
        {
            this.command = this.connection.CreateCommand();
            BuildInsertParameters(item, this.command);
            this.command.CommandText = GetInsertCommandText();

            try
            {
                this.connection.Open();
                this.command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.connection.Close();
            }
        }

        private void Update(T item)
        {
            this.command = this.connection.CreateCommand();
            this.command.Parameters.Add(new SqlParameter("@ID", item.ID));
            BuildInsertParameters(item, this.command);

            this.command.CommandText = GetUpdateCommandText();

            try
            {
                this.connection.Open();
                this.command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.connection.Close();
            }
        }

        public void Save(T item)
        {
            if (item.ID != 0)
            {
                this.Update(item);
            }
            else
            {
                this.Insert(item);
            }
        }

        public void Delete(int id)
        {
            this.command = this.connection.CreateCommand();
            this.command.Parameters.Add(new SqlParameter("@ID", id));

            this.command.CommandText = $"DELETE FROM {tableName} WHERE ID=@ID";

            try
            {
                this.connection.Open();
                this.command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.connection.Close();
            }           
            
        }

        protected abstract T MapEntity(IDataReader reader);
        protected abstract void BuildInsertParameters(T item, SqlCommand command);
        protected abstract string GetInsertCommandText();

        protected abstract string GetUpdateCommandText();
    }
}
