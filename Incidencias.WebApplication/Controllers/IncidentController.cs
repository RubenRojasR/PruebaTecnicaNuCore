using Incidencias.Application.Interfaces;
using Incidencias.Domain.Entities;
using Incidencias.Domain.Enums;
using Incidencias.WebApplication.Models.Incidents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Incidencias.WebApplication.Controllers
{
    public class IncidentController : Controller
    {
        private readonly IIncidentService _incidentService;
        private readonly ICategoryService _categoryService;
        private readonly IResponsibleService _responsibleService;

        public IncidentController(
            IIncidentService incidentService,
            ICategoryService categoryService,
            IResponsibleService responsibleService)
        {
            _incidentService = incidentService;
            _categoryService = categoryService;
            _responsibleService = responsibleService;
        }


        public async Task<IActionResult> Index(
    int? status,
    int? priority)
        {
            var incidents = await _incidentService
                .GetIncidents(status, priority);


            var model = new IncidentFilterViewModel
            {
                Status = status,
                Priority = priority,
                Incidents = incidents
            };


            return View(model);
        }


        public async Task<IActionResult> Create()
        {
            var model = new IncidentCreateViewModel();

            await LoadCatalogs(model);

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            IncidentCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadCatalogs(model);
                return View(model);
            }


            try
            {
                var incident = new Incident
                {
                    Title = model.Title,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    ResponsibleId = model.ResponsibleId,
                    Priority = (Priority)model.Priority,
                };


                await _incidentService.CreateIncident(incident);


                TempData["Success"] =
                    "Incidencia creada correctamente.";


                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                await LoadCatalogs(model);

                return View(model);
            }
        }



        public async Task<IActionResult> Edit(int id)
        {
            var incident =
                await _incidentService.GetIncidentById(id);


            if (incident == null)
                return NotFound();


            var model = new IncidentEditViewModel
            {
                Id = incident.Id,

                Title = incident.Title,

                Description = incident.Description,

                CategoryId = incident.CategoryId,

                ResponsibleId = incident.ResponsibleId,

                Priority = incident.Priority
            };


            await LoadCatalogs(model);


            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            IncidentEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadCatalogs(model);

                return View(model);
            }


            try
            {
                var incident = new Incident
                {
                    Id = model.Id,

                    Title = model.Title,

                    Description = model.Description,

                    CategoryId = model.CategoryId,

                    ResponsibleId = model.ResponsibleId,

                    Priority = model.Priority,

                    UpdatedDate = DateTime.Now
                };


                await _incidentService.UpdateIncident(incident);


                TempData["Success"] =
                    "Incidencia actualizada.";


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                await LoadCatalogs(model);

                return View(model);
            }
        }



        private async Task LoadCatalogs(
            IncidentCreateViewModel model)
        {
            var categories =
                await _categoryService.GetActiveCategories();


            model.Categories =
                categories
                .Where(x => x.IsActive)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToList();



            var responsibles =
                await _responsibleService.GetResponsibles();


            model.Responsibles =
                responsibles
                .Where(x => x.IsActive)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToList();
        }



        private async Task LoadCatalogs(
            IncidentEditViewModel model)
        {
            var categories =
                await _categoryService.GetActiveCategories();


            model.Categories =
                categories
                .Where(x => x.IsActive)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToList();



            var responsibles =
                await _responsibleService.GetResponsibles();


            model.Responsibles =
                responsibles
                .Where(x => x.IsActive)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                })
                .ToList();
        }

        public async Task<IActionResult> ChangeStatus(int id)
        {
            var incident = await _incidentService.GetIncidentById(id);

            if (incident == null)
                return NotFound();


            var model = new ChangeStatusViewModel
            {
                Id = incident.Id,
                Code = incident.Code,
                Title = incident.Title,
                CurrentStatus = (int)incident.Status,

                Status = new List<SelectListItem>
        {
            
            new SelectListItem
            {
                Value = "2",
                Text = "En progreso"
            },
            new SelectListItem
            {
                Value = "3",
                Text = "Resuelta"
            },
            new SelectListItem
            {
                Value = "4",
                Text = "Cancelada"
            }
        }
            };


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(
    ChangeStatusViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);


            try
            {
                await _incidentService.ChangeStatus(
                    model.Id,
                    (IncidentStatus)model.NewStatus,
                    model.Comment
                );


                TempData["Success"] =
                    "Estado actualizado correctamente.";


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(model);
            }
        }


        public async Task<IActionResult> Details(int id)
        {
            var incident =
                await _incidentService.GetIncidentById(id);


            if (incident == null)
                return NotFound();


            var model = new IncidentDetailViewModel
            {
                Incident = incident,

                History = incident.History
                    .OrderByDescending(x => x.UpdatedDate)
                    .ToList()
            };


            return View(model);
        }

    }


}
