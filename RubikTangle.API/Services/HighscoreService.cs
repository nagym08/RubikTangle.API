using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using RubikTangle.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubikTangle.API.Services
{
    public class HighscoreService : IHighscoreService
    {
        private readonly IConfiguration config;

        public HighscoreService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<Highscore>> GetAll()
        {
            //Appconfig infos should be handled by a secret manager in production
            CosmosClient client = new CosmosClient(config.GetValue<string>("CosmosDb:Endpoint"), config.GetValue<string>("CosmosDb:PrimaryKey"));
            var container = client.GetDatabase(config.GetValue<string>("CosmosDb:Database")).GetContainer(config.GetValue<string>("CosmosDb:Container"));

            var sqlQueryText = "SELECT * FROM c ORDER BY c.Steps";
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
            //Appconfig infos should be handled by a secret manager in production
            CosmosClient client = new CosmosClient(config.GetValue<string>("CosmosDb:Endpoint"), config.GetValue<string>("CosmosDb:PrimaryKey"));
            var container = client.GetDatabase(config.GetValue<string>("CosmosDb:Database")).GetContainer(config.GetValue<string>("CosmosDb:Container"));

            highscore.Id = Guid.NewGuid();
            var createdItem = await container.CreateItemAsync(highscore);
            return createdItem;
        }
    }
}
