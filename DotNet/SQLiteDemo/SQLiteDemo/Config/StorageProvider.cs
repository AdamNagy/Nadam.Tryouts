namespace SQLiteDemo.Config
{
    public interface IStorageProvider
    {
        byte[] GetObject(string key);
        void SaveFile(string key, byte[] file, bool force);
    }

    public abstract class StorageProvider
    {
        public abstract void SaveFile(string key, byte[] file, bool force);

        public abstract byte[] GetFile(string key);
    }

    public class AzureStorageProvider : StorageProvider
    {
        public override byte[] GetFile(string key)
        {
            throw new NotImplementedException();
        }

        public override void SaveFile(string key, byte[] file, bool force)
        {
            throw new NotImplementedException();
        }
    }

    public class LocalDriveStorageProvider : StorageProvider
    {
        public override byte[] GetFile(string key)
        {
            if (!File.Exists(key)) throw new FileNotFoundException();

            return File.ReadAllBytes(key);
        }

        public override void SaveFile(string key, byte[] file, bool force)
        {
            if (File.Exists(key) && !force) throw new ArgumentException();

            File.WriteAllBytes(key, file);
        }
    }
}
