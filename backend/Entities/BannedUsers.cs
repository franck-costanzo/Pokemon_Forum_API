namespace Pokemon_Forum_API.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BannedUsers
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int banned_user_id { get; set; }
        public int user_id { get; set; }
        public int banned_by_user_id { get; set; }
        public DateTime ban_start_date { get; set; }
        public DateTime ban_end_date { get; set; }
        public string reason { get; set; }

        public Users user { get; set; }
        public Users bannedbyuser { get; set; }

        #endregion

        #region Constructor

        public BannedUsers() {}

        public BannedUsers(int banned_user_id, int user_id, int banned_by_user_id, DateTime ban_start_date, DateTime ban_end_date, string reason)
        {
            this.banned_user_id = banned_user_id;
            this.user_id = user_id;
            this.banned_by_user_id = banned_by_user_id;
            this.ban_start_date = ban_start_date;
            this.ban_end_date = ban_end_date;
            this.reason = reason;
        }

        public BannedUsers(int user_id, int banned_by_user_id, DateTime ban_start_date, DateTime ban_end_date, string reason)
        {
            this.user_id = user_id;
            this.banned_by_user_id = banned_by_user_id;
            this.ban_start_date = ban_start_date;
            this.ban_end_date = ban_end_date;
            this.reason = reason;
        }

        #endregion
    }
}
