using System;

namespace Stregsystem
{
    class Program
    {
        static void Main(string[] args)
        {
            IStregsystem stregsystem = new Stregsystem();
            stregsystem.ImportCsv();
            

            IStregsystemUI stregsystemCLI = new StregsystemCLI(stregsystem);
            StregsystemCommandParser controller = new StregsystemCommandParser(stregsystemCLI, stregsystem);
            stregsystemCLI.Start();
        }
    }
}
