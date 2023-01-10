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
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime join_date { get; set; }
        public int role_id { get; set; }
        public bool isBanned { get; set; }

        public Users(){}
        public Users(int user_id, string username, string password, string email, DateTime join_date, int role_id, bool isBanned )
        {
            this.user_id = user_id;
            this.username = username;
            this.password = password;
            this.email = email;
            this.join_date = join_date;
            this.role_id = role_id;
            this.isBanned = isBanned;
        }

        
    }

}