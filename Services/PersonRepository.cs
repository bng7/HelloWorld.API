using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelloWorld.API.Models;
using HelloWorld.API.Services;
using HelloWorld.API.Data;

namespace HelloWorld.API.Services
{
    public class PersonRepository : IPersonRepository
    {
        protected AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;

            if (_context.People.Count() == 0)
            {
                _context.People.Add(new Person { Name = "Bazza B"});
                _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Person>> ListPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            return await _context.People.FindAsync(id);
        }

        public async Task AddPersonAsync(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonAsync(string name, Person person)
        {
            var personEntry = _context.People.First(p => p.Name == name);
            personEntry.Name = person.Name;
            await _context.SaveChangesAsync();
        }
    }
}