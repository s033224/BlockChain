    using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChain;

namespace BlockChainElection
{
    class Program
    {
        static void Main(string[] args)
        {
            contentController();
        }

        private static void contentController()
        {
            BlockChainManager bcm = new BlockChainManager(10);
            while(true)
            {
                Console.Clear();
                Console.WriteLine(drawMAIN());
                choiceController(int.Parse(Console.ReadLine()), bcm);
                Console.ReadKey();
            }
        }

        private static void choiceController(int ID, BlockChainManager bcm)
        {
            switch(ID)
            {
                case 1:
                    {
                        bcm.addData(1);
                        break;
                    }

                case 2:
                    {
                        bcm.addData(2);
                        break;
                    }
                case 3:
                    {
                        List<int> results = bcm.getResults();
                        Console.WriteLine(extractResults(results));
                        break;
                    }
                case 4:
                    {
                        printUsers(bcm);
                        Console.WriteLine("Select user: ");
                        int userID = int.Parse(Console.ReadLine()) -1;
                        Console.WriteLine("\n"+ bcm.users[userID].ToString());
                        int blockID = int.Parse(Console.ReadLine()) -1;
                        Block block = bcm.users[userID].getBlockByID(blockID);
                        block.data = 420;
                        break;
                    }
                default:
                    {
                        Environment.Exit(0);
                        break;
                    }
            }
        }

        private static void printUsers(BlockChainManager bcm)
        {
            for(int i = 0; i < bcm.users.Count; i++)
            {
                Console.WriteLine($"User: {i + 1}");
            }
        }

        private static String extractResults(List<int> values)
        {
            Hashtable votes = new Hashtable();
            foreach(int value in values)
            {
                if(votes.ContainsKey(value))
                {
                    votes[value] = (int)votes[value] + 1;
                }else
                {
                    votes.Add(value, 1);
                }
            }
            Console.WriteLine();

            List< DictionaryEntry > dictionaryEntries= new List<DictionaryEntry>();
            dictionaryEntries = votes.Cast<DictionaryEntry>().OrderByDescending(entry => entry.Value).ToList();
            
            string result = "Balsavimo rezultatai:\n";
            foreach(DictionaryEntry value in dictionaryEntries)
            {
                result += $"#{value.Key} gavo {value.Value} balsu\n";
            }
            return result;
        }

        private static string drawMAIN()
        {
            return ("Kandidatai:\n" +
                "1. Ingrida Simonytė\n" +
                "2. Gitanas Nauseda\n"+
                "3. Rezultatai\n" +
                "4. Pakeisti vartotojo balsa\n"+
                "5. Baigti"+
                "Pasirinkite: ");
        }

        private static int randVote()
        {
            Random random = new Random();
            return random.Next(1, 5);
        }
    }
}
