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
            Console.WriteLine("Program started!");
            Random random = new Random();
            bool allVaultsCracked = false;
            int totalVaultsCracked = 0;
            var inputs = new List<string>{ "10", "5" };     //format: [0] = number of total length of code; [1] = number of availible digits
            var C = int.Parse(inputs[0]);
            var N = int.Parse(inputs[1]);
            var combinations = Convert.ToInt32(Math.Pow(5, (C-N))) * Convert.ToInt32(Math.Pow(10, (N)));
            int numberOfRobbers = 4;    //This number can be 1+
            int numberOfVaults = 5;     //this number can be 1+
            IList<Vault> vaultList = new List<Vault>();
            IList<Robber> robberList = new List<Robber>();
            Func<Vault> newFreeVault = () => vaultList.FirstOrDefault(v => v.status == 0);
            //creating all vaults
            for (int i = 0; i < numberOfVaults; i++){
                vaultList.Add(new Vault());
            }

            //creating all robbers and giving them a first vault if possible
            for (int i = 0; i < numberOfRobbers; i++)
            {
                try
                {
                    robberList.Add(new Robber(vaultList.FirstOrDefault(v => v.status == 0)));
                }
                catch (System.NullReferenceException)
                {}    
            }

            while (!allVaultsCracked)
            {
                foreach (var robber in robberList)
                {
                    try
                    {
                        if (robber.currentVault.status == 1)
                        {
                            DateTime startingTime = DateTime.Now;
                            bool combinationFound = false;
                            while (!combinationFound)
                            {
                                if (random.Next(1, combinations) == 1)
                                {
                                    try
                                    {
                                        DateTime endingTime = DateTime.Now;
                                        robber.totalTimeSpentCracking += (endingTime - startingTime).TotalSeconds;
                                        robber.currentVault.status = -1;
                                        combinationFound = true;
                                        totalVaultsCracked++;
                                        robber.currentVault = newFreeVault();
                                        robber.currentVault.status = 1;  
                                    }
                                    catch (System.NullReferenceException)
                                    {
                                        break;
                                    }   
                                }
                            }
                        }
                    }
                    catch (System.NullReferenceException)
                    {
                        break;
                    }
                        
                }

                if (totalVaultsCracked == numberOfVaults)
                {
                    allVaultsCracked = true;
                    var longestTimeCracking = 0.0;
                    foreach (var robber in robberList)
                    {
                        if (longestTimeCracking < robber.totalTimeSpentCracking)
                        {
                            longestTimeCracking = robber.totalTimeSpentCracking;
                        }
                    }
                    Console.WriteLine($"The robbers did {longestTimeCracking} seconds over cracking {vaultList.Count()} vault(s) with {robberList.Count()} robber(s).");
                }       
            }              
        } 
    } 
}
