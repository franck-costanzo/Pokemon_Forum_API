﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Pokemon_Forum_API.Entities
{
    public class Threads
    {
        private int userId;

        public Threads(int thread_id, string title, DateTime create_date, DateTime last_post_date, int userId, int? forum_id, int? subforum_id)
        {
            this.thread_id = thread_id;
            this.title = title;
            this.create_date = create_date;
            this.last_post_date = last_post_date;
            this.userId = userId;
            this.forum_id = forum_id;
            this.subforum_id = subforum_id;
        }

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
