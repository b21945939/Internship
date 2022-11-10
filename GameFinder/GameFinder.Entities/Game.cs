using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameFinder.Entities
{
    public class Game
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get;set;}
        public string Name { get; set; }
        public string Types { get; set; }
        public string Platforms { get; set; }
        public int MetaScore { get; set; }
        public double UserScore { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
    }
}
