using GameFinder.DataAccess;
using GameFinder.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameFinder.Razor.MyFeature.Pages
{
    public class Page1Model : PageModel
    {
        public string games="Games List\n\n";
        public string images = "";
        public string videos = "";
        public void OnGet()
        {
            using (var gameDbContext = new GameDbContext())
            {
                List<Game> gameList = gameDbContext.Games.ToList();
                foreach(Game i in gameList)
                {
                    games += "Name: "+i.Name + "\n";
                    games += "Types: " + i.Types + "\n";
                    games += "Platforms: " + i.Platforms + "\n";
                    games += "MetaScore: " + i.MetaScore.ToString() + "\n";
                    games += "UserScore: " + i.UserScore.ToString() + "\n\n";
                    images += i.Image+" ";
                    videos += i.Video+ " ";
                }
            }
        }
    }
}
