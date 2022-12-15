using ExampleBlazorApp.Client.Models.MessageBoard;
using ExampleBlazorApp.Client.Services;

namespace ExampleBlazorApp.Client.ViewModels
{
    public class MessageBoardViewModel : BaseViewModel
    {
        private readonly IMessageBoardService messageBoardService;

        public GetMessageRequest Model
        {
            get;
            private set;
        } = new();


        public MessageBoardViewModel(IMessageBoardService messageBoardService)
        {
            this.messageBoardService = messageBoardService;
        }

        public async Task GetMessageBoard()
        {
            IsBusy = true;
            int result = await messageBoardService.GetMessageBoard(Model);
            IsBusy = false;
        }
    }
}
