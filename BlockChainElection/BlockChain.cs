using BlockChain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainElection
{
    public class BlockChain
    {
        private List<User> users { get; set; }

        public BlockChain(int userCount)
        {
            users = new List<User>();
            populateUsers(userCount);
        }

        private void populateUsers(int ammount)
        {
            for(int i = 0; i < ammount; i++)
            {
                User user = new User();
                users.Add(user);
            }
        }
    }
}
