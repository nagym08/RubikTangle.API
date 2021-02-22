using Microsoft.Azure.Cosmos;
using RubikTangle.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubikTangle.API.Services
{
    public class HighscoreService : IHighscoreService
    {
        public async Task<List<Highscore>> GetAll()
        {
            string endpointUrl = "https://miklos-nagy.documents.azure.com:443/";
            string primaryKey = "Ri7pQaf1BdsyS6LsM14pEUJlmVgz2QCDj8KPFKh20yaRXT60BQfel9P2Dg5QnuVVC9EgdZyNZQfGZHxPHPw4bg==";

            CosmosClient client = new CosmosClient(endpointUrl, primaryKey);
            var container = client.GetDatabase("NeuroDiab").GetContainer("Highscores");

            var sqlQueryText = "SELECT * FROM c";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Highscore> queryResultSetIterator = container.GetItemQueryIterator<Highscore>(queryDefinition);

            var highscores = new List<Highscore>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Highscore> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Highscore highscore in currentResultSet)
                {
                    highscores.Add(highscore);
                }
            }

            return highscores;
        }

        public async Task<Highscore> Save(Highscore highscore)
        {
            string endpointUrl = "https://miklos-nagy.documents.azure.com:443/";
            string primaryKey = "Ri7pQaf1BdsyS6LsM14pEUJlmVgz2QCDj8KPFKh20yaRXT60BQfel9P2Dg5QnuVVC9EgdZyNZQfGZHxPHPw4bg==";

            CosmosClient client = new CosmosClient(endpointUrl, primaryKey);
            var container = client.GetDatabase("NeuroDiab").GetContainer("Highscores");

            highscore.Id = Guid.NewGuid();
            var createdItem = await container.CreateItemAsync(highscore);
            return createdItem;
        }
    }
}
