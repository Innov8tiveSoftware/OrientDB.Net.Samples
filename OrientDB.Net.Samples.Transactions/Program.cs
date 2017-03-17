using OrientDB.Net.ConnectionProtocols.Binary;
using OrientDB.Net.ConnectionProtocols.Binary.Core;
using OrientDB.Net.Core;
using OrientDB.Net.Core.Abstractions;
using OrientDB.Net.Serializers.RecordCSVSerializer;
using System;
using System.Collections.Generic;

namespace OrientDB.Net.Samples.Transactions
{
    class Program
    {
        static void Main(string[] args)
        {
            string hostname = args[0];
            string username = args[1];
            string password = args[2];
            int port = int.Parse(args[3]);
            string database = args[4];

            var serverConnectionOptions = new ServerConnectionOptions
            {
                HostName = hostname,
                Password = password,
                PoolSize = 1,
                Port = port,
                UserName = username
            };

            var databaseConnectionOptions = new DatabaseConnectionOptions
            {
                Database = database,
                HostName = hostname,
                Password = password,
                PoolSize = 10,
                Port = port,
                Type = DatabaseType.Graph,
                UserName = username
            };

            var serializer = new OrientDBRecordCSVSerializer();

            DatabaseUtilities.CreateDatabase(serverConnectionOptions, database, serializer);
            Console.WriteLine($"Database {database} created.");

            using (OrientDBBinaryConnection connection = new OrientDBBinaryConnection(databaseConnectionOptions, serializer))
            {
                connection.Open();

                connection.CreateCommand().Execute("CREATE CLASS Person");

                var transaction = connection.CreateTransaction();
                var person1 = new Person { Age = 33, FirstName = "John", LastName = "Doe", FavoriteColors = new[] { "black", "blue" } };
                transaction.AddEntity(person1);
                transaction.AddEntity(new Person { Age = 35, FirstName = "Jane", LastName = "Doe", FavoriteColors = new[] { "red", "blue" } });
                transaction.Commit();
                transaction = connection.CreateTransaction();
                transaction.Remove(person1);
                transaction.Commit();
            }
            
            IEnumerable<Person> personCollection = new List<Person>();

            IOrientConnection orientConnection = new OrientDBConfiguration()
                .ConnectWith<byte[]>()
                .Connect(new BinaryProtocol(hostname, username, password, database, DatabaseType.Graph, 2424))
                .SerializeWith.Serializer(new OrientDBRecordCSVSerializer())
                .LogWith.Logger(new ConsoleOrientDBLogger())
                .CreateFactory().GetConnection();

            personCollection = orientConnection.ExecuteQuery<Person>("SELECT * FROM Person");

            foreach (var person in personCollection)
            {
                Console.WriteLine($"FirstName: {person.FirstName}, LastName: {person.LastName}, Age: {person.Age}, Favorite Colors: {string.Join(",", person.FavoriteColors)}");
            }

            DatabaseUtilities.DeleteDatabase(serverConnectionOptions, database, serializer);
            Console.WriteLine($"Database {database} deleted.");
        }

        static IOrientConnection CreateConnection(string hostname, string username, string password, string database, int port, IOrientDBRecordSerializer<byte[]> serializer)
        {
            return new OrientDBConfiguration()
                .ConnectWith<byte[]>()
                .Connect(new BinaryProtocol(hostname, username, password, database, DatabaseType.Graph, port))
                .SerializeWith.Serializer(new OrientDBRecordCSVSerializer())
                .LogWith.Logger(new ConsoleOrientDBLogger())
                .CreateFactory().GetConnection();
        }
    }
}