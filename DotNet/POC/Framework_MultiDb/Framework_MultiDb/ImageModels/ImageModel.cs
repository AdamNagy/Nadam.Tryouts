namespace MultiDb.ImageModels
{
    public class WebImage
    {
        public string ThumbnailImageSource { get; set; }
        public string RealImageSource { get; set; }
    }

    public class StoredImage
    {
        public string Title { get; set; }
    }

    public class HibridImage
    {
        public string ThumbnailImageSource { get; set; }
        public string RealImageSource { get; set; }
        public string Route { get; set; }
    }
}
