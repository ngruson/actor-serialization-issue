using Dapr.Actors.Runtime;

namespace WebApi;

internal class PersonActor(ActorHost host): Actor(host), IPersonActor
{
    public Task<Person> ReturnPerson()
    {
        Person person = new(
            Guid.NewGuid(),
            "John Doe");

        return Task.FromResult(person);
    }
}
