using System.Collections;
using System.Data;
using Dapper;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories
{
    public class CategoryRepository
    {
        private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }

        public Category Get(int categoryId)
        {
            string sql = "SELECT * FROM Category WHERE CategoryId = @categoryId";
            
            using var connection = GetConnection();
            var category = connection.QuerySingle<Category>(sql, new { categoryId });
            return category;
        }

        public IEnumerable<Category> Get()
        {
            string sql = "SELECT * FROM Category ORDER BY Name";
            
            using var connection = GetConnection();
            var categories = connection.Query<Category>(sql);
            return categories;
        }

        
        
        public IEnumerable<Category> GetCategoriesWithProducts()
        {
            //het vullen van een lijst met categorieën is lastig met dapper (parent (Cagegory) with children (Products from Category))
            //ik heb idee van stackoverflow wat aangepast om het ook met LEFT JOIN te laten werken

            //https://stackoverflow.com/questions/6379155/multi-mapper-to-create-object-hierarchy#6380756
            //https://stackoverflow.com/questions/9350467/how-do-i-write-one-to-many-query-in-dapper-net
            
            string sql = @"SELECT * FROM 
                    Category as C LEFT JOIN Product 
                        as P ON C.CategoryId = P.CategoryId
                        
                    ORDER BY C.CategoryId, P.Name";
            
            using var connection = GetConnection();
            var categoryLookup = new Dictionary<int, Category>();
            var categories = connection.Query<Category, Product, Category>(sql, (category, product) =>
            {
                if(categoryLookup.TryGetValue(category.CategoryId, out var existingCategory))
                {
                    existingCategory.Products.Add(product);
                    return existingCategory;
                }
                else
                {
                    if (product is not null) //de LEFT JOIN zorgt voor null product (wanneer een category geen producten heeft)!
                    {                  
                        category.Products.Add(product);
                    }

                    categoryLookup.Add(category.CategoryId, category);
                    return category;
                }
            }, splitOn: "ProductId");

            //ophalen van de categorieën uit de categoryLookup
            var result = categoryLookup.Values.OrderBy(x => x.Name); 
            
            //sorteren van producten binnen een category, gebruik liever ORDER BY in de SQL query om de product te sorteren binnen een category
            // foreach (var category in result)
            // {
            //     category.Products = category.Products.OrderBy(x => x.Name).ToList();
            // }
            
            return result;
        }

        public Category Add(Category? category)
        {
            string sql = @"
                INSERT INTO Category (Name) 
                VALUES (@Name); 
                SELECT * FROM Category WHERE CategoryId = LAST_INSERT_ID()";
            
            using var connection = GetConnection();
            var addedCategory = connection.QuerySingle<Category>(sql, category);
            return addedCategory;
        }

        public bool Delete(int categoryId)
        {
            string sql = @"DELETE FROM Category WHERE CategoryId = @categoryId";
            
            using var connection = GetConnection();
            int numOfEffectedRows = connection.Execute(sql, new { categoryId });
            return numOfEffectedRows == 1;
        }

        public Category Update(Category category)
        {
            string sql = @"
                UPDATE Category SET 
                    Name = @Name 
                WHERE CategoryId = @CategoryId;
                SELECT * FROM Category WHERE CategoryId = @CategoryId";
            
            using var connection = GetConnection();
            var updatedCategory = connection.QuerySingle<Category>(sql, category);
            return updatedCategory;
        }
    }
}