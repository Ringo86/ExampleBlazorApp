using ExampleBlazorApp.Client.Models.MessageBoard;
using ExampleBlazorApp.Client.Services;

namespace ExampleBlazorApp.Client.ViewModels
{
    public interface IMessageBoardViewModel
    {
        bool IsBusy { get; }
        Task GetMessageBoard(GetMessageRequest getRequest);
    }

    public class MessageBoardViewModel: IMessageBoardViewModel
    {
        private readonly IMessageBoardService messageBoardService;

        public MessageBoardViewModel(IMessageBoardService messageBoardService)
        {
            this.messageBoardService = messageBoardService;
        }

        public bool IsBusy { get; private set; }

        public async Task GetMessageBoard(GetMessageRequest getRequest)
        {
            IsBusy = true;
            int result = await messageBoardService.GetMessageBoard(getRequest);
            IsBusy = false;
        }
    }
}
