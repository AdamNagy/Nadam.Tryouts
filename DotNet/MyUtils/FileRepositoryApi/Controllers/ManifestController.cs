using System;
using System.Collections.Generic;
using System.Web.Http;
using FileRepositoryApi.Models;

namespace FileRepositoryApi.Controllers
{
    public class ManifestController : ApiController
    {
        // GET api/manifest/test1
        [HttpGet]
        [Route("api/manifest/{title}")]
        public string GetByTitle(string title)
        {
            var manifest = ManifestRepository.GetFileByTitle(title);
            return manifest.ReadFile();
        }

        [HttpGet]
        [Route("api/thumbnails/{title}")]
        public IEnumerable<string> GetThumbnailsByTitleFragment(string title)
        {
            var fileNames = ManifestRepository.GetFilesByFileTitleSegment(title);
            throw new NotImplementedException();
        }

        // POST api/manifest
        public void Post([FromBody]ManifestRequestModel value)
        {

        }
    }
}
