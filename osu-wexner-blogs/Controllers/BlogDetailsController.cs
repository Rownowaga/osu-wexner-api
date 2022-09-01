using Microsoft.AspNetCore.Mvc;
using osu_wexner_blogs.Model;
using osu_wexner_blogs.Services;

namespace osu_wexner_blogs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogDetailsController : ControllerBase
    {

        [HttpGet()]
        public ActionResult<List<BlogDetail>> GetAll()
        {
            return BlogDetailService.GetAll();
        }

        [HttpGet("{title}")]
        public ActionResult<List<BlogDetail>> GetByTitle(string title)
        { 
            return BlogDetailService.GetByTitle(title);
        }

        [HttpPost()]
        public IActionResult Create(BlogDetail blogDetail)
        {
            return Ok(BlogDetailService.Add(blogDetail));
        }

        [HttpPut()]
        public IActionResult Update(BlogDetail blogDetail)
        {
            BlogDetailService.Update(blogDetail);
            return Ok();
        }
    }
}