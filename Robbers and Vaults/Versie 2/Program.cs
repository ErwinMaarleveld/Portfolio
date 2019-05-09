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
            var N = int.Parse(inputs[1]);
            var combinations = Convert.ToInt32(Math.Pow(5, (C-N))) * Convert.ToInt32(Math.Pow(10, (N)));
            int numberOfRobbers = 1;    //This number can be 1+
            int numberOfVaults = 1;     //this number can be 1+
            IList<Vault> vaultList = new List<Vault>();
            IList<Robber> robberList = new List<Robber>();

            //creating all vaults
            for (int i = 0; i < numberOfVaults; i++){
                vaultList.Add(new Vault());
            }

            //creating all robbers and giving them a first vault if possible
            for (int i = 0; i < numberOfRobbers; i++)
            {
                try
                {
                    robberList.Add(new Robber(vaultList.First(v => v.status == 0)));
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("There are more robbers than available vaults!");
                    break;                    
                }
                catch (System.NullReferenceException)
                {
                    Console.WriteLine("There are more robbers than available vaults!");
                    break; 
                }
            }

            while (!allVaultsCracked)
            {
                for (int i = 0; i < robberList.Count(); i++)
                {
                    if (robberList[i].currentVault.status == 1)
                    {
                        Console.WriteLine("Starting to crack vault.");
                        DateTime startingTime = DateTime.Now;
                        bool combinationFound = false;
                        while (!combinationFound)
                        {
                            if (random.Next(1, combinations) == 1)
                            {
                                DateTime endingTime = DateTime.Now;
                                robberList[i].totalTimeSpentCracking += (endingTime - startingTime).TotalSeconds;
                                robberList[i].currentVault.status = -1;
                                Console.WriteLine("Vault is cracked!");
                                combinationFound = true;
                                totalVaultsCracked++;

                                for (int j = 0; j < vaultList.Count(); j++)
                                {
                                    if (vaultList[j].status == 0)
                                    {
                                        robberList[i].currentVault = vaultList[j];
                                        robberList[i].currentVault.status = 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }                   
                }

                if (totalVaultsCracked == numberOfVaults)
                {
                    allVaultsCracked = true;
                    var longestTimeCracking = 0.0;
                    for (int i = 0; i < robberList.Count(); i++)
                    {
                        try
                        {
                            if (robberList[i].totalTimeSpentCracking > longestTimeCracking)
                            {
                                longestTimeCracking = robberList[i].totalTimeSpentCracking;
                            }
                            
                        }
                        catch (System.NullReferenceException)
                        {
                            break;
                        }  
                    }
                    Console.WriteLine($"The robbers did {longestTimeCracking} seconds over cracking {vaultList.Count()} vault(s) with {robberList.Count()} robber(s).");
                }       
            }              
        } 
    } 
}