using System;

namespace SitemaUnicesumar.Models
{
    public class LabReserveViewModel
    {
        public int LaboratoryId { get; set; }

        public int UserId { get; set; }

        public int TypeReserveId { get; set; }

        public int ClassId { get; set; }

        public int ShiftId { get; set; }

        public int DisciplineId { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }
    }
}