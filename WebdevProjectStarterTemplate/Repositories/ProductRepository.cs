using System.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using WebdevProjectStarterTemplate.Models;

namespace WebdevProjectStarterTemplate.Repositories;

[Authorize]
public class ProductRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }
    
    public IEnumerable<Product> GetProductWithCategorie()
    {
        string sql = @"    SELECT * 
                            FROM Product as P
                                JOIN Categorie as C ON P.CategorieId = C.CategorieId 
                            ORDER BY C.Name, P.Name";
            
        using var connection = GetConnection();
        var productsWithCategorie = connection.Query<Product, Categorie, Product>(
            sql, 
            map: (product, Categorie) =>
            {
                product.Categorie = Categorie;
                return product;
            }, 
            splitOn: "CategorieId"
        );
        return productsWithCategorie;
    }
}