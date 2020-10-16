using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Domain.Entities
{
    public interface IAggregateRoot:IEntity
    {
        
    }

    public interface IAggregateRoot<Tkey> : IEntity<Tkey>, IAggregateRoot
    {

    }
}
