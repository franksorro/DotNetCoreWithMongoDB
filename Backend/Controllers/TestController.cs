using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreWithMongoDB.Models;
using FS.Interfaces;
using MongoDB.Driver;
using DotNetCoreWithMongoDB.Middlewares;
using Microsoft.Extensions.Options;

namespace DotNetCoreWithMongoDB.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class TestController : BaseController
    {
        private readonly IMongoDBService service;
        private readonly AppSettings settings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public TestController(
            IMongoDBService service,
            IOptions<AppSettings> settings)
        {
            this.service = service;
            this.settings = settings.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await service.Get<TestModel>(settings.MongoDB.CollectionName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(TestModel model)
        {
            return Ok(await service.Insert(settings.MongoDB.CollectionName, model));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(TestModel model)
        {
            var filter = Builders<TestModel>.Filter.Where(f => f.Id == model.Id);
            return Ok(await service.Update(settings.MongoDB.CollectionName, filter, model));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            FilterDefinition<TestModel> filter = Builders<TestModel>.Filter.Empty;
            return Ok(await service.Delete(settings.MongoDB.CollectionName, filter));
        }
    }
}
