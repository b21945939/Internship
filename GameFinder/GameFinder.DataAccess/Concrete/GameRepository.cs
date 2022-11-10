
using GameFinder.APIRequest;
using GameFinder.DataAccess.Abstract;
using GameFinder.Entities;
using MetacriticScraper.Interfaces;
using MetacriticScraper.Scraper;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameFinder.DataAccess.Concrete
{
    public class GameRepository : IGameRepository
    {
        public Game CreateGame(Game game)
        {
            using (var gameDbContext = new GameDbContext())
            {
                gameDbContext.Games.Add(game);
                gameDbContext.SaveChanges();
                return game;
            }
        }

        public void DeleteGame(int id)
        {
            using (var gameDbContext = new GameDbContext())
            {
                var deletedGame = GetGameById(id);
                gameDbContext.Games.Remove(deletedGame);
                gameDbContext.SaveChanges();
            }
        }

        public List<Game> GetAllGames()
        {
            using (var gameDbContext = new GameDbContext())
            {
                return gameDbContext.Games.ToList();
            }
        }

        public Game GetGameById(int id)
        {
            using (var gameDbContext = new GameDbContext())
            {
                return gameDbContext.Games.Find(id);
            }
        }

        public Game UpdateGame(Game game)
        {
            using (var gameDbContext = new GameDbContext())
            {
                List<Game> games = GetAllGames();
                Game oldGame = gameDbContext.Games.Find(game.Id);
                oldGame.Name = game.Name;
                oldGame.Platforms = game.Platforms;
                oldGame.Types = game.Types;
                gameDbContext.Games.Update(oldGame);
                gameDbContext.SaveChanges();
                return game;
            }
        }

        public Game AddType(string type, int id)
        {
            using (var gameDbContext = new GameDbContext())
            {
                var updatedGame = GetGameById(id);
                updatedGame.Types += ","+ type;
                gameDbContext.Games.Update(updatedGame);
                gameDbContext.SaveChanges();
                return updatedGame;
            }
        }
        public Game RemoveType(string type,int id)
        {
            using (var gameDbContext = new GameDbContext())
            {
                var updatedGame = GetGameById(id);
                string[] types = updatedGame.Types.Split(",");
                int remover = 0;
                for(int i=0;i<types.Length;i++)
                {
                    if (types[i].Equals(type))
                    {
                        remover = i;
                        break;
                    }
                }
                types[remover] = "";
                var result = String.Join(",", types.Where(s => !string.IsNullOrEmpty(s)));
                updatedGame.Types = result;
                gameDbContext.Games.Update(updatedGame);
                gameDbContext.SaveChanges();
                return updatedGame;
            }
        }

        public void RefreshRates()
        {
            using (var gameDbContext = new GameDbContext())
            {
                GameProcessor proc = new GameProcessor();
                List<Game> games = GetAllGames();
                foreach (Game i in games)
                {
                    Tuple<int, double> newScores = proc.LoadGame(i.Name).Result;
                    i.MetaScore = newScores.Item1;
                    i.UserScore = newScores.Item2;
                }
                gameDbContext.Games.UpdateRange(games);
                gameDbContext.SaveChanges();
            }
        }

        public Game AddPlatform(string platform, int id)
        {
            using (var gameDbContext = new GameDbContext())
            {
                var updatedGame = GetGameById(id);
                updatedGame.Platforms += "," + platform;
                gameDbContext.Games.Update(updatedGame);
                gameDbContext.SaveChanges();
                return updatedGame;
            }
        }

        public Game RemovePlatform(string platform, int id)
        {
            using (var gameDbContext = new GameDbContext())
            {
                var updatedGame = GetGameById(id);
                string[] platforms = updatedGame.Platforms.Split(",");
                int remover = 0;
                for (int i = 0; i < platforms.Length; i++)
                {
                    if (platforms[i].Equals(platform))
                    {
                        remover = i;
                        break;
                    }
                }
                platforms[remover] = "";
                var result = String.Join(",", platforms.Where(s => !string.IsNullOrEmpty(s)));
                updatedGame.Platforms = result;
                gameDbContext.Games.Update(updatedGame);
                gameDbContext.SaveChanges();
                return updatedGame;
            }
        }

        public Game UpdateImage(string image, int id)
        {
            using (var gameDbContext = new GameDbContext())
            {
                var updatedGame = GetGameById(id);
                updatedGame.Image = image;
                gameDbContext.Games.Update(updatedGame);
                gameDbContext.SaveChanges();
                return updatedGame;
            }
        }

        public Game UpdateVideo(string video, int id)
        {
            using (var gameDbContext = new GameDbContext())
            {
                var updatedGame = GetGameById(id);
                updatedGame.Video = video;
                gameDbContext.Games.Update(updatedGame);
                gameDbContext.SaveChanges();
                return updatedGame;
            }
        }
    }
}
