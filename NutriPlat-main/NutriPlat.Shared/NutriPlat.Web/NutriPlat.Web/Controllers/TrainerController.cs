using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NutriPlat.Shared.Dtos;
using NutriPlat.Web.Models;
using NutriPlat.Web.Services.Interfaces;

namespace NutriPlat.Web.Controllers
{
    [Authorize(Roles = "Trainer")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        public IActionResult CreateRoutine()
        {
            return View(new CreateRoutineViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRoutine(CreateRoutineViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            // 👇 Aquí va el mapeo del ViewModel al Dto
            var dto = new WorkoutRoutineDto
            {
                Name = model.Name,
                Description = model.Description
            };

            var success = await _trainerService.CreateRoutineAsync(dto, token);
            if (success)
            {
                TempData["Message"] = "✅ Rutina creada exitosamente.";
                return RedirectToAction("CreateRoutine");
            }

            ModelState.AddModelError(string.Empty, "❌ Error al crear rutina.");
            return View(model);
        }

    }

}
