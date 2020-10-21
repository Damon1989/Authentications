﻿using System;

namespace Volo.Abp.Auditing
{
    [Serializable]
    public class EntityPropertyChangeInfo
    {
        public const int MaxPropertyNameLength = 96;
        public const int MaxValueLength = 512;
        public const int MaxPropertyTypeFullNameLength = 192;
        public virtual string NewValue { get; set; }
        public virtual string OriginalValue { get; set; }
        public virtual string PropertyName { get; set; }
        public virtual string PropertyTypeFullName { get; set; }
    }
}
