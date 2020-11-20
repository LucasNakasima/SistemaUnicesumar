using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitemaUnicesumar.Models
{
    public class LaboratoryViewModel
    {
        public LaboratoryViewModel()
        {
            ListHeadOffice = new List<HeadOfficeViewModel>();
            ListBlock = new List<BlockViewModel>();
            ListLaboratoryCategory = new List<LaboratoryCategoryViewModel>();
            ListLaboratoryCapacity = new List<LaboratoryCapacityViewModel>();
            ListLab = new List<LabViewModel>();
        }

        public List<LabViewModel> ListLab { get; set; }

        public List<HeadOfficeViewModel> ListHeadOffice { get; set; }

        public List<BlockViewModel> ListBlock { get; set; }

        public List<LaboratoryCategoryViewModel> ListLaboratoryCategory { get; set; }

        public List<LaboratoryCapacityViewModel> ListLaboratoryCapacity { get; set; }
    }
}