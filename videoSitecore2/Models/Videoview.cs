using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace videoSitecore2.Models
{
    public class Videoview
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Title { get;  set; }
        public string StartTime { get; set; }
        public int metaDataTypeId { get; set; }
        public int metadataTitleId { get; set; }
        
        public string metaTypeName { get; set; }

        public List<MetaDataTitles> metaDataTitles { get; set; }
        //public ICollection<MetaDataTitles> metaDataTitles { get; set; }

        // public MetaDataTitles metaDataTitles { get; set; }

    }
}



