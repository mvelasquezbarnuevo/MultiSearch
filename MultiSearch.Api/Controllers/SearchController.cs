using MultiSearch.Api.Models;
using MultiSearch.Common.Contracts;
using MultiSearch.Common.Search;
using MultiSearch.Engine.Bing;
using MultiSearch.Engine.Google;
using MultiSearch.SearchingCore.Engines;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace MultiSearch.Api.Controllers
{
    [RoutePrefix("Search")]
    public class SearchController : ApiController
    {
        private readonly IEngineLoader _engineLoader;
        private ResultsFormatter _formatter = new ResultsFormatter();

        public SearchController(IEngineLoader engineLoader)
        {
            _engineLoader = engineLoader;
        }

        // GET api/values -- test
        [SwaggerOperation("GetAll")]
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // POST search/getdata
        [SwaggerOperation("GetData")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [Route("GetData")]
        [HttpPost]
        public async Task<IHttpActionResult> GetList([FromBody] InputParams data)
        {
            //loaded plugins via my personal nuget repo
            var plugins = new List<IPluginComponent>();
            plugins.Add(new GoogleEngine());
            plugins.Add(new MsnEngine());

            //send request to engineLoader
            var result = await _engineLoader.SendRequest(new SearchRequest { Criteria = data.Words.ToList() }, plugins);

            // fill data in requested format
            _formatter.Fill(result);
            
            // return Json result
            return Ok(new ApiResult
            {
                WinnerWord = _formatter.WinnerWord,
                SearchEngines = _formatter.SearchEngines
            });
        }
    }
}