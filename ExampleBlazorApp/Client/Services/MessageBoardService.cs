using ExampleBlazorApp.Client.Models.MessageBoard;

namespace ExampleBlazorApp.Client.Services
{
    public interface IMessageBoardService
    {
        Task<int> GetMessageBoard(GetMessageRequest getRequest);
    }

    public class MessageBoardService: IMessageBoardService
    {
        private IHttpService httpService;

        public MessageBoardService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<int> GetMessageBoard(GetMessageRequest getRequest)
        {
            return await httpService.Get<int>("messageBoard", getRequest);
        }
    }
}
