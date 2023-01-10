using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Pokemon_Forum_API.Entities
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int post_id { get; set; }
        public string content { get; set; }
        public DateTime create_date { get; set; }
        public int thread_id { get; set; }
        public int user_id { get; set; }

        public Threads thread { get; set; }
        public Users user { get; set; }
        public List<Likes> likes { get; set; } = new List<Likes>();
    }
}
