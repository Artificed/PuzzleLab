using System.Reflection;

namespace PuzzleLab.Infrastructure.Persistence.Utils;

public class SeedHelper
{
    public static byte[] LoadImageDataFromFile(string relativeFilePath)
    {
        try
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            if (string.IsNullOrEmpty(assemblyLocation))
            {
                throw new InvalidOperationException("Could not determine assembly location.");
            }

            string baseDirectory = Path.GetDirectoryName(assemblyLocation);
            if (string.IsNullOrEmpty(baseDirectory))
            {
                throw new InvalidOperationException(
                    $"Could not determine directory from assembly location '{assemblyLocation}'.");
            }

            string fullFilePath = Path.Combine(baseDirectory, relativeFilePath);

            if (File.Exists(fullFilePath))
            {
                return File.ReadAllBytes(fullFilePath);
            }
            else
            {
                Console.Error.WriteLine($"Error loading seed data: File not found at expected path '{fullFilePath}'.");
                Console.Error.WriteLine($" > Base directory calculated as: '{baseDirectory}'");
                Console.Error.WriteLine($" > Relative path provided: '{relativeFilePath}'");
                Console.Error.WriteLine($" > Executing Assembly Location: '{assemblyLocation}'");
                throw new FileNotFoundException($"Seed data file not found.", fullFilePath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading seed file '{relativeFilePath}': {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);
            throw;
        }
    }
}