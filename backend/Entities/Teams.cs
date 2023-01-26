using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Pokemon_Forum_API.Entities
{
    public class Teams
    {
        #region Properties 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int team_id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public DateTime date_created { get; set; }
        public int user_id { get; set; }

        #endregion

        #region Constructors

        public Teams(int team_id, string name, string link, DateTime date_created, int user_id)
        {
            this.team_id = team_id;
            this.name = name;
            this.link = link;
            this.date_created = date_created;
            this.user_id = user_id;
        }

        public Teams(string name, string link, DateTime now, int user_id)
        {
            this.name = name;
            this.link = link;
            this.date_created = now;
            this.user_id = user_id;
        }

        #endregion

    }
}
