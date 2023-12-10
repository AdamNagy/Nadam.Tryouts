using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SQLiteDemo
{
    public enum PeopleRelationType
    {
        Mother, Father, Brother, Syster, Collegue, Friend, Neighbour
    }

    public class Person
    {
        [Key]
        public Guid Id { get; set; }

        [NotNull]
        public string Email { get; set; }

        [NotNull]
        public string FirstName { get; set; }

        [NotNull]
        public string LastName { get; set; }

        [NotNull]
        public DateTime DOB { get; set; }

        public int Weight { get; set; }
        public int Height { get; set; }
    }

    public class PeopleRelation
    {
        [Key]
        [ForeignKey("PersonA")]
        public Guid PersonAId { get; set; }
        public Person PersonA { get; set; }


        [ForeignKey("PersonB")]
        public Guid PersonBId { get; set; }
        public Person PersonB { get; set; }
    }
}