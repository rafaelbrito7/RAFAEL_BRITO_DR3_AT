using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FriendshipService
    {
        private FriendshipRepository repository;

        public FriendshipService(FriendshipRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Friendship>> GetAll()
        {
            var Friendship = await repository.GetAll();
            return Friendship;
        }

        public async Task<Friendship> GetOneById(Guid id)
        {
            var Friendship = await repository.GetOneFriendshipById(id);
            return Friendship;
        }

        public async Task<int> Create(Friendship friendship)
        {
            var result = await repository.Create(friendship);
            return result;
        }

        public async Task<int> Delete(Guid id)
        {
            var result = await repository.Delete(id);
            return result;
        }
    }
}
