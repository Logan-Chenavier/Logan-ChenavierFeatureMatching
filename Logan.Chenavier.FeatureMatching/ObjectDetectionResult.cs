namespace Logan.Chenavier.FeatureMatching;

public record ObjectDetectionResult
{
    public byte [] ImageData { get; set; }
    public IList<ObjectDetectionResult> Points { get; set; }
}

public record ObjectDetectionPoint
{
    public double X { get; set; }
    public double Y { get; set; }
}