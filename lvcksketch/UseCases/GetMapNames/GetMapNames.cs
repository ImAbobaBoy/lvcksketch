using lvcksketch.Data;
using lvcksketch.UseCases.GetMapNames.Interfaces;

namespace lvcksketch.UseCases.GetMapNames;

public class GetMapNames : IGetMapNames
{
    public Task<IEnumerable<string>> ExecuteAsync()
    {
        var maps = new List<string>
        {
            MapNames.Hellas
        };

        return Task.FromResult<IEnumerable<string>>(maps);
    }
}