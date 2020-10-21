using System;
using JetBrains.Annotations;

namespace Volo.Abp.Auditing
{
    public interface IMustHaveCreator<TCreator> : IMustHaveCreator
    {
        [NotNull] TCreator Creator { get; set; }
    }

    public interface IMustHaveCreator
    {
        Guid CreatorId { get; set; }
    }
}