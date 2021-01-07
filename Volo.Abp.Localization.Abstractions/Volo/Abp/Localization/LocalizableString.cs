using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Localization;

namespace Volo.Abp.Localization
{
    public class LocalizableString
    {
        [CanBeNull]
        public Type ResourceType { get; set; }

        [NotNull]
        public string Name { get; set; }

        public LocalizableString(Type resourceType,[NotNull]string name)
        {
            Name = Check.NotNullOrEmpty(name, nameof(name));
            ResourceType = resourceType;
        }

        //public LocalizedString Localize(IStringLocalizerFactory stringLocalizerFactory)
        //{
        //    return stringLocalizerFactory.Create(ResourceType)[Name];
        //}

        public static LocalizableString Create<TResource>([NotNull] string name)
        {
            return new LocalizableString(typeof(TResource), name);
        }
    }
}
