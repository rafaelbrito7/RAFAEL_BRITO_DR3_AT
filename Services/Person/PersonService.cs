using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PersonService
    {

        private PersonRepository repository;

        public PersonService(PersonRepository repository) {
            this.repository = repository;
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            var People = await repository.GetAll();
            return People;
        }

        public async Task<Person> GetOneById(Guid id) 
        {
            var Person = await repository.GetOneById(id);
            return Person;
        }

        public async Task<int> Create(Person person)
        {
            var result = await repository.Create(person);
            return result;
        }

        public async Task<int> Update(Person person)
        {
            var result = await repository.Update(person);
            return result;
        }

        public async Task<int> Delete(Guid id) {
            var result = await repository.Delete(id);
            return result;
        }

        public async Task<int> GetCount()
        {
            var result = await repository.GetCount();
            return result;
        }
    }
}
