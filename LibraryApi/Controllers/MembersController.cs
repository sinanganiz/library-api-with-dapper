using AutoMapper;
using LibraryApi.DTOs.Members;
using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _repository;
        private readonly IMapper _mapper;

        public MembersController(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ResponseMemberDto>>(members));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _repository.GetByIdAsync(id);

            if (member is null) return NotFound();

            return Ok(_mapper.Map<ResponseMemberDto>(member));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberDto dto)
        {
            var member = _mapper.Map<Member>(dto);            
            var id = await _repository.CreateAsync(member);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMemberDto dto)
        {
            var member = _mapper.Map<Member>(dto);
            member.Id = id;
            var updated = await _repository.UpdateAsync(member);

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