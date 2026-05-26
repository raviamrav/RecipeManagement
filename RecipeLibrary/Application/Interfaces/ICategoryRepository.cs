using System;
using System.Collections.Generic;
using System.Text;
using RecipeLibrary.Domain.Entities;

namespace RecipeLibrary.Application.Interfaces
{
    public interface ICategoryRepository
    {
        void Add(Category category);

        void Update(Category category);

        List<Category> GetAll();

        Category? GetById(Guid id);

        void Delete(Category category);
    }
}
