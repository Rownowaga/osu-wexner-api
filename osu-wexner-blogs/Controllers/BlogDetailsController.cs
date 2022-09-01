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

        [HttpGet("{topic}")]
        public ActionResult<List<BlogDetail>> GetByTitle(string topic)
        { 
            return BlogDetailService.GetByTopic(topic);
        }

        [HttpPost()]
        public IActionResult Create(BlogDetail blogDetail)
        {
            string pkey = BlogDetailService.Add(blogDetail);
            if (pkey != null && pkey.Length > 4)
                return Ok(pkey);
            else
                return BadRequest();
        }

        [HttpPut()]
        public IActionResult Update(BlogDetail blogDetail)
        {
            if(BlogDetailService.Update(blogDetail))
                return Ok();
            else
                return NotFound();
        }

        [HttpDelete()]
        public IActionResult Delete(BlogDetail blogDetail)
        {
            if (BlogDetailService.Delete(blogDetail))
                return Ok();
            else 
                return BadRequest();
        }
    }
}