using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pokemon_Forum_API.Entities
{
    public class User_Moderates_SubForum
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UMSF_id { get; set; }
        public int user_id { get; set; }

        public int subforum_id { get; set; }

        public Users user { get; set; }

        public SubForums subforum { get; set; }

        #endregion

        #region Constructors

        public User_Moderates_SubForum(int UMSF_id, int user_id, int subforum_)
        {
            this.UMSF_id = UMSF_id;
            this.user_id = user_id;
            this.subforum_id = subforum_;
        }

        public User_Moderates_SubForum(int user_id, int subforum_)
        {
            this.user_id = user_id;
            this.subforum_id = subforum_;
        }

        #endregion
    }
}
