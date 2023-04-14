using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountryService
    {
        private CountryRepository repository;

        public CountryService(CountryRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IEnumerable<Country>> GetAll()
        {
            var Country = await repository.GetAll();
            return Country;
        }

        public async Task<Country> GetOneById(Guid id)
        {
            var Country = await repository.GetOneById(id);
            return Country;
        }

        public async Task<int> Create(Country country)
        {
            var result = await repository.Create(country);
            return result;
        }

        public async Task<int> Update(Country country)
        {
            var result = await repository.Update(country);
            return result;
        }

        public async Task<int> Delete(Guid id)
        {
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
