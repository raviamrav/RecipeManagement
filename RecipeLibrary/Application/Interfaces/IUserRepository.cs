using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Application.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        List<User> GetAll();
        User? GetById(Guid id);
        User? GetByEmail(string email);
    }
}
