using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Threading;

namespace PasswordCracker
{
    class Program
    {
        static void Main(string[] args)
        {
            //make sure all hashes get resolved to a string that equals uppercase or lowercase otherwise it will not work 

            // string Hash = "81DC9BDB52D04DC20036DBD8313ED055"; //1234
            // string Hash = "C0CD38F67DD1D4DA8D8A18FE6F4C502F".ToUpper(); //bean1
            // string Hash = "093ec71f562ba6cbf5825b7c9a48f19e".ToUpper(); //daddy 
            //  string Hash = "fcea920f7412b5da7be0cf42b8c93759".ToUpper(); //1234567
            // string Hash = "67881381dbc68d4761230131ae0008f7".ToUpper(); //babygirl
            // string Hash = "E10ADC3949BA59ABBE56E057F20F883E"; //123456

            Here: //a return point

             string Hash = "";
            Console.Write("Enter Your MD5 hash : ");
            Hash = Console.ReadLine().ToUpper();

            if(!string.IsNullOrEmpty(Hash))
            {
                Console.WriteLine("Valid MD5 Hash Value Ok");
            }
            else
            {
                Console.WriteLine("Not a Valid MD5 Hash Value !!!");
                goto Here;
            }

            string passwordList = "";
            Console.WriteLine("Enter the Password List File Name :: example (rockyou.txt)");
            passwordList = Console.ReadLine();

            if(File.Exists(passwordList))
            {
                Console.WriteLine("Password List Found Ok");
            }
            else
            {
                Console.WriteLine("Could Not Find the Password List");
                goto Here;
            }

            //  Hash = "e10adc3949ba59abbe56e057f20f883e"; //123456
            string Pass = "";
            int counter = 0;
            bool closeLoop = true; //this ends the loop after a password is found

           

            //open this file that will be save in your bin directory
            using (StreamReader file = new StreamReader(passwordList))
            {

                //this will run untill closeloop = false or the end of the file 
                while (closeLoop == true && (Pass = file.ReadLine()) != null)
                {
                    //this compares the output md5hash to the hash entered above and closes while loop
                    if (Md5Hash(Pass) == Hash)
                    {

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(Pass);
                        Console.ForegroundColor = ConsoleColor.Green;
                      
                        Console.WriteLine("Cracked Hash = " + Pass + "\n\r" + Md5Hash(Pass));

                        Console.ResetColor();
                        Console.ReadKey();
                        file.Close();
                        closeLoop = false;
                    }
                    else
                    {
                        //if no match just write out the password that was tried
                        Console.WriteLine(Pass);
                    }
                    counter++;
                    Console.Title = "Current Password Count: " + counter.ToString();
                    Thread.Sleep(10);
                }
                file.Close();
                Console.ReadKey();
             
            }
        }
        //this takes the file as an input string and outputs a md5 hash
        public static string Md5Hash(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            MD5CryptoServiceProvider MD5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = MD5Provider.ComputeHash(new UTF8Encoding().GetBytes(inputString));

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("X2"));

            }
            return sb.ToString();
        }
    }
}
