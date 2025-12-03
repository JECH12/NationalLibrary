using AutoMapper;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NacionalLibrary_Application.Models;
using Services.Interfaces;

namespace NacionalLibrary_Application.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IEditorialService _editorialService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IEditorialService editorialService, IMapper mapper, IAuthorService authorService)
        {
            _bookService = bookService;
            _editorialService = editorialService;
            _mapper = mapper;
            _authorService = authorService;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllAsync();
            List<BookModel> model = _mapper.Map<List<BookModel>>(books);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            List<Editorial> editorialsEntities = await _editorialService.GetAllAsync();
            ViewBag.Editorials = _mapper.Map<List<EditorialModel>>(editorialsEntities);

            List<Author> authorsEntities = await _authorService.GetAllAsync();
            ViewBag.Authors = _mapper.Map<List<AuthorModel>>(authorsEntities);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookModel model)
        {
            if (!ModelState.IsValid)
            {
                List<Editorial> editorialsEntities = await _editorialService.GetAllAsync();
                ViewBag.Editorials = _mapper.Map<List<EditorialModel>>(editorialsEntities);

                List<Author> authorsEntities = await _authorService.GetAllAsync();
                ViewBag.Authors = _mapper.Map<List<AuthorModel>>(authorsEntities);
                return View(model);
            }

            var bookEntity = _mapper.Map<Book>(model);
            bookEntity.AuthorBooks = new List<AuthorBook>();
            foreach (var authorId in model.SelectedAuthorIds)
            {
                
                bookEntity.AuthorBooks.Add(new AuthorBook { AuthorId = authorId });
            }

            await _bookService.CreateAsync(bookEntity);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var bookEntity = await _bookService.GetByIdAsync(id);
            if (bookEntity == null)
                return NotFound();

            var model = _mapper.Map<BookModel>(bookEntity);

            var editorialsEntities = await _editorialService.GetAllAsync();
            ViewBag.EditorialsSelect = new SelectList(
                _mapper.Map<List<EditorialModel>>(editorialsEntities),
                "Id",    
                "Name",  
                model.EditorialId 
            );

            var authorsEntities = await _authorService.GetAllAsync();
            ViewBag.AuthorsSelect = new MultiSelectList(
                _mapper.Map<List<AuthorModel>>(authorsEntities),
                "Id",    
                "FullName",
                model.BookAuthors.Select(ba => ba.AuthorId)
            );

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                List<Editorial> editorialsEntities = await _editorialService.GetAllAsync();
                ViewBag.Editorials = _mapper.Map<List<EditorialModel>>(editorialsEntities);

                List<Author> authorsEntities = await _authorService.GetAllAsync();
                ViewBag.Authors = _mapper.Map<List<AuthorModel>>(authorsEntities);

                return View(model);
            }

            var bookEntity = await _bookService.GetByIdAsync(id);
            if (bookEntity == null)
                return NotFound();

            _mapper.Map(model, bookEntity);

            bookEntity.AuthorBooks!
                    .Where(ab => !model.SelectedAuthorIds.Contains(ab.AuthorId))
                    .ToList()
                    .ForEach(ab => bookEntity.AuthorBooks!.Remove(ab));

            var existingIds = bookEntity.AuthorBooks!.Select(ab => ab.AuthorId).ToList();
            foreach (var authorId in model.SelectedAuthorIds)
            {
                if (!existingIds.Contains(authorId))
                {
                    bookEntity.AuthorBooks!.Add(new AuthorBook { AuthorId = authorId });
                }
            }

            await _bookService.UpdateAsync(bookEntity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();

            await _bookService.DeleteAsync(id);
            return Ok();
        }
    }
}
