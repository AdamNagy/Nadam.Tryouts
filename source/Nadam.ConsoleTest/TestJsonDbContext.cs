using System.Collections.Generic;
using Nadam.Global.JsonDb;

using Nadam.ConsoleTest.Models;

namespace Nadam.ConsoleTest
{
    public class TestJsonDbContext : JsonDbEngineContext
    {
        public TestJsonDbContext(bool inmemory = true) : base("TestJsonDbRoot", inmemory)
        {
            if (inmemory)
            {
                _images = GetTableData<Image>("Images") ?? new List<Image>();
                _users = GetTableData<User>("Users") ?? new List<User>();
                _colors = GetTableData<Color>("Colors") ?? new List<Color>();
            }
        }

        // TODO: moove getter logic to base class (find a solution)

        public IList<Image> Images {
            get
            {
                if (!Inmemory || _images == null)
                {
                    var value = GetTableData<Image>("Images");
                    return value ?? new List<Image>();
                }
                return _images;
            }
            set { _images = value; }
        }
        private IList<Image> _images;

        public IList<User> Users
        {
            get
            {
                if (!Inmemory)
                {
                    var value = GetTableData<User>("Users");
                    return value ?? new List<User>();
                }
                return _users;
            }
            set { _users = value; }
        }
        private IList<User> _users;
        
        public IList<Color> Colors
        {
            get
            {
                if (!Inmemory)
                {
                    var value = GetTableData<Color>("Colors");
                    return value ?? new List<Color>();
                }
                return _colors;
            }
            set { _colors = value; }
        }
        private IList<Color> _colors;
    }
}
