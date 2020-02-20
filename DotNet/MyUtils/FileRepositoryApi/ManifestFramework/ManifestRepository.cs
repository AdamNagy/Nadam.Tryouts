using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ManifestRepositoryApi.ManifestFramework
{
    /*
    file name structure(path does not included):
    <file_title>.<category>.<extension>
    examples:
    fileName: my-test-file.gallery.json
    fileTitle: my-test-file -> file name first segment till the first dot(.)
    */
    public class ManifestRepository
    {
        #region singleton instance
        private static ManifestRepository _instance;
        public static ManifestRepository Instance
        {
            get
            {
                if(_instance == null)
                    throw new Exception("Singleton instance havn't initialized");

                return _instance;
            }
        }
        #endregion

        public string Root { get; private set; }
        public int Count { get => _manifests.Count(); }

        //                    file title / file name
        private Dictionary<string, string> _manifests;

        private ManifestRepository() { }

        public static void Init(string root)
        {
            _instance = new ManifestRepository();

            _instance.Root = root;
            _instance._manifests = Directory.GetFiles(root)
                                    .ToDictionary(path => _instance.GetFileTitle(Path.GetFileNameWithoutExtension(path)),
                                                  path => Path.GetFileName(path));
        }

        public static void Init(string root, IDirectoryProvider provider)
        {
            _instance = new ManifestRepository();

            _instance.Root = root;
            _instance._manifests = provider.GetFiles(root)
                                            .ToDictionary(path => _instance.GetFileTitle(Path.GetFileNameWithoutExtension(path)),
                                                          path => Path.GetFileName(path));
        }

        #region returns ReadonlyManifest
        public ReadonlyManifest GetFileByTitle(string fileNameWithExtension)
        {
            if (_manifests.ContainsKey(fileNameWithExtension))
                return GenerateManifestFor(_manifests[fileNameWithExtension]);

            return null;
        }

        public List<ReadonlyManifest> GetFilesByFileTitleSegment(string fileTitleSegment)
        {
            fileTitleSegment = fileTitleSegment.ToLower();
            var ret = new List<string>();
            foreach (var filePureTitle in _manifests.Keys)
            {
                if(filePureTitle.ToLower().Contains(fileTitleSegment))
                    ret.Add(_manifests[filePureTitle]);
            }

            return ret.Select(GenerateManifestFor).ToList();
        }

        public IEnumerable<ReadonlyManifest> All()
        {
            foreach (var manifest in _manifests)
                yield return GenerateManifestFor(manifest.Value);
        }
        #endregion

        #region returns file names
        public IEnumerable<string> GetFileNames()
        {
            foreach (var fileName in _manifests.Values)
                yield return fileName;
        }

        public IEnumerable<string> GetFileNames(Func<string, bool> pred)
        {
            foreach (var fileName in _manifests.Values.Where(pred))
                yield return fileName;
        }
        #endregion

        public ReadonlyManifest CreateManifest(string fileName, string content)
        {
            if (GetFileNames().SingleOrDefault(p => p == fileName) != null)
                throw new Exception($"File already exist with provided name: {fileName}");

            var newFileStream = File.Create($"{Root}\\{fileName}");
            var byteArr = Encoding.ASCII.GetBytes(content);
            newFileStream.Write(byteArr, 0, byteArr.Length);

            var title = GetFileTitle(fileName);
            _manifests.Add(title, fileName);

            newFileStream.Close();
            return GetFileByTitle(title);
        }

        public bool DeleteManifest(string title)
        {
            if (!_manifests.ContainsKey(title))
                return false;

            _manifests.Remove(title);
            return true;
        }

        private string GetFileTitle(string fileName)
            => fileName.Split('.').First();
        
        private string GetCategoryFor(string fileName)
        {
            var splitted = fileName.Split('.');
            if (splitted.Length != 3)
                return "";

            return splitted[1];
        }

        private ReadonlyManifest GenerateManifestFor(string fileName)
        {
            var category = GetCategoryFor(fileName);
            switch (category)
            {
                case "gallery": return new WebGalleryManifest($"{Root}\\{fileName}");
                case "local-gallery": return new LocalGalleryManifest($"{Root}\\{fileName}");
                default: throw new ArgumentException($"for file {fileName} no handler was found");
            }
        }
    }

    public interface IDirectoryProvider
    {
        IEnumerable<string> GetFiles(string path);
    }
}