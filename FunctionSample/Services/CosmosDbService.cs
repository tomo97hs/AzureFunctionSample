using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionSample.Services
{
    public interface ICosmosDbService
    {
        object ProcessDocuments(IReadOnlyList<MyProfile> documents);
    }

    public class CosmosDbService : ICosmosDbService
    {
        public CosmosDbService()
        {

        }

        public object ProcessDocuments(IReadOnlyList<MyProfile> documents)
        {
            return documents.Select(x => new
            {
                id = x.Id,
                LastName = x.LastName,
                FirstName = x.FirstName,
                Sex = x.Sex,
                Age = x.Age + 10,
                Introduction = x.Introduction + "(Added 10 years)"
            });
        }
    }
}
