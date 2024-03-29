using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloWorld.API.Models;
using HelloWorld.API.Services;

namespace HelloWorld.API.Controllers
{
    [Route("/api/[controller]")]
    public class PeopleController : ControllerBase
    {
        protected IPersonRepository _personRepository;
        protected IMessageBuilder _messageBuilder;

        public PeopleController(IPersonRepository personRepository, IMessageBuilder messageBuilder)
        {
            _personRepository = personRepository;
            _messageBuilder = messageBuilder;
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> ListPeopleAsync()
        {
            return await _personRepository.ListPeopleAsync();
        }

        [HttpGet("greeting")]
        public async Task<string> GetMessageAsync()
        {
            return await _messageBuilder.BuildMessage();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersonAsync(int id)
        {
            var result = await _personRepository.GetPersonAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonAsync([FromBody] Person person)
        {
            if  (!ModelState.IsValid)
            {
                return BadRequest("Body format incorrect.");
            }

            if (_personRepository.CheckPresence(person.Name))
            {
                return BadRequest($"Add failed, {person.Name} already exists.");
            }
            
            await _personRepository.AddPersonAsync(person);

            return CreatedAtAction("AddPersonAsync", new { name = person.Name }, person);

        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdatePersonAsync(string name, [FromBody] Person newPerson)
        {
            if  (!ModelState.IsValid)
            {
                return BadRequest("Body format incorrect.");
            }

            await _personRepository.UpdatePersonAsync(name, newPerson);

            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeletePersonAsync(string name)
        {
            if (name == "Bazza")
            {
                return BadRequest("Bazza cannot be deleted.");
            }

            await _personRepository.DeletePersonAsync(name);

            return NoContent();
        }
    }
}