using System;

namespace DatingApp.Models
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set;}
        public DateTime CreatedAt { get; set; }
    }
}