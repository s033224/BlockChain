using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public class User
    {
        public delegate Hashtable requestBlocks();
        public requestBlocks getBlocks;

        public Hashtable blocks { get; set; }

        public User()
        {
            blocks = new Hashtable();
            Block block = new Block(null, -1);
            blocks.Add(block.ToString(), block);
        }

        public void addBlock(Block block)
        {
                blocks.Add(block.ToString(), block);
        }

        public void getSyncedBlocks()
        {
            Hashtable tempBlocks = getBlocks.Invoke();
            if(tempBlocks!=null)
            {
                blocks = new Hashtable();
                foreach (DictionaryEntry de in tempBlocks)
                {
                    blocks.Add(de.Key, de.Value);
                }
            }
        }

        public void changeBlockValueByID(int ID, int value)
        {
            int currID = 0;
            Block block = new Block();
            foreach (DictionaryEntry de in blocks)
            {
                if (currID == ID)
                {
                    block = ((Block)de.Value);
                }                    
                currID++;
            }
            
            Block temp = new Block();
            temp.previousHash = block.previousHash;
            temp.data = value;
            blocks.Remove(block.ToString());
            blocks.Add(temp.ToString(), temp);
        }
        
        public bool verifyBlocks(Block lastBlock)
        {
            Block block = lastBlock;
            while(block.previousHash != null)
            {
                if (!blocks.ContainsKey(block.ToString()))
                    return false;
                block = (Block)blocks[block.previousHash];
                if (block == null)
                    return false;
            }
            return true;
        }

        public override string ToString() // This IS SPARTĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄĄ ĄĄĄĄĄĄĄĄĄĄĄĄĄĄ !!!!!!!!!!!!!!!!!!!!!!!!!!
        {
            string s = "";
            foreach(DictionaryEntry block in blocks)
            {
                s += $"\n{((Block)block.Value).ToString()} - {((Block)block.Value).previousHash} -  {((Block)block.Value).data}";
            }
            return s;
        }
    }
}
