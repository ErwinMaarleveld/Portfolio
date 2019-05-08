using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Timers;


namespace Robbers
{
    public class Vault{
        public int status {get; set;}   //status: -1 = cracked vault; 0 = free vault; 1 = taken vault;

        public Vault(){
            status = 0;
        }
    }

    public class Robber{
        public Vault currentVault {get; set;}
        public double totalTimeSpentCracking {get; set;}

        public Robber(Vault vault){
            currentVault = vault;
            currentVault.status = 1;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Random random = new Random();
            bool allVaultsCracked = false;
            int totalVaultsCracked = 0;
            var inputs = new List<string>{ "10", "5" };     //format: [0] = number of total length of code; [1] = number of availible digits
            var C = int.Parse(inputs[0]);
            Console.WriteLine("C has a value of: " + C);
            var N = int.Parse(inputs[1]);
            Console.WriteLine("N has a value of: " + N);
            var combinations = Convert.ToInt32(Math.Pow(5, (C-N))) * Convert.ToInt32(Math.Pow(10, (N)));
            Console.WriteLine("Total possible combinations are: " + combinations);
            int numberOfRobbers = 2;    //This number can be 1+
            int numberOfVaults = 1;     //this number can be 1+
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
                    allRobbers[i] = new Robber(allVaults[i]);
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("There are more robbers than available vaults!");
                    break;                    
                }
            }

            while (!allVaultsCracked)
            {
                for (int i = 0; i < allRobbers.Count(); i++)
                {
                    try
                    {
                        if (allRobbers[i].currentVault.status == 1)
                        {
                            bool combinationFound = false;
                            while (!combinationFound)
                            {
                                if (random.Next(1, combinations) == 1)
                                {
                                    allRobbers[i].currentVault.status = -1;
                                    Console.WriteLine("Vault is cracked!");
                                    combinationFound = true;
                                    totalVaultsCracked++;

                                    for (int j = 0; j < allVaults.Count(); j++)
                                    {
                                        if (allVaults[j].status == 0)
                                        {
                                            allRobbers[i].currentVault = allVaults[j];
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        Console.WriteLine($"This robber, robber {i + 1}, doesn't have a vault.");    
                    }
                    
                }

                if (totalVaultsCracked == numberOfVaults)
                {
                    allVaultsCracked = true;
                    Console.WriteLine("All vaults are cracked!");
                    Console.WriteLine($"Total vaults are: {numberOfVaults}, and total number of vaults in array is: {allVaults.Count()}");
                    Console.WriteLine("Total cracked vauts are: " + totalVaultsCracked);
                    Console.WriteLine($"Total robbers is: {numberOfRobbers}, and total number of robbers in array is: {allRobbers.Count()}");
                }
                
                
            }
                   
      }
    } 
}