using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoGame.DAL.Entities;
using VideoGame.DAL.Models;
namespace VideoGame.DAL.Repository
{
    public class GamesRepository : IGamesRepository
    {
        private string jsonFile = @"..\VideoGame.DAL\Data\data.json";

        GamesDto allGames = new GamesDto();
        string jsonGames = string.Empty;

        public GamesRepository()
        {
            jsonGames = File.ReadAllText(jsonFile);
            allGames = JsonConvert.DeserializeObject<GamesDto>(jsonGames);
        }

        //get all games in the json 
        public List<Game> GetAll()
        {
            return allGames.games;
        }

        public Game GetGame(string name)
        {
           var objGame = allGames.games
                    .Where(g => g.name == name).FirstOrDefault();

            return objGame;

        }

        //Get specific category game and order by name or year
        public List<Game> GetSpecCatGameOrderby(string categorName, string orderby)
        {
            switch (orderby)
            {
                case "name":
                    return allGames.games
                    .Where(g => g.categories.Any(c => c == categorName))
                    .OrderBy(g => g.name).ToList();

                case "name dsc":

                    return allGames.games
                        .Where(g => g.categories.Any(c => c == categorName))
                         .OrderByDescending(g => g.name).ToList();

                case "year":

                    return allGames.games
                    .Where(g => g.categories.Any(c => c == categorName))
                    .OrderBy(g => g.releaseYear).ToList();

                case "year dsc":

                    return allGames.games
                            .Where(g => g.categories.Any(c => c == categorName))
                    .OrderByDescending(g => g.releaseYear).ToList();

                default:
                    return allGames.games
                            .Where(g => g.categories.Any(c => c == categorName)).ToList();
            }
        }

        public List<Game> GetSpecCatGame(string categorName)
        {
            var specCatGame = allGames.games.Where(g => g.categories
            .Any(c => c == categorName)).ToList();
            return specCatGame;
        }

        //Get all games order by name or releaseYear
        public List<Game> GetGamesOrderby(string orderby)
        {
            switch (orderby)
            {
                case "name":
                    return allGames.games
                    .OrderBy(g => g.name).ToList();

                case "name dsc":
                    return allGames.games
                    .OrderByDescending(g => g.name).ToList();

                case "year":
                    return allGames.games
                    .OrderBy(g => g.releaseYear).ToList();
                case "year dsc":
                    return allGames.games
                   .OrderByDescending(g => g.releaseYear).ToList();
                default:
                    return allGames.games;
            }
        }

        //Add new game
        public Game Create(Game objGame)
        {
            //Game objGame = JsonConvert.DeserializeObject<Game>(jsonGame);
            allGames.games.Add(objGame);
            return objGame;
        }

        //update game by name
        public Game Update(Game objGame)
        {
            var removeGame = allGames.games.First(g => g.name == objGame.name); //name acts as key here so never change game name 
            allGames.games.Remove(removeGame);
            allGames.games.Add(objGame);
            return objGame;
        }

        //delete game by name
        public void Delete(Game objGame)
        {
            allGames.games.Remove(objGame);
        }

    }
}
