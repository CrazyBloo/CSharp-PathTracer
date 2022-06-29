using System;
using System.Linq;
using System.Numerics;

public class Program
{
    
    // Image Setup
    static float AspectRatio = 16.0f / 9.0f;
    static int ImageWidth = 400;
    static int ImageHeight = (int)(ImageWidth / AspectRatio);
    static int SamplesPerPixel = 100;

    public static string ImageBuffer = "P3\n" + ImageWidth + ' ' + ImageHeight + "\n255\n";

    public static List<Hittable> Hittables = new();

    public static Camera Camera = new();

    static void Main()
    {
        //Populate scene with objects
        Hittables.Add(new Sphere(new Vector3(0, 0, -1), 0.5f));
        Hittables.Add(new Sphere(new Vector3(0, -100.5f, -1), 100));
        
        //for every pixel in the image, calculate
        for (int j = ImageHeight - 1; j >= 0; --j)
        {
            for (int i = 0; i < ImageWidth; ++i)
            {
                CalcColor(i, j);
            }
        }
        
        File.WriteAllText("image.ppm", ImageBuffer);
    }

    public static void CalcColor(float i, float j)
    {

        Vector3 pixel_color = new(0, 0, 0);
        var rand = new Random();
        for (int s = 0; s < SamplesPerPixel; ++s)
        {
            float u = ((float)i + (float)rand.NextDouble()) / ((float)ImageWidth - 1f);
            float v = ((float)j + (float)rand.NextDouble()) / ((float)ImageHeight - 1f);
            Ray ray = Camera.GetRay(u, v);
            pixel_color += Ray.RayColor(ray, Hittables);
        }

        WriteColor(pixel_color);

        float percent = j / (float)ImageHeight;
        Console.WriteLine($"Drawing new pixel {i}, {j}, {percent * 100}% remaining");

    }


    static void WriteColor(Vector3 color)
    {
        var r = color.X;
        var g = color.Y;
        var b = color.Z;

        // Divide the color by the number of samples.
        var scale = 1.0f / SamplesPerPixel;
        r *= scale;
        g *= scale;
        b *= scale;

        var x = (int)(256 * Math.Clamp(r, 0.0f, 0.999f));
        var y = (int)(256 * Math.Clamp(g, 0.0f, 0.999f));
        var z = (int)(256 * Math.Clamp(b, 0.0f, 0.999f));


        //append to end of string in ppm format
        ImageBuffer = ImageBuffer + x + ' ' + y + ' ' + z + '\n';
    }


   

    public static Vector3 UnitVector(Vector3 vec)
    {
        return vec / vec.Length();
    }
    
    public static float DegreesToRadians(float deg)
    {
        return deg * MathF.PI * 180;
    }


}