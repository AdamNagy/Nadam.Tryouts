using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevGuideToCollections;
using System.ComponentModel;

namespace Driver
{
    static class DL
    {
        static int s_nextId = 0;
        static NotificationList<Company> s_datasource;
        static WinFormsBindingList<Company> s_datasourceBL;
        static bool s_datasourceNotifications = true;
        static bool s_datasourceBLNotifications = true;

        public static WinFormsBindingList<Company> GetDataSourceBL()
        {
            if (s_datasourceBL == null)
            {
                NotificationList<Company> list = GetDataSource();

                s_datasourceBL = new WinFormsBindingList<Company>(list);

                // Use the following code to keep the list somewhat insync
                list.CollectionChanged +=
                    (sender, e) =>
                    {
                        if (!s_datasourceNotifications)
                        {
                            return;
                        }
                        switch (e.Action)
                        {
                            case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                                s_datasourceNotifications = false;
                                foreach (Company item in e.NewItems)
                                {
                                    s_datasourceBL.Add(item);
                                }
                                s_datasourceNotifications = true;
                                break;
                            case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                                s_datasourceNotifications = false;
                                foreach (Company item in e.OldItems)
                                {
                                    s_datasourceBL.Remove(item);
                                }
                                s_datasourceNotifications = true;
                                break;
                            case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                                if (s_datasource.Count == 0)
                                {
                                    s_datasourceNotifications = false;
                                    s_datasourceBL.Clear();
                                    s_datasourceNotifications = true;
                                }
                                break;
                        }
                    };

                s_datasourceBL.ListChanged +=
                    (sender, e) =>
                    {
                        if (!s_datasourceBLNotifications)
                        {
                            return;
                        }
                        switch (e.ListChangedType)
                        {
                            case ListChangedType.ItemAdded:
                                s_datasourceBLNotifications = false;
                                s_datasource.Insert(e.NewIndex, s_datasourceBL[e.NewIndex]);
                                s_datasourceBLNotifications = true;
                                break;
                            case ListChangedType.ItemDeleted:
                                s_datasourceBLNotifications = false;
                                s_datasource.RemoveAt(e.OldIndex);
                                s_datasourceBLNotifications = true;
                                break;
                            case ListChangedType.Reset:
                                if (s_datasourceBL.Count == 0)
                                {
                                    s_datasourceBLNotifications = false;
                                    s_datasource.Clear();
                                    s_datasourceBLNotifications = true;
                                }
                                break;
                        }
                    };
            }

            return s_datasourceBL;
        }


        public static NotificationList<Company> GetDataSource()
        {
            if (s_datasource == null)
            {
                s_datasource = new NotificationList<Company>();
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Alpine Ski House ",
                    Website = "http://www.alpineskihouse.com/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Tailspin Toys",
                    Website = "http://www.tailspintoys.com/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Graphic Design Institute ",
                    Website = "http://www.graphicdesigninstitute.com/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Contoso Pharmaceuticals",
                    Website = "http://www.contoso.com/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Wingtip Toys",
                    Website = "http://www.wingtiptoys.com/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Proseware, Inc.",
                    Website = "http://www.proseware.com/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Fabrikam, Inc.",
                    Website = "http://www.fabrikam.com/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "The Phone Company",
                    Website = "http://www.thephone-company.com/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Woodgrove Bank",
                    Website = "http://www.woodgrovebank.net/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "Coho Vineyard & Winery",
                    Website = "http://www.cohovineyardandwinery.com/"
                });
                s_datasource.Add(new Company()
                {
                    Id = GetNextId(),
                    Name = "School of Fine Art",
                    Website = "http://www.fineartschool.com/"
                });
            }
            return s_datasource;
        }

        public static int GetNextId()
        {
            return s_nextId++;
        }
    }

}
