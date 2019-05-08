using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Timers;


namespace Robbers
{
    public class Vault{
        public string Code {get; set;}
        public int Status {get; set;}   // -1 = Vault is cracked, 0 = Free vault, 1 = Robber is working on this vault

        public Vault(){
            Code = createCode(Code);
            Status = 0;
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

        
        public Vault CurrentVault {get; set;}
        public List<string> allPossibleNumberCombinations {get; set;}
        public List<string> allPossibleVowelCombinations {get; set;}

        public Robber(Vault vault, List<string> numberCombinations, List<string> vowelCombinations){
            CurrentVault = vault;
            allPossibleNumberCombinations = numberCombinations;
            allPossibleVowelCombinations = vowelCombinations;
        }

        public void CrackVault(){
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
                        CurrentVault.Status = -1; 
                        break;
                    }
                }
            }
        }

        public void processRobber(Vault[] allVaults){
                try
                    {
                        if (CurrentVault.Status == 1)
                        {
                            Console.WriteLine("Cracking current vault");
                            CrackVault();
                        }
                        
                        if (CurrentVault.Status == -1)
                        {
                            Console.WriteLine("This vault is cracked!");
                            
                            for (int i = 0; i < allVaults.Count(); i++)
                            {
                                if (allVaults[i].Status == 0)
                                {
                                    Console.WriteLine("Getting new vault");
                                    CurrentVault = allVaults[i];
                                    CurrentVault.Status = 1;
                                    Console.WriteLine("Next vault code is: " + CurrentVault.Code);
                                    //Console.WriteLine("Starting proseccing again");
                                    //processRobber(allVaults);
                                    break;
                                }
                            }

                        }  
                    }
                    catch (System.NullReferenceException)
                    {
                        Console.WriteLine("Object is not an instance, fixing  it later");
                    }
            }


    }

    class Program
    {
        static void Main(string[] args)
        {

            bool allVaultsCracked = false;
            int totalVaultsCracked = 0;
            int numberOfRobbers = 3;
            int numberOfVaults = 2;
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
                    for (int p = 0; p < allVaults.Count(); p++)
                    {
                        if (allVaults[p].Status == 0)
                        {
                            Console.WriteLine("Current robber number: " + i);
                            allRobbers[i] = new Robber(allVaults[p], allPossibleNumberCombinations, allPossibleVowelCombinations);
                            allVaults[p].Status = 1;
                            break;
                        } 
                    }
         
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("There are more robbers than available vaults!");
                    break;
                    
                }
            }
            
            
                
            while (!allVaultsCracked)
            {
                
            

                for (int x = 0; x < allRobbers.Count(); x++)
                {
                    if (allRobbers[x] != null)
                    {
                        Console.WriteLine("Current Robber: " + x);
                        allRobbers[x].processRobber(allVaults);
                    }
                    
                    
                }
                
                totalVaultsCracked = 0;
                for (int p = 0; p < allVaults.Count(); p++)
                {   
                    if (allVaults[p].Status == -1)
                    {
                        totalVaultsCracked++;
                        
                        
                    }
                }
                Console.WriteLine("Total vaults cracked: " + totalVaultsCracked);

                if (totalVaultsCracked == allVaults.Count())
                {
                    allVaultsCracked = true;
                }

                
            }

            

            
                                   
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
                }
                return tempList;
            }

        //var C = int.Parse(inputs[0]);
        //var N = int.Parse(inputs[1]);
        //var combinations = Convert.ToInt32(Math.Pow(5, (C-N))) * Convert.ToInt32(Math.Pow(10, (N)));


        }

            

    } 

    
}
