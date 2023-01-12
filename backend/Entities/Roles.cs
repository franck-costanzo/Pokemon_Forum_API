using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.Entities
{
    public class Roles
    {

        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int role_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public List<Users> users { get; set; } = new List<Users>();

        #endregion

        #region Constructors

        public Roles(){}

        public Roles(int role_id, string name, string description)
        {
            this.role_id = role_id;
            this.name = name;
            this.description = description;
        }

        public Roles(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        #endregion
    }
}
