namespace CoreGrpcServices.Services;
using CoreGrpcServices;
using CoreGrpcServices.People;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Threading.Tasks;

public class PeopleService : People.PeopleBase
{
    public override Task<CoreDefaultResponseObject> GetPeople(GetPeopleRequest request, ServerCallContext context)
    {
        var reply = new CoreDefaultResponseObject
        {
            HasErrors = false,
            ResultType = "OK",
            MetaDataInfo = new MetaDataInfo
            {
                DataSource = "DataLake",
                CacheEvent = "CacheHit",
                Count = 2
            }
        };
        var people = GetAllPeople().Select(p => Any.Pack(p));
        reply.Content.AddRange(people);
        reply.Errors.AddRange([new Error { Code = "500", Description = "Something went wrong" }]);

        return Task.FromResult(reply);
    }

    private static IEnumerable<Person> GetAllPeople() =>
        [new Person {Id = 1, Name = "Glen Wilkin", Address = "214a Barnsley Road" },
         new Person {Id = 2, Name = "Jana Statue", Address = "214a Barnsley Road" }];
}
