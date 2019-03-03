namespace Nadam.Infrastructure.UTest.Gallery
{
    public class Gallery
    {
        private GalleryModel model;
        public FileManifest File{ get; private set; }

        public Gallery()
        {
            
        }

        public void Load()
        {
            model = File.Load<GalleryModel>();
        }

        public void Save()
        {
            var biteArray = new byte[10];

            File.Save(biteArray);
        }
    }
}
