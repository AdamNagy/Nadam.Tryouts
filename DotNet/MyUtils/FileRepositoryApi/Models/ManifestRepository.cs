using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileRepositoryApi.Models
{
    /*
    file name structure(path does not included):
    <file_title>.<category>.<extension>
    examples:
    fileName: my-test-file.gallery.json
    fileTitle: my-test-file -> file name first segment till the first dot(.)
    */
    public static class ManifestRepository
    {
        //                    file title / file name
        private static Dictionary<string, string> _manifests;
        private static string _root;

        public static void Init(string root)
        {
            _root = root;
            _manifests = Directory.GetFiles(root)
                                 .ToDictionary(path => GetFileTitle(Path.GetFileNameWithoutExtension(path)),
                                               path => Path.GetFileName(path));
        }

        public static ReadonlyManifest GetFileByTitle(string fileNameWithExtension)
        {
            if (_manifests.ContainsKey(fileNameWithExtension))
                return GenerateManifestFor(_manifests[fileNameWithExtension]);

            return null;
        }

        public static List<ReadonlyManifest> GetFilesByFileTitleSegment(string fileTitleSegment)
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

        private static string GetFileTitle(string fileNameWithoutExtension)
            => fileNameWithoutExtension.Split('.').First();

        public static string GetRoot() => _root;

        private static string GetCategoryFor(string fileName)
        {
            var splitted = fileName.Split('.');
            if (splitted.Length != 3)
                return "";

            return splitted[1];
        }

        private static ReadonlyManifest GenerateManifestFor(string fileName)
        {
            var category = GetCategoryFor(fileName);
            switch (category)
            {
                case "gallery": return new WebGalleryManifest($"{_root}\\{fileName}");
                case "local-gallery": return new GalleryManifest($"{_root}\\{fileName}");
                default: throw new ArgumentException($"for file {fileName} no handler was found");
            }
        }
    }
}