using Data.Common.Enums;
using System;
using System.Collections.Generic;

namespace Data.Models
{
    public class Log
    {
        public Log() => Tags = new HashSet<Tag>();

        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public LogType Type { get; set; }
        public string EncodedTitle { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
#nullable enable
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<Tag> Tags { get; set; }
#nullable disable
    }
}