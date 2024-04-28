using AutoMapper;
using Dapper;
using MinimalApplication.Domain.Entities.Category;
using MinimalApplication.Infrastructure.Data;

namespace MinimalApplication.Infrastructure.Repositories;


public class CategoryRepository : ICategoryRepository
{
    private DbSession _session;
    private readonly IMapper _mapper;

    public CategoryRepository(DbSession session, IMapper mapper)
    {
        _session = session;
        _mapper = mapper;
    }
    public async Task<int> Create(CategoryEntity category)
    {
        
        var sql = "INSERT INTO Categories(name, description, created) VALUES(@Name, @Description,  @Created) returning id";

        var parameters = new { Name = category.Name, Description = category.Description, Created = DateTime.UtcNow };

        using (var connection = _session.Connection)
        {
            var result = await connection.QueryFirstAsync<int>(sql, parameters, null);
            return result;
        }
    }

    public async Task<IEnumerable<CategoryEntity>> GetAll()
    {
        List<CategoryEntity> categories = new List<CategoryEntity>();
        using (var connection = _session.Connection)
        {
            var reader = await connection.ExecuteReaderAsync("SELECT id, name, description FROM Categories");

            while (reader.Read())
            {
                categories.Add(
                    new CategoryEntity(
                        id : reader.GetInt32(0),
                        name: reader.GetString(1),
                        description: reader.GetString(2)
                        )
                    );  
            }
        }

        return categories;

    }

    public async Task<CategoryEntity> GetById(int? id)
    {
        CategoryEntity? category = null;
        var sql = " SELECT id, name, description FROM Categories WHERE id=@id ";

        var parameters = new { id  = id};

        using (var connection = _session.Connection)
        {
            var reader = await connection.ExecuteReaderAsync(sql, parameters, null);

            while (reader.Read())
            {
                category = new CategoryEntity(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
            }
        }

        return category;
    }

    public async Task<bool> Remove(CategoryEntity category)
    {
       
        var sql = @"
                    DELETE 
                    FROM Categories
                    WHERE id = @id ";

        var parameters = new { id = category.Id };

        using (var connection = _session.Connection)
        {
            var execute = await connection.ExecuteAsync(sql, parameters, null);
            if (execute == 1)
            {
                return true;
            }
            else
            {
                throw new Exception("Não foi possível excluir a Categoria!");
            }
        }
    }

    public async Task<bool> Update(CategoryEntity category)
    {
       var sql = @"
                UPDATE Categories
                    SET name = @Name,
                        description  = @Description,
                        updated = @Updated
                    WHERE id = @id ";


        var parameters = new { id = category.Id, Name = category.Name, Description = category.Description, Updated = DateTime.UtcNow };

        using (var connection = _session.Connection)
        {
            var execute = await connection.ExecuteAsync(sql, parameters, null);

            if (execute==1)
            {
                return true;
            }
            else
            {
                throw new Exception("Não foi possível atualizar a Categoria!");
            }
        }

    }
}
