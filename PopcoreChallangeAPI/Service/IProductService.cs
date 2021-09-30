using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopcoreChallangeAPI.Extension
{
    public interface IProductService
    {
        Task<List<Product>> GetProductByIngredient(string ingredient, int limit);
    }
}