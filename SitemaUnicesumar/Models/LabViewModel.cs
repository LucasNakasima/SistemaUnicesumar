using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitemaUnicesumar.Models
{
    public class LabViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int HeadOfficeId { get; set; }

        public string HeadOfficeTitle { get; set; }

        public int BlockId { get; set; }

        public string BlockTitle { get; set; }

        public int LaboratoryCategoryId { get; set; }

        public string LaboratoryCategoryTitle { get; set; }

        public int LaboratoryCapacityId { get; set; }

        public string LaboratoryCapacityTitle { get; set; }
    }
}