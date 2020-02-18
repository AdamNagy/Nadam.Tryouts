using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        private static ManifestRepository _instance;
        public static ManifestRepository Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new ManifestRepository();

                return _instance;
            }
        }

        public int Count
        {
            get => _manifests.Count();
        }

        //                    file title / file name
        private static Dictionary<string, string> _manifests;
        private static string _root;

        private ManifestRepository() { }

        public void Init(string root)
        {
            if (root == _root)
                return;

            _root = root;
            _manifests = Directory.GetFiles(root)
                                 .ToDictionary(path => GetFileTitle(Path.GetFileNameWithoutExtension(path)),
                                               path => Path.GetFileName(path));
        }

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

        private string GetFileTitle(string fileNameWithoutExtension)
            => fileNameWithoutExtension.Split('.').First();

        public string GetRoot() => _root;

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
                case "gallery": return new WebGalleryManifest($"{_root}\\{fileName}");
                case "local-gallery": return new LocalGalleryManifest($"{_root}\\{fileName}");
                default: throw new ArgumentException($"for file {fileName} no handler was found");
            }
        }
    }
}