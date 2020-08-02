using System;

namespace DatingApp.Models
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
    }
}