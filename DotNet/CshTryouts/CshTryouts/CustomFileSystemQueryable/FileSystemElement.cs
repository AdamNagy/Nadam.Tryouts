namespace CshTryouts.CustomQueryable
{
    public abstract class FileSystemElement
    {
        public string Path { get; private set; }
        public abstract ElementType ElementType { get; }

        protected FileSystemElement(string path)
        {
            Path = path;
        }
    }

    public enum ElementType
    {
        File,
        Folder
    }
}
