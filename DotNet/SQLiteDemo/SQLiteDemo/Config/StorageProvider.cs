namespace SQLiteDemo.Config
{
    public interface IStorageProvider
    {
        bool GetObject(string key, out byte[] file);
        void SaveObject(string key, byte[] file, bool force);
    }

    public class AzureStorageProvider : IStorageProvider
    {
        public bool GetObject(string key, out byte[] file)
        {
            throw new NotImplementedException();
        }

        public void SaveObject(string key, byte[] file, bool force)
        {
            throw new NotImplementedException();
        }
    }

    public class LocalDriveStorageProvider : IStorageProvider
    {
        public string Root { get => _root; }
        private readonly string _root;

        public LocalDriveStorageProvider(string root)
        {
            _root = root;
        }

        public bool GetObject(string key, out byte[] file)
        {
            if (!File.Exists(Path.Combine(_root, key)))
            {
                file = null;
                return false;
            }

            file = File.ReadAllBytes(Path.Combine(_root, key));
            return true;
        }

        public void SaveObject(string key, byte[] file, bool force)
        {
            if (File.Exists(Path.Combine(_root, key)) && !force) throw new ArgumentException();

            File.WriteAllBytes(Path.Combine(_root, key), file);
        }
    }
}
