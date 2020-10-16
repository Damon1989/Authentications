using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.MultiTenancy;

namespace Volo.Abp.Domain.Entities
{
    public static class EntityHelper
    {
        private static readonly ConcurrentDictionary<string,PropertyInfo> CachedIdProperties=new ConcurrentDictionary<string, PropertyInfo>();

        public static bool EntityEquals(IEntity entity1, IEntity entity2)
        {
            if (entity1 == null || entity2 == null)
            {
                return false;
            }

            if (ReferenceEquals(entity1,entity2))
            {
                return true;
            }

            var typeOfEntity1 = entity1.GetType();
            var typeOfEntity2 = entity2.GetType();
            if (!typeOfEntity1.IsAssignableFrom(typeOfEntity2) && !typeOfEntity2.IsAssignableFrom(typeOfEntity1))
            {
                return false;
            }

            if (entity1 is IMultiTenant&&entity2 is IMultiTenant)
            {
                var tenant1Id = ((IMultiTenant) entity1).TenantId;
                var tenant2Id = ((IMultiTenant) entity2).TenantId;

                if (tenant1Id!=tenant2Id)
                {
                    if (tenant1Id==null||tenant2Id==null)
                    {
                        return false;
                    }

                    if (!tenant1Id.Equals(tenant2Id))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static bool IsDefaultKeyValue(object value)
        {
            if (value == null)
            {
                return true;
            }

            var type = value.GetType();

            if (type == typeof(int))
            {
                return Convert.ToInt32(value) <= 0;
            }

            if (type == typeof(long))
            {
                return Convert.ToInt64(value) <= 0;
            }

            return true;// TypeHelper.IsDefaultValue(value);
        }
    }
}
