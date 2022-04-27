// See https://aka.ms/new-console-template for more information

using RedisTree;
using StackExchange.Redis;

// const string connectionString = "";
const string connectionString = "";

var configOptions = ConfigurationOptions.Parse(connectionString);
configOptions.AsyncTimeout = (int)TimeSpan.FromMinutes(5).TotalMilliseconds;
var connection = await ConnectionMultiplexer.ConnectAsync(configOptions);
var database = connection.GetDatabase(2);

var key = "testtree";

// await database.TreeInsert(key, 1, 4);
// await database.TreeInsert(key, 1, 3, InsertOption.Before, 4);
// await database.TreeInsert(key, 2, 5, InsertOption.Index, 1000);
// await database.TreeInsert(key, 5, 4);

// await database.TreeInsert(key, "ROOT1", "1");
// await database.TreeInsert(key, "ROOT1", "2");
// await database.TreeInsert(key, "ROOT1", "3");
// await database.TreeInsert(key, "2", "4");
// await database.TreeInsert(key, "4", "5");
// await database.TreeInsert(key, "3", "6");

// await database.TreeInsert(key, "ROOT2", "8");
// await database.TreeInsert(key, "8", "6");
// await database.TreeInsert(key, "8", "7");
// var result = await database.TreePath(key, 1, 5);
// var result = await database.TreePath(key, "ROOT1", "1");
// result = await database.TreePath(key, "ROOT1", "7");
// result = await database.TreePath(key, "ROOT1", "6");
// result = await database.TreePath(key, "ROOT1", "5");
// result = await database.TreePath(key, "ROOT1", "ROOT2");
// result = await database.TreePath(key, "ROOT2", "7");

// await database.TreeChildren(key, "ROOT1");
var result = await database.TreeParents(key, "5", 1);

var asd = 1;