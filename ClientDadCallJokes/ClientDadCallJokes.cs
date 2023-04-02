using ClientDadCallJokes.Interface;
using ServiceDadCallJokes;

namespace ClientDadCallJokes
{
    public class ClientDadCallJokes : IClientDadCallJokes
    {
        private IServiceDadCallJokes serviceDadCallJokes;
        public ClientDadCallJokes() 
        {
            serviceDadCallJokes = new ServiceDadCallJokes.ServiceDadCallJokes();
        
        }
        public async Task<Dictionary<string, List<string>>> GetJokesAsync(string searchTerm)
        {
            return await serviceDadCallJokes.GetJokesAsync(searchTerm);
        }

        public async Task<string> GetRandomJokeAsync()
        {
           return await serviceDadCallJokes.GetRandomJokeAsync();
        }
        
    }

}