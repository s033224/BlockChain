using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockChain
{
    public class BlockChainManager
    {
        public List<User> users { get; set; }
        private Block lastBlock { get; set; }
        private List<int> dataProcessingList { get; set; }

        public BlockChainManager(int peers)
        {
            users = new List<User>();
            dataProcessingList = new List<int>();
            populateUsers(peers);
            foreach(DictionaryEntry de in users[0].blocks)
            {
                lastBlock = (Block)de.Value;
            }
            
        }

        public void populateUsers(int ammount)
        {
            for (int i = 0; i < ammount; i++)
            {
                User user = new User();
                user.getBlocks += syncTable;
                users.Add(user);
            }
        }

        public List<int> getResults()
        {
            List<int> results = new List<int>();
            foreach(User user in users)
            {
                if(user.verifyBlocks(lastBlock))
                {
                    Block block = lastBlock;
                    while(block != null)
                    {
                        if (block.previousHash == null)
                            return results;
                        results.Add(block.data);
                        block = (Block)user.blocks[block.previousHash];
                    }
                }
            }
            return null;
        }

        public void changeData(User user, int blockNum, int changeTo)
        {
            
        }

        public Hashtable syncTable()
        {
            foreach(User user in users)
            {
                if (user.verifyBlocks(lastBlock))
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
                int val = dataProcessingList[0];
                Block block = new Block(lastBlock.ToString(), val);
                foreach (User user in users)
                {
                    if(user.verifyBlocks(lastBlock))
                    {
                        user.addBlock(block);
                    }
                    else
                    { 
                        MessageBox.Show("Block was incorrect... SYNCING...");
                        user.getSyncedBlocks();
                        try
                        {
                            user.addBlock(block);
                        }
                        catch { }
                    }
                }
                lastBlock = block;
                dataProcessingList.RemoveAt(0);
            }
        }
    }
}
