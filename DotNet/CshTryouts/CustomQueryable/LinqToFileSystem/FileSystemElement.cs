namespace CustomQueryable
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

    public class FolderElement : FileSystemElement
    {
        public FolderElement(string path) : base(path) { }

        public override ElementType ElementType => ElementType.Folder;
    }

    public class FileElement : FileSystemElement
    {
        public FileElement(string path) : base(path) { }

        public override ElementType ElementType => ElementType.File;
    }

    public enum ElementType
    {
        File,
        Folder
    }
}
