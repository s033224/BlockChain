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

        public Block(string previousHash, int data)
        {
            this.previousHash = previousHash;
            this.data = data;
        }

        public override string ToString()
        {
            return Hash.GetHashString($"PreviousHash:{previousHash}\n{data}");
        }

    }
}
