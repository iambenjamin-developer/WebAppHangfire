using Hangfire;
using Microsoft.AspNetCore.Mvc;
using WebAppHangfire.Jobs;

namespace WebAppHangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public ActionResult BuyProcessJob(int id)
        {
            BackgroundJob.Enqueue<BuyProcessJob>(x => x.Execute(id, null));

            return Ok();
        }


    }
}
