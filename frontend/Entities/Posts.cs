using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smogon_MAUIapp.Entities
{
    public class Posts
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int post_id { get; set; }
        public string content { get; set; }
        public DateTime create_date { get; set; }
        public int thread_id { get; set; }
        public int user_id { get; set; }

        public Threads thread { get; set; }
        public Users user { get; set; }
        public List<Likes> likes { get; set; } = new List<Likes>();

        #endregion

        #region Constructor

        public Posts(int post_id, string content, DateTime create_date, int thread_id, int user_id)
        {
            this.post_id = post_id;
            this.content = content;
            this.create_date = create_date;
            this.thread_id = thread_id;
            this.user_id = user_id;
        }

        public Posts()
        {
        }

        public Posts(string content, DateTime create_date, int thread_id, int user_id)
        {
            this.content = content;
            this.create_date = create_date;
            this.thread_id = thread_id;
            this.user_id = user_id;
        }

        #endregion
    }
}
