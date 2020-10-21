using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace Volo.Abp.Localization
{
    public interface ILocalizableString
    {
        LocalizableString Localize(IStringLocalizerFactory stringLocalizerFactory);
    }
}
