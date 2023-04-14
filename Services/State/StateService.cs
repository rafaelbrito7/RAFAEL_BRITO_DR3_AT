using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StateService
    {
        private StateRepository repository;

        public StateService(StateRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<State>> GetAll()
        {
            var State = await repository.GetAll();
            return State;
        }

        public async Task<State> GetOneById(Guid id)
        {
            var State = await repository.GetOneById(id);
            return State;
        }

        public async Task<int> Create(State state)
        {
            var result = await repository.Create(state);
            return result;
        }

        public async Task<int> Update(State state)
        {
            var result = await repository.Update(state);
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
