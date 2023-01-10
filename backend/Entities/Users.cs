using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Threading;

namespace Pokemon_Forum_API.Entities
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime join_date { get; set; }

        public int role_id { get; set; }
        public Roles role { get; set; }
        public bool is_banned { get; set; }

        public List<Threads> threads { get; set; } = new List<Threads>();
        public List<Posts> posts { get; set; } = new List<Posts>();
        public List<Likes> likes { get; set; } = new List<Likes>();
        public List<BannedUsers> bannedusers { get; set; } = new List<BannedUsers>();
    }

}