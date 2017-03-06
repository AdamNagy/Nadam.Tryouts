using System;
using System.Collections;
using System.Collections.Generic;

namespace Nadam.ConsoleTest.Models
{
    public class User : IEqualityComparer<User>, IEquatable<User>, IEqualityComparer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int Rank { get; set; }

        // IEqualityComparer<User> starts
        public bool Equals(User x, User y)
        {
            return x.Id == y.Id &&
                   x.Name.Equals(y.Name) &&
                   x.RegistrationDate == y.RegistrationDate;
        }

        public int GetHashCode(User obj)
        {
            return Id.GetHashCode();
        }

        // IEquatable<User> starts
        public bool Equals(User other)
        {
            return Id == other?.Id &&
                   Name.Equals(other.Name) &&
                   RegistrationDate.Equals(other.RegistrationDate);
        }

        public bool Equals(object other)
        {
            var otherOwner = (User) other;
            return Id == otherOwner?.Id &&
                   Name.Equals(otherOwner.Name) &&
                   RegistrationDate.Equals(otherOwner.RegistrationDate);
        }

        // IEqualityComparer
        public new bool Equals(object x, object y)
        {
            return ((User) x).Equals((User) y);
        }

        public int GetHashCode(object obj)
        {
            return Rank != 0 ?
                    ((Id * Rank) / 2).GetHashCode() :
                    Id.GetHashCode();
        }

        // operator ==
        public static bool operator ==(User a, User b)
        {
            return a?.Equals(b) ?? false;
        }

        public static bool operator !=(User a, User b)
        {
            return a?.Equals(b) ?? true;
        }
    }
}
