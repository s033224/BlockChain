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
            blocks.Add("FirstBlock", block);
        }

        public bool addBlock(Block block)
        {
            if(verifyBlocks())
            {
                blocks.Add(block.ToString(), block);
                return true;
            }
            return false;
        }

        public void getSyncedBlocks()
        {
            Hashtable tempBlocks = getBlocks.Invoke();
            if (tempBlocks != null)
            {
                blocks = new Hashtable();
                foreach (DictionaryEntry de in tempBlocks)
                {
                    blocks.Add(de.Key, de.Value);
                }
            }
        }

        public Block getBlockByID(int ID)
        {
            int currID = 0;
            foreach(DictionaryEntry de in blocks)
            {
                if (currID == ID)
                    return (Block)de.Value;
                currID++;   
            }
            return null; 
        }
        
        public bool verifyBlocks()
        {
            int fake = 0;
            foreach(DictionaryEntry de in blocks)
            {
                if(de.Key != null)
                {
                    if (!blocks.Contains(((Block)de.Value).ToString()))
                    {
                        fake++;
                    }
                }
            }

            if (fake > 1)
                return false;
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
