using eShop.Database.Entities.Base;

namespace eShop.Database.Entities
{
    public class User : BaseEntity
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public int? Role { get; set; }
    }
}
