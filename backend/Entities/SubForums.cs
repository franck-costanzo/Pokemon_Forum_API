using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.Entities
{
    public class SubForums
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int subforum_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int forum_id { get; set; }

        public Forums forum { get; set; }

        public List<Threads> threads { get; set; } = new List<Threads>();

        #endregion

        #region Constructor

        public SubForums() { }

        public SubForums(int subforum_id, string name, string description, int forum_id)
        {
            this.subforum_id = subforum_id;
            this.name = name;
            this.description = description;
            this.forum_id = forum_id;
        }

        #endregion
    }
}
