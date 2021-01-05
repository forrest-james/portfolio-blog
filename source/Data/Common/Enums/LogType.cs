using System.ComponentModel.DataAnnotations;

namespace Data.Common.Enums
{
    public enum LogType
    {
        [Display(Name = "Designer's Log")]
        Design = 1,
        [Display(Name = "Developer's Log")]
        Development,
        [Display(Name = "Personal Log")]
        Personal,
        [Display(Name = "Reader's Log")]
        Reading,
        [Display(Name = "Writer's Log")]
        Writing
    }
}