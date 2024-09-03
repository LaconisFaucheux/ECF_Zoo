using API_Arcadia.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace API_Arcadia.Services
{
    public class StatsService
    {
        private readonly IMongoCollection<AnimalStats> _animalsStatsCollection;
        private readonly IMongoCollection<HabitatStats> _habitatsStatsCollection;

        public StatsService(
            IOptions<StatsDatabaseSettings> statsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                statsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                statsDatabaseSettings.Value.DatabaseName);

            _animalsStatsCollection = mongoDatabase.GetCollection<AnimalStats>(
                statsDatabaseSettings.Value.AnimalsCollectionName);

            _habitatsStatsCollection = mongoDatabase.GetCollection<HabitatStats>(
                statsDatabaseSettings.Value.HabitatsCollectionName);
        }

        //ANIMALS
        public async Task<List<AnimalStats>> GetAnimalStatsAsync() =>
            await _animalsStatsCollection.Find(_ => true).SortByDescending(s => s.NbClics).ToListAsync();

        public async Task<byte> CreateAnimalStatsAsync(AnimalStats animalStat)
        {
            //Checking if a stat with this animal already exists or not
            var stat = await _animalsStatsCollection.Find(stat => stat.Name == animalStat.Name).FirstOrDefaultAsync();
            if(stat == null)
            {
                await _animalsStatsCollection.InsertOneAsync(animalStat);
                return 1;
            }
            else
            {
                return 0;
            }
        }                  

        public async Task UpdateAnimalStats(string id, AnimalStats statUpdate) =>
            await _animalsStatsCollection.ReplaceOneAsync(stat => stat.Id == id, statUpdate);

        public async Task DeleteAnimalStats(string id) =>
            await _animalsStatsCollection.DeleteOneAsync(stat => stat.Id == id);



        //HABITATS
        public async Task<List<HabitatStats>> GetHabitatStatsAsync() =>
            await _habitatsStatsCollection.Find(_ => true).SortByDescending(s => s.NbClics).ToListAsync();

        public async Task<byte> CreateHabitatStatsAsync(HabitatStats habitatStat)
        {
            var stat = await _habitatsStatsCollection.Find(hab => hab.Name == habitatStat.Name).FirstOrDefaultAsync();
            if(stat == null)
            {
                await _habitatsStatsCollection.InsertOneAsync(habitatStat);
                return 0;
            }
            else
            {
                return 1;
            }            
        }            

        public async Task UpdateHabitatStats(string id, HabitatStats statUpdate) =>
            await _habitatsStatsCollection.ReplaceOneAsync(stat => stat.Id == id, statUpdate);

        public async Task DeleteHabitatStats(string id) =>
            await _habitatsStatsCollection.DeleteOneAsync(stat => stat.Id == id);


    }
}
