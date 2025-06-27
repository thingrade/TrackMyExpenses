namespace TrackMyExpenses.API.Common.Interfaces
{
    public interface ICrudService<TDto, TCreateDto, TUpdateDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
		Task<TDto?> GetByIdAsync(Guid id);
		Task<TDto> CreateAsync(TCreateDto dto);
		Task UpdateAsync(Guid id, TUpdateDto dto);
		Task DeleteAsync(Guid id);
    }
}