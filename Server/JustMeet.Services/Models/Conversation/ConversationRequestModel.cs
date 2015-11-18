namespace JustMeet.Services.Models.Conversation
{
    using System; 

    public class ConversationRequestModel
    {
        public string Topic { get; set; }

        public string Text { get; set; }

        public DateTime StartedOn { get; set; }
    }
}