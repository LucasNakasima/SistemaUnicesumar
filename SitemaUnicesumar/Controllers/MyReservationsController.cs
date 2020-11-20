using SitemaUnicesumar.Data;
using SitemaUnicesumar.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SitemaUnicesumar.Helpers;
using System.Threading.Tasks;

namespace SitemaUnicesumar.Controllers
{
    public class MyReservationsController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                ViewBag.PageKey = "MyReservations";

                var userId = Convert.ToInt32(Session["userId"]);

                if (userId == 0)
                    return RedirectToAction("Login", "Account");

                using (var context = new UnicesumarBdEntities())
                {
                    var listLabsReserve = context.ListLaboratoryReserve
                       .Include(p => p.Laboratory)
                       .Include(p => p.Laboratory.Block)
                       .Include(p => p.Class)
                       .Where(p => p.UserId == userId)
                       .Select(p => new LaboratoryReserveViewModel
                       {
                           Id = p.Id,
                           LaboratoryName = p.Laboratory.Name,
                           LaboratoryBlockName = p.Laboratory.Block.Title,
                           ClassTitle = p.Class.Title,
                           Date = p.Date,
                           DateStart = p.DateStart,
                           DateEnd = p.DateEnd,
                           TypeReserveId = p.TypeReserveId,
                       })
                       .OrderByDescending(p => p.Id)
                       .ToList();

                    return View(listLabsReserve);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> ViewReserve(int id)
        {
            try
            {
                using (var context = new UnicesumarBdEntities())
                {
                    var reserveLab = await context.ListLaboratoryReserve
                       .Include(p => p.Laboratory)
                       .Include(p => p.Laboratory.Block)
                       .Include(p => p.Class)
                       .Include(p => p.Horary)
                       .Where(p => p.Id == id)
                       .Select(p => new LaboratoryReserveViewModel
                       {                           
                           TypeReserveId = p.TypeReserveId,
                           Date = p.Date,
                           DateStart = p.DateStart,
                           DateEnd = p.DateEnd,
                           LaboratoryName = p.Laboratory.Name,
                           LaboratoryBlockName = p.Laboratory.Block.Title,
                           DisciplineTitle = p.Discipline.Title,
                           ClassTitle = p.Class.Title,
                           ShiftTitle = p.Shift.Title,
                           HoraryTitle = p.Horary.Title,
                       })
                       .FirstOrDefaultAsync();

                    reserveLab.TypeReserveDescription = LayoutHelper.GetEnumDescription(reserveLab.TypeReserveId);

                    var content = LayoutHelper.GetPartialViewData("_ModalBody", ControllerContext, ViewData, TempData, reserveLab).ToString();

                    return Json(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CancelReserveModal(int id)
        {
            try
            {
                using (var context = new UnicesumarBdEntities())
                {
                    var viewModel = new LaboratoryReserveViewModel 
                    {
                        Id = id,
                    };

                    var content = LayoutHelper.GetPartialViewData("_ModalCancel", ControllerContext, ViewData, TempData, viewModel).ToString();

                    return Json(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CancelReserve(LaboratoryReserveViewModel model)
        {
            try
            {
                using (var context = new UnicesumarBdEntities())
                {
                    var reserve = await context.ListLaboratoryReserve.FirstOrDefaultAsync(p => p.Id == model.Id);

                    context.Entry(reserve).State = EntityState.Deleted;

                    await context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}