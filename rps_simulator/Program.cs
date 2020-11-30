using System;
using System.Linq;

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
                new gameChoice("rock", new string[]{"scissors", "lizard"}, new string[]{"paper", "spock"}),
                new gameChoice("paper", new string[]{"rock", "spock"}, new string[]{"scissors", "lizard"}),
                new gameChoice("scissors", new string[]{"paper", "lizard"}, new string[]{"rock", "spock"}),
                new gameChoice("lizard", new string[]{"paper", "spock"}, new string[]{"rock", "scissors"}),
                new gameChoice("spock", new string[]{"rock", "scissors"}, new string[]{"scissors", "lizard"})
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
            Console.WriteLine();

            //create score keeping method
            int rounds = 0;
            int wins = 0;
            int draws = 0;
            int[] choiceUses = new int[names.Length];
            //accept user choice and process result
            Boolean done = false;
            while (!done)
            {

                string userChoiceName = Console.ReadLine();
                int choicePosition = Array.IndexOf(names, userChoiceName);
                if (choicePosition > -1)
                {
                    choiceUses[choicePosition]++;
                    rounds++;
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
                            wins++;
                            break;
                        case 0:
                            //draw
                            Console.WriteLine("It's a draw, you both picked {0}", userChoice.Name);
                            draws++;
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
                else if (userChoiceName == "done")
                {
                    //user is done with the game
                    done = true;
                }
                else
                {
                    //some sort of error idk
                    Console.WriteLine("Invalid input, please try again");
                }
            }
            Console.WriteLine("You won {0} rounds out of {1} rounds with {2} draws", wins, rounds, draws);
            Console.WriteLine();
            Console.WriteLine("Overall Result:");
            if (wins > ((rounds - draws) - wins))
            {
                //player wins
                Console.WriteLine("YOU WIN!!!!! You should go pro with that performance");
            }
            else if (wins < ((rounds - draws) - wins))
            {
                //machine wins
                Console.WriteLine("you lose........noob");
            }
            else
            {
                //draw
                Console.WriteLine("It's a draw? Pretty anticlimactic but oh well.");
            }
            Console.WriteLine();
            int maxPosition = Array.IndexOf(choiceUses, choiceUses.Max());
            Console.WriteLine("Your most picked option was {0}, which you chose {1} out of {2} times", names[maxPosition], choiceUses[maxPosition], rounds);
            Console.ReadLine();
        }
    }
}
