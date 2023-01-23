namespace Smogon_MAUIapp.Entities
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

        public List<Posts> posts { get; set; }

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

        public Users(int id, string username, string email, DateTime join_date, int role_id, bool isBanned) : this(id, username, email)
        {
            this.user_id = id;
            this.username = username;
            this.email = email;
            this.join_date = join_date;
            this.role_id = role_id;
            this.isBanned = isBanned;
        }

        public Users(int id, string username, string email, 
                        DateTime join_date, int role_id, bool isBanned, List<Posts> posts)
        {
            this.user_id = id;
            this.username = username;
            this.email = email;
            this.join_date = join_date;
            this.role_id = role_id;
            this.isBanned = isBanned;
            this.posts = posts;
        }

    }

}