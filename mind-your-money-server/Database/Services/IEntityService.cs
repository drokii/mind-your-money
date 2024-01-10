namespace mind_your_domain.Database.Services;

public interface IEntityService<T>
{
    Task<List<T>> GetAll();
    Task Create(T toBeCreated);
    Task<T?> FindById(Guid id);
    Task Remove(T toBeRemoved);
}