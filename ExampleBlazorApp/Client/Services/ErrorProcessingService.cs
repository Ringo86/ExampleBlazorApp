namespace ExampleBlazorApp.Client.Services
{
    public interface IErrorProcessingService
    {
        void ProcessError(Exception exception);
    }
    public class ErrorProcessingService : IErrorProcessingService
    {
        private readonly AlertService alertService;

        public ErrorProcessingService(AlertService alertService)
        {
            this.alertService = alertService;
        }

        public void ProcessError(Exception exception)
        {
            alertService.Error(exception.Message);
        }
    }
}
