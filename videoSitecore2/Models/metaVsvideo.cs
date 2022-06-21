using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace videoSitecore2.Models
{
    
    public class metaVsvideo
    {
        public int Id { get; set; }

        public Videos videos { get; set; }
        public MetaData metadata { get; set; }
    }
}


