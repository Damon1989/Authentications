using System;
using System.Collections.Generic;

namespace Volo.Abp.Domain.Entities
{
    [Serializable]
    public abstract class Entity:IEntity
    {
        public override string ToString()
        {
            return $"[ENTITY:{GetType().Name}] Keys={GetKeys().JoinAsString(",")}";
        }

        public abstract object[] GetKeys();

        public bool EntityEquals(IEntity other)
        {
            return EntityHelper.EntityEquals(this,other);
        }
    }

    [Serializable]
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }

        public Entity()
        {
            
        }

        public Entity(TKey id)
        {
            Id = id;
        }

        public override object[] GetKeys()
        {
            return new object[]{Id};
        }

        public override string ToString()
        {
            return $"[ENTITY:{GetType().Name}] Id={Id}";
        }
    }
}
