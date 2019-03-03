using System;

namespace Nadam.Infrastructure
{
    public class FileManifest
    {
        public string Directory { get; set; }
        public string FileTitle { get; set; }
        public string Extension { get; set; }
        public string Path { get { return $"{Directory}\\{FileTitle}.{Extension}"; } }

        public readonly DateTime created;

        private bool _isLoaded;
        private bool _isDeleted;

        public FileManifest(string _directory, string _fileTtiel, string _extension)
        {
            Directory = _directory;
            FileTitle = _fileTtiel;
            Extension = _extension;

            created = DateTime.Now;
            _isLoaded = false;
            _isDeleted = false;
        }

        public virtual void Save(byte[] serialized)
        {
            throw new NotImplementedException();
        }

        public T Load<T>()
        {
            _isLoaded = true;
            throw new NotImplementedException();
        }

        public virtual void Delete()
        {
            _isDeleted = true;
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void ChangeFileTitle(string newTitle)
        {

        }
    }
}
