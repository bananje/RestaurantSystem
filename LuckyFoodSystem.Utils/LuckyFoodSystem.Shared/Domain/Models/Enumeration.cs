using System.Reflection;

namespace LuckyFoodSystem.Shared.Domain.Models
{
    public abstract class Enumeration
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>();


#pragma warning disable CS8765
        public override bool Equals(object obj)
#pragma warning restore CS8765
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static T FromName<T>(string name) where T : Enumeration
            => GetAll<T>().FirstOrDefault(x => x.Name == name)!;

        public static T FromId<T>(int id) where T : Enumeration
            => GetAll<T>().FirstOrDefault(x => x.Id == id)!;
    }
}
