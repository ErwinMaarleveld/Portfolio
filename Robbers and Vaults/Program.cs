using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Timers;


namespace Robbers
{
    public class Vault{
        public string Code {get; set;}

        public Vault(){
            Code = createCode(Code);
        }

        public string createCode(string Code){
            List<string> numbers = new List<string>(){"0", "1", "2", "3"/*,"4","5", "6", "7", "8", "9"*/}; //need to remove comments for finished product but its made short for testing
            List<string> vowels = new List<string>(){"A", "E", "O", "I", "U"};


            numbers = ShuffleList<string>(numbers);
            vowels = ShuffleList<string>(vowels);

            numbers.AddRange(vowels);
            string tempCode = "";
            foreach (var number in numbers)
            {
                tempCode += number;
            }
            return tempCode;
        }

        
        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }
            return randomList; //return the new random list
        }
    }

    public class Robber{

        public int RobberNumber {get; set;}
        public Vault CurrentVault {get; set;}
        public List<string> allPossibleNumberCombinations {get; set;}
        public List<string> allPossibleVowelCombinations {get; set;}

        public Robber(int numberOfRobber, Vault vault, List<string> numberCombinations, List<string> vowelCombinations){
            RobberNumber = numberOfRobber;
            CurrentVault = vault;
            allPossibleNumberCombinations = numberCombinations;
            allPossibleVowelCombinations = vowelCombinations;
        }

        public void CrackVault(Vault[] allVaults){
            string tempCombination = "";
            for (int i = 0; i < allPossibleNumberCombinations.Count; i++)
            {
                for (int j = 0; j < allPossibleVowelCombinations.Count; j++)
                {
                    tempCombination = allPossibleNumberCombinations[i] + allPossibleVowelCombinations [j];
                    if (tempCombination == CurrentVault.Code)
                    {
                        Console.WriteLine("The code has been cracked! " + CurrentVault.Code);
                        Console.WriteLine("tempCombination = " + tempCombination);
                        for (int a = 0; a < allVaults.Count(); a++)
                        {
                            allVaults[(Array.IndexOf(allVaults, tempCombination))-1].Code = "";
                            
                        }
                    }
                }
            }
        }


    }

    class Program
    {
        static void Main(string[] args)
        {

            int numberOfRobbers = 1;
            int numberOfVaults = 1;
            List<string> allPossibleNumberCombinations = createListPossibleCombinations("0123");  //Needs to be "0123456789" but for it is made short for testing
            List<string> allPossibleVowelCombinations = createListPossibleCombinations("AEOIU");
            Vault[] allVaults = new Vault[numberOfVaults];
            Robber[] allRobbers = new Robber[numberOfRobbers];

            //creating all vaults
            for (int i = 0; i < numberOfVaults; i++){
                allVaults[i] = new Vault();
            }

            //creating all robbers and giving them a first vault if possible
            for (int i = 0; i < numberOfRobbers; i++)
            {
                try
                {
                    allRobbers[i] = new Robber(i, allVaults[i], allPossibleNumberCombinations, allPossibleVowelCombinations);
                    
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("There are more robbers then vaults!");
                    break;
                    throw;
                }
            }

            allRobbers[0].CrackVault(allVaults);

            Console.WriteLine(allVaults[0].Code);
            Console.WriteLine(allRobbers[0].CurrentVault.Code);
                        
            List<string> createListPossibleCombinations(string avalibleCharacters){
            List<string> tempList = new List<string>();
            string alphabet = avalibleCharacters;
            var q = alphabet.Select(x => x.ToString());
            int size = alphabet.Length;
            for (int i = 0; i < size - 1; i++){
                q = q.SelectMany(x => alphabet, (x, y) => x + y);
            }
            
            foreach (var item in q){
                tempList.Add(item);
                Console.WriteLine(item);
            }
            return tempList;
        }

        }

        

    }
}
