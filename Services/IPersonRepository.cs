using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorld.API.Models;

namespace HelloWorld.API.Services
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> ListPeopleAsync();
        Task<Person> GetPersonAsync(int id);
        Task AddPersonAsync(Person person);
        Task UpdatePersonAsync(string name, Person newPerson);
        Task DeletePersonAsync(string name);
    }
}