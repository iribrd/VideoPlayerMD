using System;
using System.Collections.Generic;

namespace videoSitecore2.Models
{
    public partial class MetaData
    {
        public long Id { get; set; }
        public int VideoId { get; set; }
        public int MetadataTitleId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public virtual MetaDataTitles MetadataTitle { get; set; }
        public virtual Videos Video { get; set; }
    }
}
