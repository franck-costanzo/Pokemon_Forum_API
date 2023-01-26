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
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime join_date { get; set; }
        public string avatar_url {get; set; }
        public bool isBanned { get; set; }
        public int role_id { get; set; }

        #endregion

        #region Constructor

        public Users(){}

        public Users(int user_id, string username, string password, string email, DateTime join_date, string avatar_url, int role_id, bool isBanned )
        {
            this.user_id = user_id;
            this.username = username;
            this.password = password;
            this.email = email;
            this.join_date = join_date;
            this.avatar_url = avatar_url;
            this.role_id = role_id;
            this.isBanned = isBanned;
        }

        public Users(string username, string email, DateTime join_date, int role_id, bool isBanned)
        {
            this.username = username;
            this.email = email;
            this.join_date = join_date;
            this.role_id = role_id;
            this.isBanned = isBanned;
        }

        public Users(int id, string username, string email)
        {
            this.user_id = id;
            this.username = username;
            this.email = email;
        }

        public Users(int id, string username, string email, DateTime join_date, string avatar_url, int role_id, bool isBanned) : this(id, username, email)
        {
            this.user_id = id;
            this.username = username;
            this.email = email;
            this.join_date = join_date;
            this.role_id = role_id;
            this.isBanned = isBanned;
        }

        public Users(int id, string avatar_url)
        {
            this.user_id = id;
            this.avatar_url = avatar_url;
        }

        #endregion

    }

}