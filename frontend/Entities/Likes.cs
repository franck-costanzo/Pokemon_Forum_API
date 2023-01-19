using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smogon_MAUIapp.Entities
{
    public class Likes
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int like_id { get; set; }
        public int post_id { get; set; }
        public int user_id { get; set; }

        public Posts post { get; set; }
        public Users user { get; set; }

        #endregion

        #region Constructor

        public Likes(int like_id, int post_id, int user_id)
        {
            this.like_id = like_id;
            this.post_id = post_id;
            this.user_id = user_id;
        }

        public Likes()
        {
        }

        public Likes(int post_id, int user_id)
        {
            this.post_id = post_id;
            this.user_id = user_id;
        }

        #endregion
    }

}
