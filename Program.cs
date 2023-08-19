using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MongoDBRepository mongoDBRepository = new MongoDBRepository();
            MongoClient dbClient = mongoDBRepository.connectDataBase();

            try
            {
                Console.WriteLine("Enter your choice: \n" +
                    "1 for Display List of Databases\n" +
                    "2 for provide database name that you want connect \n" +
                    "3 insert document into connected database");
                string b = Console.ReadLine();
                int input;
                if (int.TryParse(b, out input))
                {
                    switch (input)
                    {
                        case 1:
                            mongoDBRepository.GetDatabases(dbClient).ForEach(db1 => Console.WriteLine(db1));
                            break;
                        case 2:
                            Console.WriteLine("Enter Which database wants to connect: ");
                            var result = mongoDBRepository.GetDatabaseStats(dbClient, Console.ReadLine());
                            Console.WriteLine(result.ToJson());
                            break;
                        case 3:

                            Console.WriteLine("Enter Which database wants to insert document: ");
                            string dbNamea = Console.ReadLine();
                            var objList = mongoDBRepository.InsertCollection(dbClient, dbNamea);
                            if (objList.Count > 0)
                            {
                                Console.WriteLine(objList.Count + " documents inserted successfully");
                            }
                            break;


                    }
                }

                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Connection to database failed");
            }

        }
    }
}
