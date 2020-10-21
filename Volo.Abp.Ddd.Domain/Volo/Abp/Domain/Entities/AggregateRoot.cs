using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;

namespace Volo.Abp.Domain.Entities
{
    [Serializable]
    public abstract class AggregateRoot:Entity,IAggregateRoot,IGeneratesDomainEvents,IHasExtraProperties,IHasConcurrencyStamp
    {
        public virtual Dictionary<string,object> ExtraProperties { get; protected set; }

        
        public virtual string ConcurrencyStamp { get; set; }

        private readonly ICollection<object> _localEvents=new Collection<object>();
        private readonly ICollection<object> _distributedEvents=new Collection<object>();

        protected AggregateRoot()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            ExtraProperties=new Dictionary<string, object>();
        }

    }
}
