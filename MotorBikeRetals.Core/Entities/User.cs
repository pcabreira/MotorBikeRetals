using System;

namespace MotorBikeRetals.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string name, 
                    string password, 
                    string email, 
                    string role,
                    UserDetails details)
        {
            Name = name;
            Password = password;
            Email = email;
            CreatedAt = DateTime.Now;
            Active = true;
            Role = role;
            Details = details;
        }

        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; set; }
        public string Role { get; private set; }

        public UserDetails Details { get; set; }
    }
}
