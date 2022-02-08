using System.Collections.Generic; 
using System.IO;
using System.Reflection; 
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit;

namespace Logan.Chenavier.FeatureMatching.Tests; 

public class FeatureMatchingUnitTest
{
    [Fact]
    public async Task ObjectShouldBeDetectedCorrectly()
    {
        var executingPath = GetExecutingPath(); 
        var imageScenesData = new List<byte[]>(); 
        foreach (var imagePath in Directory.EnumerateFiles(Path.Combine(executingPath, "Scenes")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath); 
            imageScenesData.Add(imageBytes);
        }
        var objectImageData = await File.ReadAllBytesAsync(Path.Combine(executingPath, "Chenavier-Logan-object.jpg"));
        var detectObjectInScenesResults = new ObjectDetection().DetectObjectInScenes(objectImageData, imageScenesData);

        var tmp = JsonSerializer.Serialize(detectObjectInScenesResults[0].Points);
        
        Assert.Equal("[{\"X\":10,\"Y\":10}]",JsonSerializer.Serialize(detectObjectInScenesResults[0].Points));

        Assert.Equal("[{\"X\":15,\"Y\":15}]",JsonSerializer.Serialize(detectObjectInScenesResults[1].Points));}
    private static string GetExecutingPath()
{
    var executingAssemblyPath = Assembly.GetExecutingAssembly().Location;
    var executingPath = Path.GetDirectoryName(executingAssemblyPath); 
    return executingPath;
}
}