using Domain.Base.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);
    }
}
