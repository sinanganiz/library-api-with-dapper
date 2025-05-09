using AutoMapper;
using FluentValidation;
using LibraryApi.DTOs.Books;
using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookDto> _createBookValidator;
        private readonly IValidator<UpdateBookDto> _updateBookValidator;


        public BooksController(IBookRepository repository, IMapper mapper, IValidator<CreateBookDto> createBookValidator, IValidator<UpdateBookDto> updateBookValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _createBookValidator = createBookValidator;
            _updateBookValidator = updateBookValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ResponseBookDto>>(books));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book is null) return NotFound();

            return Ok(_mapper.Map<ResponseBookDto>(book));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var result = await _createBookValidator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var book = _mapper.Map<Book>(dto);
            var id = await _repository.CreateAsync(book);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookDto dto)
        {
            var result = await _updateBookValidator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var book = _mapper.Map<Book>(dto);
            book.Id = id;
            var updated = await _repository.UpdateAsync(book);

            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);

            return deleted ? NoContent() : NotFound();
        }
    }
}