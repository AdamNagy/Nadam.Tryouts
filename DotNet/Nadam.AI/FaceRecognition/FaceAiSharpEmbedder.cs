using FaceAiSharp;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FaceRecognition;

public class FaceAiSharpEmbedder
{
    public IEnumerable<EmbeddingRectangle> GenerateEmbeddings(byte[] image)
    {
        var img = Image.Load<Rgb24>(image);

        var faceDetector = FaceAiSharpBundleFactory.CreateFaceDetectorWithLandmarks();
        var faceEmbeddingsGenerator = FaceAiSharpBundleFactory.CreateFaceEmbeddingsGenerator();

        var faces = faceDetector.DetectFaces(img);

        var results = new List<EmbeddingRectangle>();
        foreach (var face in faces)
        {
            var clone = img.Clone();
            faceEmbeddingsGenerator.AlignFaceUsingLandmarks(clone, face.Landmarks!);

            var embedding1 = faceEmbeddingsGenerator.GenerateEmbedding(clone);

            results.Add(new EmbeddingRectangle(face.Box.X, face.Box.Y, face.Box.Width, face.Box.Height, embedding1));
        }

        return results;
    }
}
