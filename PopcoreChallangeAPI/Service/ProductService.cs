using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopcoreChallangeAPI.Extension
{
    public class ProductService : IProductService
    {
        private readonly IRestClient _restClient;

        public ProductService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<List<Product>> GetProductByIngredient(string ingredient, int limit)
        {
            var request = new RestRequest("search.pl")
               .AddParameter("action", "process")
               .AddParameter("tagtype_0", "ingeridents")
               .AddParameter("tagcontaions_0", "contains")
               .AddParameter("tag_0", ingredient)
               .AddParameter("page_size", limit)
               .AddParameter("tag_0", ingredient)
               .AddParameter("json", true);

            var resp = await _restClient.GetAsync<dynamic>(request);

            List<Product> products = new List<Product>();

            for (int i = 0; i < resp["products"].Count; i++)
            {
                Product product = new Product();
                product.Name = resp["products"][i]["product_name"];
                product.Ingredients = new List<string>();

                for (int j = 0; j < resp["products"][i]["ingredients"].Count; j++)
                {
                    product.Ingredients.Add(resp["products"][i]["ingredients"][j]["text"].ToString());
                }

                products.Add(product);
            }

            return products;
        }
    }
}
