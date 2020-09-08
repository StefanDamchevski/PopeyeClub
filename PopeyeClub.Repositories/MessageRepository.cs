using PopeyeClub.Data;
using PopeyeClub.Repositories.Interfaces;

namespace PopeyeClub.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext context;

        public MessageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Message message)
        {
            context.Messages.Add(message);
            context.SaveChanges();
        }
    }
}
