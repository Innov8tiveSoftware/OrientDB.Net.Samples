# OrientDB.Net.Samples - OrientDB.Net Driver

## A special note on samples

This solution provides a set of examples on how to work with the OrientDB.NET driver. The OrientDB.NET driver is currently under heavy development and should be considered PRE-ALPHA. As such, it should be assumed that any examples only reflect the current state of the driver and will change, sometimes dramatically, over the coming months. As with most open-source projects, comments and criticism is welcomed and encouraged.

## Sample 1: OrientDB.Net.Samples.DatabaseManagement

This example provides insight on how to connect to an OrientDB host is server management mode to perform the following tasks:

* Create a new database.
* Connect to the newly created database.
* Check if a database already exists.
* Drop a database from the host.

Executable arguments:

```
dotnet OrientDB.Net.Samples.DatabaseManagement.dll [Hostname] [Username] [Password] [Port] [Database]
```

## Sample 2: OrientDB.Net.Samples.DataManagement

This set of examples provides insight on how to connect to an OrientDB host database and execute commands and queries. 

*Note the Transactions interface is still being fleshed out within the OrientDB.Core library and as such, this sample details its usage from within the OrientDB.Net.ConnectionProtocols.Binary library.*

Executable arguments:

```
dotnet OrientDB.Net.Samples.DataManagement.dll [Hostname] [Username] [Password] [Port] [Database]
```

## Sample 3: OrientDB.Net.Samples.Transactions

This set of examples details how to use transactions within the OrientDB.Net driver.

Executable arguments:

```
dotnet OrientDB.Net.Samples.Transactions [Hostname] [Username] [Password] [Port] [Database]
```