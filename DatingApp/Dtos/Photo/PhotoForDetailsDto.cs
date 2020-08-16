using System;

namespace DatingApp.Dtos.Photo
{
    public class PhotoForDetailsDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }

        public string Description { get; set; }
        public bool IsMain { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}