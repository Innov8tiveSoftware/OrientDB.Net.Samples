using OrientDB.Net.ConnectionProtocols.Binary.Core;
using OrientDB.Net.Core.Abstractions;
using OrientDB.Net.Serializers.RecordCSVSerializer;
using System;

namespace OrientDB.Net.Samples.DatabaseManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 5)
                throw new ArgumentException($"{args.Length} arguments present, only 5 are required: hostname username password port database");

            // Create a new instance of an IOrientDBRecordSerializer of type byte[].
            IOrientDBRecordSerializer<byte[]> serializer = new OrientDBRecordCSVSerializer();

            // Instantiate and fill in the ServerConnectionOptions.
            ServerConnectionOptions options = new ServerConnectionOptions
            {
                HostName = args[0],
                UserName = args[1],
                Password = args[2],
                Port = int.Parse(args[3]),
                PoolSize = 1
            };

            // Create a Server Connection.
            using (OrientDBBinaryServerConnection serverConnection = new OrientDBBinaryServerConnection(options, serializer))
            {
                // Check if the database exists.
                if (serverConnection.DatabaseExists(args[4], StorageType.PLocal))
                    throw new Exception($"Database {args[4]} already exists!");

                // Create a database.
                using (OrientDBBinaryConnection databaseConnection = serverConnection.CreateDatabase(args[4], DatabaseType.Graph, StorageType.PLocal))
                {
                    // Open database connection.
                    databaseConnection.Open();
                }

                // Drop the database.
                serverConnection.DropDatabase(args[4], StorageType.PLocal);
            }
        }
    }
}