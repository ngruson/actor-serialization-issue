using Dapr.Actors;

namespace WebApi;

public interface IPersonActor : IActor
{
    Task<Person> ReturnPerson();
}