using StaplePuck.Data;

public class UserContext : Dictionary<string, object>
{
    public UserContext(StaplePuckContext context) =>
        DbContext = context;

    public readonly StaplePuckContext DbContext;
}