using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ManifestRepositoryApi.ManifestFramework;

namespace ManifestFramework.Tests
{
    public class ManifestRepositoryTests
    {
        public class CountTests
        {


            [Fact]
            public void Count_shouldbe1()
            {
                var mockFiles = new string[]
                {
                    "dir\\subdir\\test1.gallery.json",
                };

                var myDirectory = new Mock<IDirectoryProvider>();
                myDirectory.Setup(p => p.GetFiles("")).Returns(mockFiles);

                ManifestRepository.Init("", myDirectory.Object);
                Assert.Equal(1, ManifestRepository.Instance.Count);
            }

            [Fact]
            public void Count_shouldbe0()
            {
                var mockFiles = new string[]
                {
                };

                var myDirectory = new Mock<IDirectoryProvider>();
                myDirectory.Setup(p => p.GetFiles("")).Returns(mockFiles);

                ManifestRepository.Init("", myDirectory.Object);
                Assert.Equal(0, ManifestRepository.Instance.Count);
            }

            [Fact]
            public void Count_shouldbe5()
            {
                var mockFiles = new string[]
                {
                    "dir\\subdir\\test1.gallery.json",
                    "dir\\subdir\\test2.gallery.json",
                    "dir\\subdir\\test3.gallery.json",

                    "dir\\subdir\\test4.gallery.json",
                    "dir\\subdir\\test5.gallery.json",
                };

                var myDirectory = new Mock<IDirectoryProvider>();
                myDirectory.Setup(p => p.GetFiles("")).Returns(mockFiles);
                ManifestRepository.Init("", myDirectory.Object);

                Assert.Equal(5, ManifestRepository.Instance.Count);
            }
        }

        [Fact]
        public void GetFileByTitle_exist()
        {
            // <arrange>
            var mockFiles = new string[]
            {
                "dir\\subdir\\test1.gallery.json",
                "dir\\subdir\\test2.gallery.json",
                "dir\\subdir\\test3.gallery.json",

                "dir\\subdir\\test4.gallery.json",
                "dir\\subdir\\test5.gallery.json",
            };

            var myDirectory = new Mock<IDirectoryProvider>();
            myDirectory.Setup(p => p.GetFiles("dir\\subdir")).Returns(mockFiles);
            ManifestRepository.Init("dir\\subdir", myDirectory.Object);
            // </arrange>

            var file = ManifestRepository.Instance.GetFileByTitle("test1");

            Assert.Equal("dir\\subdir\\test1.gallery.json", file.Path);
        }

        [Fact]
        public void GetFileByTitle_shouldBeNull()
        {
            // <arrange>
            var mockFiles = new string[]
            {
                "dir\\subdir\\test1.gallery.json",
                "dir\\subdir\\test2.gallery.json",
                "dir\\subdir\\test3.gallery.json",

                "dir\\subdir\\test4.gallery.json",
                "dir\\subdir\\test5.gallery.json",
            };

            var myDirectory = new Mock<IDirectoryProvider>();
            myDirectory.Setup(p => p.GetFiles("dir\\subdir")).Returns(mockFiles);
            ManifestRepository.Init("dir\\subdir", myDirectory.Object);
            // </arrange>

            var file = ManifestRepository.Instance.GetFileByTitle("not_exist_file");

            Assert.Null(file);
        }

        [Fact]
        public void GetFilesByFileTitleSegment_shouldReturnSome()
        {
            // <arrange>
            var mockFiles = new string[]
            {
                "dir\\subdir\\test1.gallery.json",
                "dir\\subdir\\test2.gallery.json",
                "dir\\subdir\\test3.gallery.json",

                "dir\\subdir\\test4.gallery.json",
                "dir\\subdir\\test5.gallery.json",

                "dir\\subdir\\other1.gallery.json",
                "dir\\subdir\\other2.gallery.json",
                "dir\\subdir\\other3.gallery.json",
            };

            var myDirectory = new Mock<IDirectoryProvider>();
            myDirectory.Setup(p => p.GetFiles("dir\\subdir")).Returns(mockFiles);
            ManifestRepository.Init("dir\\subdir", myDirectory.Object);
            // </arrange>

            var files = ManifestRepository.Instance.GetFilesByFileTitleSegment("test");

            Assert.Equal(5, files.Count);
            Assert.Equal("dir\\subdir\\test1.gallery.json", files[0].Path);
        }

        [Fact]
        public void GetFilesByFileTitleSegment_shouldEmpty()
        {

        }
    }
}
