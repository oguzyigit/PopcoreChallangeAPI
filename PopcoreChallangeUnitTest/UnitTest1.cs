using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using PopcoreChallangeAPI;
using PopcoreChallangeAPI.Controllers;
using PopcoreChallangeAPI.Extension;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PopcoreChallangeUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task GetProducts_Should_ReturnOk()
        {
            var mockService = new Mock<IProductService>(MockBehavior.Default);

            mockService.Setup(m => m.GetProductByIngredient("aa", 10)).ReturnsAsync(new List<Product>());
            var controller = new ProductsController(mockService.Object);
            var result = await Task.FromResult(controller.GetByParameter(ingredient: "aa", limit: 10));
            var okResult = result as OkObjectResult;

            NUnit.Framework.Assert.IsNotNull(okResult);
            NUnit.Framework.Assert.AreEqual(200, okResult.StatusCode);

        }

        [TestMethod]
        public async Task GetProducts_Should_ReturnRequest()
        {
            var mockService = new Mock<IProductService>(MockBehavior.Default);

            mockService.Setup(m => m.GetProductByIngredient("aa", 25)).ReturnsAsync(new List<Product>());
            var controller = new ProductsController(mockService.Object);
            var result = await Task.FromResult(controller.GetByParameter(ingredient: "aa", limit: 25));
            var nokResult = result as BadRequestObjectResult;

            NUnit.Framework.Assert.AreEqual(400, nokResult.StatusCode);

        }
    }
}
