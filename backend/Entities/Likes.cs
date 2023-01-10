using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Pokemon_Forum_API.Entities
{
    public class Likes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int like_id { get; set; }
        public int post_id { get; set; }
        public int user_id { get; set; }

        public Posts post { get; set; }
        public Users user { get; set; }
    }

}
