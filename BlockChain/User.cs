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
        public delegate Hashtable Syncblocks();
        public Syncblocks syncBlocks;

        public Hashtable blocks { get; set; }
        private Block lastAddedBlock { get; set; }

        public User()
        {
            blocks = new Hashtable();
            blocks.Add("startBlock", new Block(null, -1));
            lastAddedBlock = (Block)blocks["startBlock"];
        }

        public User(Hashtable blocks)
        {
            blocks = new Hashtable();
            this.blocks = blocks;
        }


        public void addData(int value)
        {
            Block block = generateBlock(value);
            if(verifyBlocks())
            {
                lastAddedBlock = block;
                blocks.Add(objectToStringHash(block), block);
            }else
            {
                blocks = syncBlocks?.Invoke();
                if (blocks != null)
                    addData(value);
            }
        }

        private Block generateBlock(int value)
        {
            return new Block(objectToStringHash(lastAddedBlock), value);
        }

        public bool verifyBlocks()
        {
            Block block = lastAddedBlock;
            while(!String.IsNullOrEmpty(block.previousHash))
            {
                if(blocks.Count > 1)
                {
                    //Hash table has this object
                    if (blocks.ContainsKey(objectToStringHash(block)))
                    {
                        //Hash table must have previous object
                        //if (!blocks.ContainsKey(objectToStringHash((Block)blocks[block.previousHash])))
                            //return false;
                        block = (Block)blocks[block.previousHash];
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            return true;                
        }


        private static readonly Object locker = new Object();

        public static string objectToStringHash(Block objectToSerialize)
        {
            return Hash.GetHashString(objectToSerialize.ToString());
        }

    }
}
