using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TELBlazor.Components.Core.Enums;

namespace TELBlazor.Components.Core.Extensions
{
    public static class TELButtonStyleExtensions
    {
        public static string ToCssClass(this TELButtonStyle style)
        {
            return style switch
            {
                TELButtonStyle.Primary => "nhsuk-button",
                TELButtonStyle.Secondary => "nhsuk-button nhsuk-button--secondary",
                TELButtonStyle.Reverse => "nhsuk-button nhsuk-button--reverse",
                TELButtonStyle.Warning => "nhsuk-button nhsuk-button--warning",
                _ => "nhsuk-button"
            };
        }
    }
}
