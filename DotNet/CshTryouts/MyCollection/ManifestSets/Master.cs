using System;
using System.Collections.Generic;

namespace MyCollection.ManifestSets
{
    class Gallery
    {
        public string Title { get; set; }
        public int Valami { get; set; }
    }

    // has to implement for every entity because LoadPartially is custom for all entity types
    class GalleryManifest : Generic_or_Json_Manifest<Gallery>, IManifest<Gallery>
    {
        // this method comes from JsonManifest
        //public Gallery Load()
        //{
        //    throw new NotImplementedException();
        //}

        // this method is custom for entiry
        public override Gallery LoadPartially()
        {
            throw new NotImplementedException();
        }

        // this method comes from GenericManifest
        //public void Save()
        //{
        //    throw new NotImplementedException();
        //}
    }

    public interface IManifest<T>
    {
        void Save(T model);
        T Load();
        T LoadPartially();
    }

    // impl detail I think
    abstract class Generic_or_Json_Manifest<T>
    {
        public T Load()
        {
            throw new NotImplementedException();
        }

        public abstract T LoadPartially();

        public void Save(T model)
        {
            throw new NotImplementedException();
        }
    }

    // it depends on the abstraction layer
    // can be made generic or custom for entites or maybe for distributed and single
    class Repository_or_DistributedTable
    {
        private IEnumerable<GalleryManifest> manifests { get; set; }
        // OR
        public IEnumerable<IManifest<Gallery>> manifests2 { get; set; }

        public Repository_or_DistributedTable(string root, string postfix)
        {
            
        }
    }
}
