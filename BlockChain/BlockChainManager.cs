using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public class BlockChainManager
    {
        public List<User> users { get; set; }
        private List<int> dataProcessingList { get; set; }

        public BlockChainManager()
        {
            users = new List<User>();
            dataProcessingList = new List<int>();
        }

        public BlockChainManager(int userCount)
        {
            users = new List<User>();
            dataProcessingList = new List<int>();
            populateUsers(userCount);
        }

        public void populateUsers(int ammount)
        {
            for (int i = 0; i < ammount; i++)
            {
                User user = new User();
                user.syncBlocks += syncTable;
                users.Add(user);
            }
        }

        public Hashtable syncTable()
        {
            foreach(User user in users)
            {
                if (user.verifyBlocks())
                    return user.blocks;
            }
            return null;
        }

        public void addData(int vote)
        {
            dataProcessingList.Add(vote);
            addDataToBlockchain();
        }

        private void addDataToBlockchain()
        {
            lock(dataProcessingList)
            {
                foreach(User user in users)
                {
                    user.addData(dataProcessingList[0]);
                }
                dataProcessingList.RemoveAt(0);
            }
        }
    }
}
