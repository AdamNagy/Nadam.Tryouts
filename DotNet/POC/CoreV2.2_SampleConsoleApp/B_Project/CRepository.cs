using System;
using C_Project;

namespace B_Project
{
    public class CRepository
    {
        public CClass Get()
        {
            return new CClass()
            {
                Name = "Adam",
                Age = 30,
                DOB = DateTime.Now
            };
        }
    }
}
