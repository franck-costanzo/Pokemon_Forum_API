using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.Entities
{
    public class SubForums
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int subforum_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int forum_id { get; set; }

        public Forums forum { get; set; }

        public List<Threads> threads { get; set; } = new List<Threads>();
    }
}
