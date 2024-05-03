// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.Test;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var conv = new ConventionPack();
conv.AddRange(
    [
        new CamelCaseElementNameConvention(),
        new IgnoreExtraElementsConvention(true)
    ]);

ConventionRegistry.Register("conv", conv, _ => true);

var conn
    = "mongodb://read-user:Ufv77D3oEuTL6ce7XXh7%23@w-c-dev-shard-00-00-2lqnz.azure.mongodb.net:27017," +
    "w-c-dev-shard-00-01-2lqnz.azure.mongodb.net:27017," +
    "w-c-dev-shard-00-02-2lqnz.azure.mongodb.net:27017/admin?ssl=true" +
    "&retryWrites=false" +
    "&loadBalanced=false" +
    "&readPreference=primary" +
    "&connectTimeoutMS=10000" +
    "&authSource=admin" +
    "&authMechanism=SCRAM-SHA-1";

var client = new MongoClient(conn);
var repo = new RepositoryFactory(client).GetRepository<ModelB>();

var pager = repo.CreatePager(1000);
var view = await pager.GetPageAsync(0).ConfigureAwait(false);

var str = JsonSerializer.Serialize(view, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true});
Console.WriteLine(str);
