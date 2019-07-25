using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
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

            string message = "Hello ";

            int counter = people.Count() - 2;
            
            foreach (Person person in people)
            {
                message += person.Name;

                if (counter > 0)
                {
                    message += ", ";
                }

                if (counter == 0)
                {
                    message += " and ";
                }

                if (counter < 0)
                {
                    message += " ";
                }

                counter--;
            }
            message += $"- the time on the server is {date.ToString("t")} on {date.ToString("dd MMMM yyyy")}.";

            return message;
        }
    }
}