using System;

namespace rps_simulator
{
    class gameChoice
    {
        private string _name;
        private string[] _winsAgainst;
        private string[] _losesAgainst;

        public gameChoice(string name, string[] winsAgainst, string[] losesAgainst)
        {
            _name = name;
            _winsAgainst = winsAgainst;
            _losesAgainst = losesAgainst;

        }

        public string Name
        {
            get { return _name;  }
        }

        public string[] WinsAgainst
        {
            get { return _winsAgainst; }
        }

        public string[] LosesAgainst
        {
            get { return _losesAgainst; }
        }
    }
    class Program
    {
        public static int findResult(gameChoice playerChoice, gameChoice machineChoice)
        {
            string machineChoiceName = machineChoice.Name;
            int winPosition = Array.IndexOf(playerChoice.WinsAgainst, machineChoiceName);
            int losePosition = Array.IndexOf(playerChoice.LosesAgainst, machineChoiceName);
            if (winPosition >- 1)
            {
                return 1;
            }
            else if (losePosition > -1)
            {
                return -1;
            }
            else if (machineChoiceName == playerChoice.Name)
            {
                return 0;
            }
            return 2;
        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            //instantiate game choices
            gameChoice[] gameChoices = {
                new gameChoice("rock", new string[]{"scissors"}, new string[]{"paper"}),
                new gameChoice("paper", new string[]{"rock"}, new string[]{"scissors"}),
                new gameChoice("scissors", new string[]{"paper"}, new string[]{"rock"})
            };

            //write out options to console
            string[] names = new string[gameChoices.Length];
            for (int i = 0; i < gameChoices.Length; i++)
            {
                names[i] = gameChoices[i].Name;
            }

            Console.Write("Please enter an option between ");
            for(int i = 0; i < names.Length -1; i++)
            {
                Console.Write("{0}, ", names[i]);
            }
            Console.Write("{0} ", names[names.Length-1]);
            
            //accept user choice and process result
            Boolean done = false;
            while (!done)
            {
                Console.WriteLine();

                string userChoiceName = Console.ReadLine();
                int choicePosition = Array.IndexOf(names, userChoiceName);
                if (choicePosition > -1)
                {
                    gameChoice userChoice = gameChoices[choicePosition];
                    //valid entry, generate machine's choice, output it and compare results
                    gameChoice machineChoice = gameChoices[rnd.Next(0, gameChoices.Length)];
                    Console.WriteLine("The very smart machine chose {0}", machineChoice.Name);

                    int result = findResult(userChoice, machineChoice);
                    switch (result)
                    {
                        case -1:
                            //player loses
                            Console.WriteLine("You win! {0} beats {1}", userChoice.Name, machineChoice.Name);
                            break;
                        case 0:
                            //draw
                            Console.WriteLine("It's a draw, you both picked {0}", userChoice.Name);
                            break;
                        case 1:
                            //player wins
                            Console.WriteLine("You lose! {0} beats {1}", machineChoice.Name, userChoice.Name);
                            break;
                        case 2:
                            //error
                            Console.WriteLine("Error, something wrong happened");
                            break;
                    }
                }
            }
        }
    }
}
