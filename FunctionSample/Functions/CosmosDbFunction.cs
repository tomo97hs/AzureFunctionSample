using FunctionSample.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionSample.Functions
{
    public class CosmosDbFunction
    {
        private readonly ICosmosDbService _cosmosDbService;
        private readonly ILogger<CosmosDbFunction> _logger;

        public CosmosDbFunction(ICosmosDbService cosmosDbService, ILogger<CosmosDbFunction> logger)
        {
            _cosmosDbService = cosmosDbService;
            _logger = logger;
        }

        [Function(nameof(CosmosDbFunction))]
        [CosmosDBOutput("%CosmosDbName%", "%CosmosCollectionOut%", ConnectionStringSetting = "CosmosDbConnection")]
        public object Run([CosmosDBTrigger(
            databaseName: "%CosmosDbName%",
            collectionName: "%CosmosCollectionIn%",
            ConnectionStringSetting = "CosmosDbConnection",
            LeaseCollectionName = "leases", CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<MyProfile> input, FunctionContext context)
        {
            if (input != null && input.Any())
            {
                _logger.LogInformation("Documents modified: " + input.Count);

                foreach (var document in input)
                {
                    _logger.LogInformation("Document Id: " + document.Id);
                }

                return _cosmosDbService.ProcessDocuments(input);
            }

            return null;
        }
    }

    public class MyProfile
    {
        public string Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int Sex { get; set; }

        public int Age { get; set; }

        public string Introduction { get; set; }
    }
}
