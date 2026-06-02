using FaceAiSharp;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FaceRecognition;

public class FaceRecognizer
{
    public IEnumerable<Rectangle> RecognizeFace(byte[] image)
    {
        var img = Image.Load<Rgb24>(image);

        var det = FaceAiSharpBundleFactory.CreateFaceDetectorWithLandmarks();
        // var rec = FaceAiSharpBundleFactory.CreateFaceEmbeddingsGenerator();

        var faces = det.DetectFaces(img);

        foreach (var face in faces)
        {
            Console.WriteLine($"Found a face with conficence {face.Confidence}: {face.Box} {face.Box.X}");
        }

        return faces.Select(f => new Rectangle(f.Box.X, f.Box.Y, f.Box.Width, f.Box.Height));
    }
}
