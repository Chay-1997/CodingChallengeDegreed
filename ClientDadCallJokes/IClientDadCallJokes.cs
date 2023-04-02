using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientDadCallJokes.Interface
{
    public interface IClientDadCallJokes
    {
        Task<string> GetRandomJokeAsync();
        Task<Dictionary<string,List<string>>> GetJokesAsync(string searchTerm);
    }
}
