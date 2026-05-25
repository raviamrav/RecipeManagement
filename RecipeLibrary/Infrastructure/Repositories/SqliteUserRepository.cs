using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;
using RecipeLibrary.Infrastructure.Persistence;

namespace RecipeLibrary.Infrastructure.Repositories
{
    public class SqliteUserRepository : IUserRepository
    {
        private readonly RecipeDbContext _dbContext;

        public SqliteUserRepository(RecipeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _dbContext.Users.ToList();
        }

        public User? GetById(Guid id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetByEmail(string email)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }
    }
}
