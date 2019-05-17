    using System;
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
            BlockChainManager bc = new BlockChainManager(10);

            contentController();

            while (true)
            {
                bc.addData(randVote());
            }
        }

        private static void contentController()
        {
            while(true)
            {
                Console.Clear();
                drawMAIN();
                choiceController(int.Parse(Console.ReadLine()));
                
                Console.ReadKey();
            }
        }

        private static void choiceController(int ID)
        {

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
