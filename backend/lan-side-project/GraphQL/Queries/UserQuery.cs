using lan_side_project.Data;
using lan_side_project.Models;

namespace lan_side_project.GraphQL.Queries;

[ExtendObjectType("Query")]
public class UserQuery
{
    [UsePaging(IncludeTotalCount = true)]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers([Service] AppDbContext dbContext) => dbContext.Users.AsQueryable();
    
}