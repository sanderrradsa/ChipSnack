using System.Data;
using Dapper;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories;

public class ProductRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }
    
    public IEnumerable<Product> GetProductWithCategory()
    {
        string sql = @"    SELECT * 
                            FROM Product as P
                                JOIN Category as C ON P.CategoryId = C.CategoryId 
                            ORDER BY C.Name, P.Name";
            
        using var connection = GetConnection();
        var productsWithCategory = connection.Query<Product, Category, Product>(
            sql, 
            map: (product, category) =>
            {
                product.Category = category;
                return product;
            }, 
            splitOn: "CategoryId"
        );
        return productsWithCategory;
    }
}