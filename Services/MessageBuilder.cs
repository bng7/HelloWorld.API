using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using HelloWorld.API.Models;
using HelloWorld.API.Services;

namespace HelloWorld.API.Services
{
    public class MessageBuilder : IMessageBuilder
    {
        protected IPersonRepository _repository;

        public MessageBuilder(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> BuildMessage()
        {
            IEnumerable<Person> people = await _repository.ListPeopleAsync();
            DateTime date = DateTime.Now;

            StringBuilder message = new StringBuilder("Hello ", 50);
            
            foreach (Person person in people)
            {
                message.Append(person.Name);
                message.Append(", ");
            }
            message.Append($"it is currently {date}.");

            return message.ToString();
        }
    }
}