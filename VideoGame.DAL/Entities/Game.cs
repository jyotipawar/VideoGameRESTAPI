using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoGame.DAL.Entities
{
    public class Game
    {
        public List<string> categories { get; set; }
        [Required]
        [Key]
        public string name { get; set; }
        public Publisher publisher { get; set; }
        public int releaseYear { set; get; }
        public int? players { get; set; }

    }

}
