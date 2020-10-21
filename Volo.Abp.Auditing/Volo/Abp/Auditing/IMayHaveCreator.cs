using System;
using JetBrains.Annotations;

namespace Volo.Abp.Auditing
{
    public interface IMayHaveCreator<TCreator> : IMayHaveCreator
    {
        [NotNull] TCreator Creator { get; set; }
    }

    public interface IMayHaveCreator
    {
        Guid? CreatorId { get; set; }
    }
}