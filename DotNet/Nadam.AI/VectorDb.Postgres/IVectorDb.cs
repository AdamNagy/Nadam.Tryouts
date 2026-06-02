using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorDb.Postgres;

public interface IVectorDb
{
    Task InsertVector();
    Task SearchVector();
}
