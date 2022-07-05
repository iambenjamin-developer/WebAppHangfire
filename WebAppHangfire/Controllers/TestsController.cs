using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace WebAppHangfire.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {

        [HttpGet("Fire-and-Forget-Jobs")]
        public void Enqueue()
        {
            var jobId = BackgroundJob.Enqueue(() =>
                          Console.WriteLine("Fire-and-forget!"));
        }

        [HttpGet("Delayed-Jobs")]
        public void Schedule()
        {
            var jobId = BackgroundJob.Schedule(() =>
                          Console.WriteLine("Delayed!"), TimeSpan.FromMinutes(3));
        }

        [HttpGet("Recurring-Jobs")]
        public void AddOrUpdate()
        {
            RecurringJob.AddOrUpdate("myrecurringjob", () =>
                    Console.WriteLine("Recurring!"), "*/1 * * * *");
                  //Console.WriteLine("Recurring!"), Cron.Minutely);
        }

        [HttpGet("Continuations")]
        public void ContinueJobWith()
        {
            var jobId = BackgroundJob.Enqueue(() =>
            Console.WriteLine("First Job!"));


            BackgroundJob.ContinueJobWith(jobId, () =>
                      Console.WriteLine("Continuation!"));
        }
    }
}
