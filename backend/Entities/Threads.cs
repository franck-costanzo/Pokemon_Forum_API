using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Pokemon_Forum_API.Entities
{
    public class Threads
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int thread_id { get; set; }
        public string title { get; set; }
        public DateTime create_date { get; set; }
        public DateTime last_post_date { get; set; }

        public int user_id { get; set; }
        public Users user { get; set; }

        public int? forum_id { get; set; }
        public Forums forum { get; set; }

        public int? subforum_id { get; set; }
        public SubForums subforum { get; set; }

        public List<Posts> posts { get; set; } = new List<Posts>();
    }
}
