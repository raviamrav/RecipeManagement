using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Repositories;

namespace RecipeLibrary.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Register(string name, string email)
        {
            ValidateUser(name, email);

            var existingUser = _userRepository.GetByEmail(email);
            if (existingUser != null)
            {
                throw new InvalidOperationException(
                    "Email is already registered"
                );
            }

            var user = new User
            {
                Name = name,
                Email = email
            };
            _userRepository.Add(user);
            return user;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        private void ValidateUser(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException("Name is required");
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ValidationException("Email is required");
            }
            if (!new EmailAddressAttribute().IsValid(email))
            {
                throw new ValidationException("Invalid email format");
            }
        }
    }
}
