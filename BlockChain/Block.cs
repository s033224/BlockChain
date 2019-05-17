using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain
{
    public class Block
    {
        public string previousHash { get; set; }
        public int data { get; set; }

        public Block() { }

        public Block(string previousHash, int vote)
        {
            this.previousHash = previousHash;
            this.data = vote;
        }

        public override string ToString()
        {
            return previousHash + data;
        }

    }
}
