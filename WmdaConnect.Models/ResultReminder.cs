using WmdaConnect.Models.MessageBases;
using WmdaConnect.Models.MessagePayloads;

namespace WmdaConnect.Models
{
    public class ResultReminder : Message<ResultReminderPayload>
    {
        public ResultReminder(MessageRequest<ResultReminderPayload> messageRequest) : base(messageRequest)
        {
        }
    }
}
