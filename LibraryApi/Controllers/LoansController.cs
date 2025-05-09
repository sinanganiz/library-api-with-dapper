using AutoMapper;
using LibraryApi.DTOs.Loans;
using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _repository;
        private readonly IMapper _mapper;

        public LoansController(ILoanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var loans = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ResponseLoanDto>>(loans));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var loan = await _repository.GetByIdAsync(id);

            if (loan is null) return NotFound();

            return Ok(_mapper.Map<ResponseLoanDto>(loan));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLoanDto dto)
        {
            var loan = _mapper.Map<Loan>(dto);
            var id = await _repository.CreateAsync(loan);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateLoanDto dto)
        {
            var loan = _mapper.Map<Loan>(dto);
            loan.Id = id;
            var updated = await _repository.UpdateAsync(loan);

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