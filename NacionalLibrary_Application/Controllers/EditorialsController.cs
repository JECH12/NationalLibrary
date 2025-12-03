using AutoMapper;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using NacionalLibrary_Application.Models;
using Services.Interfaces;

namespace NacionalLibrary_Application.Controllers
{
    public class EditorialsController : Controller
    {
        private readonly IEditorialService _editorialService;
        private readonly IMapper _mapper;

        public EditorialsController(IEditorialService editorialService, IMapper mapper)
        {
            _editorialService = editorialService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var editorials = await _editorialService.GetAllAsync();
            List<EditorialModel> model = _mapper.Map<List<EditorialModel>>(editorials);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Editorial model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _editorialService.CreateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var editorial = await _editorialService.GetByIdAsync(id);
            if (editorial == null)
                return NotFound();

            var model = _mapper.Map<EditorialModel>(editorial);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditorialModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var editorialEntity = await _editorialService.GetByIdAsync(id);
            if (editorialEntity == null)
                return NotFound();

            _mapper.Map(model, editorialEntity);

            await _editorialService.UpdateAsync(editorialEntity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var editorial = await _editorialService.GetByIdAsync(id);
            if (editorial == null)
                return NotFound();

            if (editorial.Books!.Any())
                return BadRequest("Cannot delete this editorial because it has books assigned.");

            await _editorialService.DeleteAsync(id);
            return Ok();
        }
    }
}
