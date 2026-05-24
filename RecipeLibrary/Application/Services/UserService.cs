using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Application.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();

        public User Registration(string name, string email)
        {
            var user = new User
            {
// Id = Guid.NewGuid(),
                Name = name,
                Email = email
            };

            _users.Add(user);

            return user;
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }
    }
}
