using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitemaUnicesumar.Models
{
    public class FilterViewModel
    {
        public int HeadOfficeId { get; set; }

        public int BlockId { get; set; }

        public int LaboratoryCategoryId { get; set; }

        public int LaboratoryCapacityId { get; set; }
    }
}