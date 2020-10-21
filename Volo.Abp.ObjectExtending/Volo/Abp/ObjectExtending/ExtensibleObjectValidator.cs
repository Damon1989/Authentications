using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace Volo.Abp.ObjectExtending
{
    public class ExtensibleObjectValidator
    {
        public static void CheckValue([NotNull] IHasExtraProperties extensibleObject, [NotNull] string propertyName,
            [CanBeNull] object value)
        {

        }

        private static void AddPropertyValidationAttributeErrors(IHasExtraProperties extensibleObject,
            List<ValidationResult> validationErrors, ValidationContext objectValidationContext)
        {

        }
    }
}
