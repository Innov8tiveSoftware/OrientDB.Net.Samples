using OrientDB.Net.ConnectionProtocols.Binary;
using OrientDB.Net.ConnectionProtocols.Binary.Core;
using OrientDB.Net.Core;
using OrientDB.Net.Core.Abstractions;
using OrientDB.Net.Serializers.RecordCSVSerializer;
using System;
using System.Linq;

namespace OrientDB.Net.Samples.DataManagement
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

            var serializer = new OrientDBRecordCSVSerializer();

            DatabaseUtilities.CreateDatabase(serverConnectionOptions, database, serializer);
            Console.WriteLine($"Database {database} created.");

            IOrientConnection connection = CreateConnection(hostname, username, password, database, port, serializer);

            connection.ExecuteCommand("CREATE CLASS Persons");
            Console.WriteLine("CREATE CLASS Persons executed.");

            connection.ExecuteCommand("INSERT INTO Persons (FirstName, LastName, FavoriteColors, Age) VALUES ('Joe', 'Tester', ['red', 'blue'], 25)");
            Console.WriteLine("INSERT INTO Persons (FirstName, LastName, FavoriteColors, Age) VALUES ('Joe', 'Tester', ['red', 'blue'], 25) executed.");

            var persons = connection.ExecuteQuery<Person>("SELECT * FROM Persons");
            Console.WriteLine($"SELECT * FROM Persons executed with {persons.Count()} results.");

            foreach(var person in persons)
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