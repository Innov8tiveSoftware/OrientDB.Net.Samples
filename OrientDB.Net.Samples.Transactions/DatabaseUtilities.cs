using OrientDB.Net.ConnectionProtocols.Binary.Core;
using OrientDB.Net.Core.Abstractions;

namespace OrientDB.Net.Samples.Transactions
{
    static class DatabaseUtilities
    {
        public static void CreateDatabase(ServerConnectionOptions serverConnectionOptions, string database, IOrientDBRecordSerializer<byte[]> serializer)
        {
            using (OrientDBBinaryServerConnection serverConnection = new OrientDBBinaryServerConnection(serverConnectionOptions, serializer))
            {
                serverConnection.Open();

                if (serverConnection.DatabaseExists(database, StorageType.PLocal))
                    serverConnection.DropDatabase(database, StorageType.PLocal);

                var databaseConnection = serverConnection.CreateDatabase(database, DatabaseType.Graph, StorageType.PLocal);           
            }
        }

        public static void DeleteDatabase(ServerConnectionOptions serverConnectionOptions, string database, IOrientDBRecordSerializer<byte[]> serializer)
        {
            using (OrientDBBinaryServerConnection serverConnection = new OrientDBBinaryServerConnection(serverConnectionOptions, serializer))
            {
                serverConnection.Open();

                if (!serverConnection.DatabaseExists(database, StorageType.PLocal))
                    return;

                serverConnection.DropDatabase(database, StorageType.PLocal);
            }
        }
    }
}
