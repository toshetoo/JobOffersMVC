using JobOffersMVC.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Repositories
{
    public class JobOffersRepository: BaseRepository<JobOffer>
    {
        public JobOffersRepository(): base("JobOffers")
        {
        }

        protected override JobOffer MapEntity(IDataReader reader)
        {
            JobOffer offer = new JobOffer()
            {
                ID = (int)reader["ID"],
                CreatorId = (int)reader["CreatorId"],
                Title = reader["Title"].ToString(),
                Description = reader["Description"].ToString()
            };

            return offer;
        }

        protected override void BuildInsertParameters(JobOffer item, SqlCommand command)
        {
            command.Parameters.Add(new SqlParameter("@Title", item.Title));
            command.Parameters.Add(new SqlParameter("@Description", item.Description));
            command.Parameters.Add(new SqlParameter("@CreatorId", item.CreatorId));
        }

        protected override string GetInsertCommandText()
        {
            return "INSERT INTO JobOffers VALUES(@Title, @Description, @CreatorId)";
        }

        protected override string GetUpdateCommandText()
        {
            return "UPDATE JobOffers SET Title=@Title, Description=@Description, CreatorId=@CreatorId WHERE ID=@ID";
        }
    }
}
