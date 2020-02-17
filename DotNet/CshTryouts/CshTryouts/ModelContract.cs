using System.Collections.Generic;

namespace CshTryouts
{
    #region data access layer
    class ModelContract_A
    {
        public string Name { get; set; }
        public IEnumerable<object> Staffies { get; set; }
    }

    class ModelContract_B
    {
        public string Title { get; set; }
        public IEnumerable<string> Staffs { get; set; }
    }
    #endregion

    #region service layer
    class BusinessModelContract_A
    {
        protected const string type = "web";

        public string Name { get; set; }
        public IEnumerable<object> Staffies { get; set; }
    }

    class BusinessModelContract_B
    {
        protected const string type = "local";

        public string title;
        public IEnumerable<string> staffs;
    }
    #endregion
}
