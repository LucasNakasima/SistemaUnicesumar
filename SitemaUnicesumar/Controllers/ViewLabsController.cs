using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using SitemaUnicesumar.Data;
using SitemaUnicesumar.Models;
using SitemaUnicesumar.Helpers;
using System.Threading.Tasks;
using SitemaUnicesumar.Enum;

namespace SitemaUnicesumar.Controllers
{
    public class ViewLabsController : Controller
    {
        private const int _headOfficeDefault = 1;

        [HttpGet]
        public ActionResult Index(LaboratoryViewModel viewModel)
        {
            try
            {
                ViewBag.PageKey = "ViewLabs";

                var userId = Convert.ToInt32(Session["userId"]);

                if (userId == 0)
                    return RedirectToAction("Login", "Account");

                using (var context = new UnicesumarBdEntities())
                {
                    var laboratory = new LaboratoryViewModel();

                    laboratory.ListLab = context.ListLaboratory
                        .Include(p => p.HeadOffice)
                        .Include(p => p.Block)
                        .Include(p => p.LaboratoryCapacity)
                        .Include(p => p.LaboratoryCategory)
                        .Where(p => p.HeadOfficeId == _headOfficeDefault)
                        .Select(p => new LabViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            HeadOfficeTitle = p.HeadOffice.Title,
                            BlockTitle = p.Block.Title,
                            LaboratoryCapacityTitle = p.LaboratoryCapacity.Title,
                            LaboratoryCategoryTitle = p.LaboratoryCategory.Title,
                        }).ToList();

                    laboratory.ListHeadOffice = context.ListHeadOffice
                        .Select(p => new HeadOfficeViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                        })
                        .ToList();

                    laboratory.ListBlock = context.ListBlock
                        .Select(p => new BlockViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                        })
                        .ToList();

                    laboratory.ListLaboratoryCategory = context.ListLaboratoryCategory
                        .Select(p => new LaboratoryCategoryViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                        })
                        .ToList();

                    laboratory.ListLaboratoryCapacity = context.ListLaboratoryCapacity
                        .Select(p => new LaboratoryCapacityViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                        })
                        .ToList();

                    return View(laboratory);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Filter(FilterViewModel viewModel)
        {
            try
            {
                using (var context = new UnicesumarBdEntities())
                {
                    var laboratory = new LaboratoryViewModel();

                    laboratory.ListLab = context.ListLaboratory
                        .Include(p => p.HeadOffice)
                        .Include(p => p.Block)
                        .Include(p => p.LaboratoryCapacity)
                        .Include(p => p.LaboratoryCategory)
                        .Where(p => p.HeadOfficeId == viewModel.HeadOfficeId)
                        .Select(p => new LabViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            HeadOfficeTitle = p.HeadOffice.Title,
                            BlockTitle = p.Block.Title,
                            LaboratoryCapacityTitle = p.LaboratoryCapacity.Title,
                            LaboratoryCategoryTitle = p.LaboratoryCategory.Title,
                            BlockId = p.BlockId,
                            HeadOfficeId = p.HeadOfficeId,
                            LaboratoryCategoryId = p.LaboratoryCategoryId,
                            LaboratoryCapacityId = p.LaboratoryCapacityId,
                        }).ToList();

                    if (viewModel.BlockId != 0)
                        laboratory.ListLab = laboratory.ListLab.Where(p => p.BlockId == viewModel.BlockId).ToList();

                    if (viewModel.LaboratoryCapacityId != 0)
                        laboratory.ListLab = laboratory.ListLab.Where(p => p.LaboratoryCapacityId == viewModel.LaboratoryCapacityId).ToList();

                    if (viewModel.LaboratoryCategoryId != 0)
                        laboratory.ListLab = laboratory.ListLab.Where(p => p.LaboratoryCategoryId == viewModel.LaboratoryCategoryId).ToList();

                    laboratory.ListHeadOffice = context.ListHeadOffice
                        .Select(p => new HeadOfficeViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                        })
                        .ToList();

                    laboratory.ListBlock = context.ListBlock
                        .Select(p => new BlockViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                        })
                        .ToList();

                    laboratory.ListLaboratoryCategory = context.ListLaboratoryCategory
                        .Select(p => new LaboratoryCategoryViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                        })
                        .ToList();

                    laboratory.ListLaboratoryCapacity = context.ListLaboratoryCapacity
                        .Select(p => new LaboratoryCapacityViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                        })
                        .ToList();

                    var content = LayoutHelper.GetPartialViewData("_ListLabs", ControllerContext, ViewData, TempData, laboratory.ListLab).ToString();

                    return Json(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ManageScheduleLab(int id)
        {
            try
            {
                using (var context = new UnicesumarBdEntities())
                {
                    var viewModel = new LaboratoryReserveViewModel();

                    viewModel.LaboratoryId = id;

                    var laboratory = context.ListLaboratory.FirstOrDefault(p => p.Id == id);

                    viewModel.LaboratoryName = laboratory.Name;

                    viewModel.ListTypeReserve = new SelectList(GetListExamsStatusTypes(), "Value", "Description");

                    var listClass = await context.ListClass
                        .Select(p => new GetDropDownListViewModel
                        {
                            Id = p.Id,
                            Title = p.Title
                        })
                        .ToListAsync();

                    viewModel.ListClass = new SelectList(listClass, "Id", "Title");

                    var listShift = await context.ListShift
                        .Select(p => new GetDropDownListViewModel
                        {
                            Id = p.Id,
                            Title = p.Title
                        })
                        .ToListAsync();

                    viewModel.ListShift = new SelectList(listShift, "Id", "Title");

                    if (laboratory.LaboratoryCategoryId == 1)
                    {
                        var listDiscipline = await context.ListDiscipline
                          .Where(p => p.Id <= 6)
                          .Select(p => new GetDropDownListViewModel
                          {
                              Id = p.Id,
                              Title = p.Title
                          })
                          .ToListAsync();

                        viewModel.ListDiscipline = new SelectList(listDiscipline, "Id", "Title");
                    }
                    else
                    {
                        var listDiscipline = await context.ListDiscipline
                         .Where(p => p.Id >= 7)
                         .Select(p => new GetDropDownListViewModel
                         {
                             Id = p.Id,
                             Title = p.Title
                         })
                         .ToListAsync();

                        viewModel.ListDiscipline = new SelectList(listDiscipline, "Id", "Title");
                    }

                    var listHorary = await context.ListHorary.
                        Select(p => new GetDropDownListViewModel
                        {
                            Id = p.Id,
                            Title = p.Title
                        })
                        .ToListAsync();

                    viewModel.ListHorary = new SelectList(listHorary, "Id", "Title");

                    var content = LayoutHelper.GetPartialViewData("_ModalBody", ControllerContext, ViewData, TempData, viewModel).ToString();

                    return Json(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ReserveLab(LaboratoryReserveViewModel model)
        {
            try
            {
                using (var context = new UnicesumarBdEntities())
                {
                    var userId = Convert.ToInt32(Session["userId"]);

                    if (userId == 0)
                        return RedirectToAction("Login", "Account");

                    var labReserve = new LaboratoryReserve
                    {
                        LaboratoryId = model.LaboratoryId,
                        UserId = userId,
                        TypeReserveId = model.TypeReserveId,
                        ClassId = model.ClassId,
                        ShiftId = model.ShiftId,
                        DisciplineId = model.DisciplineId,
                        Date = model.TypeReserveId == ETypeReserve.Simple ? model.Date : null,
                        DateStart = model.TypeReserveId == ETypeReserve.Recorrent ? model.DateStart : null,
                        DateEnd = model.TypeReserveId == ETypeReserve.Recorrent ? model.DateEnd : null,
                        HoraryId = model.HoraryId,
                    };

                    context.Entry(labReserve).State = EntityState.Added;

                    await context.SaveChangesAsync();

                    return RedirectToAction("Index", "MyReservations");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ListEnumTypeViewModel> GetListExamsStatusTypes()
        {
            var listTypes = new List<ListEnumTypeViewModel>();

            var listEnums = System.Enum.GetValues(typeof(ETypeReserve)).Cast<ETypeReserve>().ToList();

            foreach (var type in listEnums)
            {
                var model = new ListEnumTypeViewModel
                {
                    Value = (int)type,
                    Description = LayoutHelper.GetEnumDescription(type),
                };

                listTypes.Add(model);
            }

            return listTypes;
        }
    }
}