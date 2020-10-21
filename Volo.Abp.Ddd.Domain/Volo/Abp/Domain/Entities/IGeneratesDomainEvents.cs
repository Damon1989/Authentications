using System.Collections.Generic;

namespace Volo.Abp.Domain.Entities
{
    public interface IGeneratesDomainEvents
    {
        IEnumerable<object> GetLocalEvents();

        IEnumerable<object> GetDistributedEvents();

        void ClearLocalEvents();

        void ClearDistributedEvents();
    }
}