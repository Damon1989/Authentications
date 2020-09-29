﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Modularity
{
    public class DependsOnAttribute:Attribute,IDependedTypesProvider
    {
        [NotNull]
        public Type[] DependedTypes { get; }

        public DependsOnAttribute(params Type[] dependedTypes)
        {
            DependedTypes = dependedTypes?? new Type[0];
        }
        public virtual Type[] GetDependedTypes()
        {
            return DependedTypes;
        }
    }
}
