using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace videoSitecore2.Models
{
    public partial class MetaDataTypes
    {
        public MetaDataTypes()
        {
            MetaDataTitles = new HashSet<MetaDataTitles>();
        }

        public int Id { get; set; }
     
        [Display(Name="Name")]
        public string Name { get; set; }

        public virtual ICollection<MetaDataTitles> MetaDataTitles { get; set; }
    }
}
