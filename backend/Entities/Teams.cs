using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Pokemon_Forum_API.Entities
{
    public class Teams
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int team_id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public DateTime date_created { get; set; }
        public int user_id { get; set; }
    }
}
