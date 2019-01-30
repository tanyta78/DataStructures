using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> peopleByEmail = new Dictionary<string, Person>();
    private Dictionary<string, SortedSet<Person>> peopleByEmailDomain = new Dictionary<string, SortedSet<Person>>();
    private Dictionary<string, SortedSet<Person>> peopleByNameAndTown = new Dictionary<string, SortedSet<Person>>();
    private OrderedDictionary<int, SortedSet<Person>> peopleByAge = new OrderedDictionary<int, SortedSet<Person>>();
    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> peopleByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.FindPerson(email) != null)
        {
            return false;
        }

        var person = new Person()
        {
            Email = email,
            Age = age,
            Name = name,
            Town = town
        };

        this.peopleByEmail.Add(email, person);

        var emailDomain = this.ExtractEmailDomain(email);
        this.peopleByEmailDomain.AppendValueToKey(emailDomain, person);

        var nameAndTown = this.CombineNameAndTown(name, town);
        this.peopleByNameAndTown.AppendValueToKey(nameAndTown, person);

        this.peopleByAge.AppendValueToKey(age, person);

        this.peopleByTownAndAge.EnsureKeyExists(town);
        this.peopleByTownAndAge[town].AppendValueToKey(age,person);

        return true;
    }

    public int Count => this.peopleByEmail.Count;

    public Person FindPerson(string email)
    {
        var personExist = this.peopleByEmail.TryGetValue(email, out var person);
        return person;
    }

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);
        if (person == null)
        {
            return false;
        }
        var personDeleted = this.peopleByEmail.Remove(email);

        var emailDomain = this.ExtractEmailDomain(email);
        this.peopleByEmailDomain[emailDomain].Remove(person);

        var nameAndTown = this.CombineNameAndTown(person.Name, person.Town);
        this.peopleByNameAndTown[nameAndTown].Remove(person);

        this.peopleByAge[person.Age].Remove(person);

        this.peopleByTownAndAge[person.Town][person.Age].Remove(person);

        return true;
    }

    private string ExtractEmailDomain(string email)
    {
        var domain = email.Split('@')[1];
        return domain;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.peopleByEmailDomain.GetValuesForKey(emailDomain);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var nameAndTown = this.CombineNameAndTown(name, town);
        return this.peopleByNameAndTown.GetValuesForKey(nameAndTown);
    }

    private string CombineNameAndTown(string name, string town)
    {
        const string separator = "[!]";
        return name + separator + town;
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var peopleInRange = this.peopleByAge.Range(startAge, true, endAge, true);
        foreach (var peopleByAge in peopleInRange.Values)
        {
            foreach (var person in peopleByAge)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (!this.peopleByTownAndAge.ContainsKey(town))
        {
            yield break;
        }

        var peopleInRange = this.peopleByTownAndAge[town].Range(startAge, true, endAge, true);
        foreach (var peopleByAge in peopleInRange.Values)
        {
            foreach (var person in peopleByAge)
            {
                yield return person;
            }
        }
    }
}
