using Incidencias.Application.Interfaces;
using Incidencias.Domain.Entities;
using Incidencias.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace Incidencias.WebApplication.Controllers
{
    public class ResponsibleController : Controller
    {
        private readonly IResponsibleService _service;

        public ResponsibleController(IResponsibleService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var responsibles = await _service.GetResponsibles();
            return View(responsibles);
        }

        public IActionResult Create()
        {
            return View(new ResponsibleViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResponsibleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _service.CreateResponsible(new Responsible
                {
                    Name = model.Name,
                    Email = model.Email,
                    IsActive = model.IsActive
                });

                TempData["Success"] = "Responsable creado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var responsible = await _service.GetResponsibleById(id);

            if (responsible == null)
                return NotFound();

            return View(new ResponsibleViewModel
            {
                Id = responsible.Id,
                Name = responsible.Name,
                Email = responsible.Email,
                IsActive = responsible.IsActive
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ResponsibleViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _service.UpdateResponsible(new Responsible
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    IsActive = model.IsActive
                });

                TempData["Success"] = "Responsable actualizado correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteResponsible(id);

            TempData["Success"] = "Responsable desactivado.";

            return RedirectToAction(nameof(Index));
        }
    }
}
