using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevGuideToCollections;

namespace Driver
{
    static class DL
    {
        static int s_nextId = 0;
        static List<Company> s_data;
        static WinFormsBindingList<Company> s_datasource;
        static WinFormsBindingListView<Company> s_datasourceView;

        public static IEnumerable<Company> GetData()
        {
            if (s_data == null)
            {
                s_data = new List<Company>();
                s_data.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Alpine Ski House ",
                    Website = "http://www.alpineskihouse.com/"
                });
                s_data.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Tailspin Toys",
                    Website = "http://www.tailspintoys.com/"
                });
                s_data.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Graphic Design Institute ",
                    Website = "http://www.graphicdesigninstitute.com/"
                });
                s_data.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Contoso Pharmaceuticals",
                    Website = "http://www.contoso.com/"
                });
            }
            return s_data;
        }

        public static WinFormsBindingList<Company> GetDataSource()
        {
            if (s_datasource == null)
            {
                s_datasource = new WinFormsBindingList<Company>(GetData());
            }
            return s_datasource;
        }

        public static WinFormsBindingListView<Company> GetDataSourceView()
        {
            if (s_datasourceView == null)
            {
                s_datasourceView = new WinFormsBindingListView<Company>(GetData());
            }
            return s_datasourceView;
        }
        
        public static int GetNextId()
        {
            return s_nextId++;
        }
    }
}
