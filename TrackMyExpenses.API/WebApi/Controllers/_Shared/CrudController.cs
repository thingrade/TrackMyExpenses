using Microsoft.AspNetCore.Mvc;
using TrackMyExpenses.API.Common.Interfaces;

namespace TrackMyExpenses.API.WebApi.Controllers.Base
{
    public abstract class CrudController<TDto, TCreateDto, TUpdateDto> : ControllerBase
    {
        private readonly ICrudService<TDto, TCreateDto, TUpdateDto> _service;

        protected CrudController(ICrudService<TDto, TCreateDto, TUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = GetIdFromDto(created) }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, TUpdateDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // Optional helper (override in child if needed)
        protected virtual Guid GetIdFromDto(TDto dto) => default;
    }
}