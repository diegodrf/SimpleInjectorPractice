using SimpleInjectorPractice.Utils;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SimpleInjectorPractice.Controllers
{
    [RoutePrefix("api/blog")]
    public class BlogController : ApiController
    {
        private readonly HttpClient _httpClient;
        private readonly IRequestMessageAccessor _requestMessageAcessor;

        public BlogController(HttpClient httpClient, IRequestMessageAccessor requestMessageAcessor)
        {
            _httpClient = httpClient;
            _requestMessageAcessor = requestMessageAcessor;
        }

        [Route("posts")]
        [HttpGet]
        public async Task<string> GetAllPosts()
        {
            var x = _requestMessageAcessor.CurrentMessage;
            var y = Request;
            var response = await _httpClient.GetAsync("posts");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
