namespace lvcksketch.UseCases.GetMapNames.Interfaces;

public interface IGetMapNames
{
    public Task<IEnumerable<string>> ExecuteAsync();
}