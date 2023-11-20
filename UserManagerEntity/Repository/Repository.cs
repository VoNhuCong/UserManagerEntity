using System.Linq.Expressions;

namespace UserManagerEntity.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity? Get(params object?[]? id);
    Task<TEntity?> GetPlainAsync(params object?[]? id);
    Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    string Add(TEntity entity);
    string AddRange(IEnumerable<TEntity> entities);

    string Remove(TEntity entity);
    string RemoveRange(IEnumerable<TEntity> entities);
}
public class Repository <TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _entities;
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
        Context = context;
        _entities = Context.Set<TEntity>();
    }

    public virtual TEntity? Get(params object?[]? id)
    {
        return _entities.Find(id);
    }
    public async Task<TEntity?> GetPlainAsync(params object?[]? id)
    {
        return await _entities.FindAsync(id);
    }
    public virtual async Task<TEntity?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _entities.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _entities.ToListAsync(cancellationToken);
    }
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _entities.CountAsync(predicate, cancellationToken);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _entities.Where(predicate);
    }

    public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.SingleOrDefaultAsync(predicate);
    }

    public string Add(TEntity entity)
    {
        try
        {
            _entities.Add(entity);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return "";
        
    }

    public string AddRange(IEnumerable<TEntity> entities)
    {
        try
        {
            _entities.AddRange(entities);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return "";
    }

    public string Remove(TEntity entity)
    {
        try{
            _entities.Remove(entity);
            return "";
        }
        catch(Exception ex){
            return ex.Message;
        }
    }

    public string RemoveRange(IEnumerable<TEntity> entities)
    {
        try
        {
            _entities.RemoveRange(entities);
            return "";
        }
        catch (Exception ex) {
            return ex.Message;
        }
    }
}
