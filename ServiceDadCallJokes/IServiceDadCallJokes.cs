namespace ServiceDadCallJokes
{
    public interface IServiceDadCallJokes
    { 
        Task<string> GetRandomJokeAsync();
        Task<Dictionary<string,List<string>>> GetJokesAsync(string searchTerm);
    }
}