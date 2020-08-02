using System;

namespace DatingApp.Models
{
    public class Photo : BaseEntity
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}