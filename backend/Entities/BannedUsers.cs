namespace Pokemon_Forum_API.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BannedUsers
    {
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
    }
}
