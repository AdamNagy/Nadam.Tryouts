using System.Collections.Generic;
using System.Linq;

namespace MultiDimensionalEntity
{
    public class DomainEntity
    {
        public string Text { get; set; }
        public IEnumerable<RelationalEntityA> As { get; set; }
        public IEnumerable<RelationalEntityB> Bs { get; set; }

        public static IEnumerable<DomainEntity> GenerateData()
        {
            return new List<DomainEntity>()
            {
                new DomainEntity()
                {
                    Text = "EvensOdss",
                    As = Enumerable.Range(0, 100).Where(p => p % 2 == 0).Select(p => new RelationalEntityA(p)),
                    Bs = Enumerable.Range(0, 100).Where(p => p % 2 == 0).Select(p => new RelationalEntityB(p+1)),
                },
                new DomainEntity()
                {
                    Text = "ThreeFives",
                    As = Enumerable.Range(0, 100).Where(p => p % 3 == 0).Select(p => new RelationalEntityA(p)),
                    Bs = Enumerable.Range(0, 100).Where(p => p % 5 == 0).Select(p => new RelationalEntityB(p)),
                },
                new DomainEntity()
                {
                    Text = "ThreeSevens",
                    As = Enumerable.Range(0, 100).Where(p => p % 3 == 0).Select(p => new RelationalEntityA(p)),
                    Bs = Enumerable.Range(0, 100).Where(p => p % 7 == 0).Select(p => new RelationalEntityB(p+1)),
                },
            };
        }
    }

    public class RelationalEntityA
    {
        public int EvenNum { get; set; }

        public RelationalEntityA(int i)
        {
            EvenNum = i;
        }
    }

    public class RelationalEntityB
    {
        public int OddNum { get; set; }

        public RelationalEntityB(int i)
        {
            OddNum = i;
        }
    }
}
