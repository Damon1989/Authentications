using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.Localization
{
    public interface IHasNameWithLocalizableDisplayName
    {
        [NotNull]
         string Name { get; set; }


    }
}
