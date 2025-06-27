using AutoMapper;
using TrackMyExpenses.API.Common.Interfaces;
using TrackMyExpenses.API.Domain.Common;

namespace TrackMyExpenses.API.Common.Services
{
    public class CrudService<TDto, TEntity, TCreateDto, TUpdateDto> 
    : ICrudService<TDto, TCreateDto, TUpdateDto>
    where TEntity : BaseEntity
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public CrudService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }

        public async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity is null ? default! : _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> CreateAsync(TCreateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            return _mapper.Map<TDto>(entity);
        }

        public async Task UpdateAsync(Guid id, TUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null) throw new KeyNotFoundException($"Entity with ID {id} not found.");

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity is null) throw new KeyNotFoundException($"Entity with ID {id} not found.");

            await _repository.SoftDeleteAsync(entity);
        }
    }
}