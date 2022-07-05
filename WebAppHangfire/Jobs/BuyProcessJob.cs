using Hangfire;
using Hangfire.Server;

namespace WebAppHangfire.Jobs
{
    public class BuyProcessJob
    {
        private readonly BusinessDbContext _context;

        public BuyProcessJob(BusinessDbContext context)
        {
            _context = context;
        }

        [JobDisplayName("Initial:Step1")]
        [AutomaticRetry(Attempts = 0)]
        public void Execute(int productId, PerformContext context)
        {
            var backgroundJobId = context.BackgroundJob.Id;

            Console.WriteLine($"Initial:Step1 - BackgroundJobId: {backgroundJobId}");

            var product = _context.Products.FirstOrDefault(x => x.Id == productId);

            if (product != null)
            {
                BackgroundJob.ContinueJobWith(context.BackgroundJob.Id,
                    () => Catalog(backgroundJobId, product.Name));
            }
        }


        [JobDisplayName("Catalog:Step2")]
        [AutomaticRetry(Attempts = 0)]
        public void Catalog(string backgroundJobId, string productName)
        {
            Console.WriteLine($"Catalog:Step2 - BackgroundJobId: {backgroundJobId}");

            var product = _context.Products.FirstOrDefault(x => x.Name == productName);

            BackgroundJob.ContinueJobWith(backgroundJobId,
               () => Basket(backgroundJobId, product.Name));
        }


        [JobDisplayName("Basket:Step3")]
        [AutomaticRetry(Attempts = 0)]
        public void Basket(string backgroundJobId, string productName)
        {
            Console.WriteLine($"Basket:Step3 - BackgroundJobId: {backgroundJobId}");

            var product = _context.Products.FirstOrDefault(x => x.Name == productName);

            BackgroundJob.ContinueJobWith(backgroundJobId,
               () => Order(backgroundJobId, product.Name));
        }


        [JobDisplayName("Order:Step4")]
        [AutomaticRetry(Attempts = 0)]
        public void Order(string backgroundJobId, string productName)
        {
            Console.WriteLine($"Order:Step4 - BackgroundJobId: {backgroundJobId}");

            var product = _context.Products.FirstOrDefault(x => x.Name == productName);

            BackgroundJob.ContinueJobWith(backgroundJobId,
               () => Pay(backgroundJobId, product.Name));
        }


        [JobDisplayName("Pay:Step5")]
        [AutomaticRetry(Attempts = 0)]
        public void Pay(string backgroundJobId, string productName)
        {
            Console.WriteLine($"Pay:Step5 - BackgroundJobId: {backgroundJobId}");

            var product = _context.Products.FirstOrDefault(x => x.Name == productName);

            BackgroundJob.ContinueJobWith(backgroundJobId,
               () => Delivery(backgroundJobId));
        }


        [JobDisplayName("Delivery:Step6")]
        [AutomaticRetry(Attempts = 0)]
        public void Delivery(string backgroundJobId)
        {
            Console.WriteLine($"Delivery:Step6 - BackgroundJobId: {backgroundJobId}");
        }
    }
}
