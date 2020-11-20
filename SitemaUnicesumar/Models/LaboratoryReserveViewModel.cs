using SitemaUnicesumar.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SitemaUnicesumar.Models
{
    public class LaboratoryReserveViewModel
    {
        public int Id { get; set; }
        
        public int LaboratoryId { get; set; }

        public string LaboratoryName { get; set; }

        public string LaboratoryBlockName { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Tipo Reserva")]
        public ETypeReserve TypeReserveId { get; set; }

        public string TypeReserveDescription { get; set; }

        [Display(Name = "Turma")]
        public int ClassId { get; set; }

        public string ClassTitle { get; set; }

        [Display(Name = "Turno")]
        public int ShiftId { get; set; }

        public string ShiftTitle { get; set; }

        [Display(Name = "Disciplina")]
        public int DisciplineId { get; set; }

        public string DisciplineTitle { get; set; }

        [Display(Name = "Horario")]
        public int HoraryId { get; set; }

        public string HoraryTitle { get; set; }

        [Display(Name = "Data Reserva")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

        [Display(Name = "Data Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateStart { get; set; }

        [Display(Name = "Data Final")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DateEnd { get; set; }

        public string StrDate => Date.Value.ToString("dd/MM/yyyy");

        public string StrDateStart => DateStart.Value.ToString("dd/MM/yyyy");

        public string StrDateEnd => DateEnd.Value.ToString("dd/MM/yyyy");

        public SelectList ListTypeReserve { get; set; }

        public SelectList ListClass { get; set; }

        public SelectList ListShift { get; set; }

        public SelectList ListDiscipline { get; set; }

        public SelectList ListHorary { get; set; }
    }
}