namespace PokeGraphQL.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Nest;

    [Route("[controller]")]
    public class SearchController : Controller
    {
        private readonly IElasticClient elasticClient;

        public SearchController(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient;
        }

        [HttpGet("")]
        public async Task<IActionResult> Search(string query, int page = 1, int pageSize = 5)
        {
            return this.NoContent();
            var response = await this.elasticClient.SearchAsync<dynamic>(s => s.AllIndices().Query(q => q.QueryString(qs => qs.Query(query)))
                        .From((page - 1) * pageSize)
                        .Size(pageSize));

            if (!response.IsValid)
            {
                // We could handle errors here by checking response.OriginalException or response.ServerError properties
                return this.NoContent();
            }

            //if (page > 1)
            //    ViewData["prev"] = GetSearchUrl(query, page - 1, pageSize);
            //if (response.IsValid && response.Total > page * pageSize)
            //    ViewData["next"] = GetSearchUrl(query, page + 1, pageSize);

            return this.Ok(response.Documents);
        }
    }
}
