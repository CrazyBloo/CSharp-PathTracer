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
    public static int MaxDepth = 50;

    public static string ImageBuffer = "P3\n" + ImageWidth + ' ' + ImageHeight + "\n255\n";

    public static List<Hittable> Hittables = new();

    public static Camera Camera = new();

    public static Random rand = new();

    static void Main()
    {
        var GroundMaterial = new Lambertian(new Vector3(0.8f, 0.8f, 0.0f));
        var CenterSphereMat = new Lambertian(new Vector3(0.7f, 0.3f, 0.3f));
        var LeftSpehereMat = new Metal(new Vector3(0.8f, 0.8f, 0.8f), 0f);
        var RightSphereMat = new Metal(new Vector3(0.8f, 0.6f, 0.2f), 0.43f);


        //Populate scene with objects
        Hittables.Add(new Sphere(new Vector3(0, 0, -1), 0.5f, CenterSphereMat));
        Hittables.Add(new Sphere(new Vector3(-1, 0, -1), 0.5f, LeftSpehereMat));
        Hittables.Add(new Sphere(new Vector3(1, 0, -1), 0.5f, RightSphereMat));
        Hittables.Add(new Sphere(new Vector3(0, -100.5f, -1), 100, GroundMaterial));

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
        for (int s = 0; s < SamplesPerPixel; ++s)
        {
            float u = ((float)i + (float)rand.NextDouble()) / ((float)ImageWidth - 1f);
            float v = ((float)j + (float)rand.NextDouble()) / ((float)ImageHeight - 1f);
            Ray ray = Camera.GetRay(u, v);
            pixel_color += Ray.RayColor(ray, Hittables, MaxDepth);
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
        r = MathF.Sqrt(scale * r);
        g = MathF.Sqrt(scale * g);
        b = MathF.Sqrt(scale * b);

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

    public static Vector3 RandomVec3()
    {
        var x = (float)rand.NextDouble();
        var y = (float)rand.NextDouble();
        var z = (float)rand.NextDouble();
        return new Vector3(x, y, z);
    }

    
    public static Vector3 RandomVec3(double min, double max)
    {
        var x = (float)GetRandomNumber(rand, min, max);
        var y = (float)GetRandomNumber(rand, min, max);
        var z = (float)GetRandomNumber(rand, min, max);
        return new Vector3(x, y, z);
    }

    

    public static double GetRandomNumber(Random random, double minValue, double maxValue)
    {
        // TODO: some validation here...
        double sample = random.NextDouble();
        return (maxValue * sample) + (minValue * (1d - sample));
    }

    public static Vector3 RandomUnitInSphere()
    {
        while (true)
        {
            var p = RandomVec3(-1, 1);
            if (p.LengthSquared() >= 1) continue;
            return p;
        }
    }

    public static Vector3 RandomUnitVector()
    {
        return UnitVector(RandomUnitInSphere());
    }
    
    public static bool NearZero(Vector3 vec)
    {
        // Return true if the vector is close to zero in all dimensions.
        var s = 1e-8;
        return (MathF.Abs(vec.X) < s) && (MathF.Abs(vec.Y) < s) && (MathF.Abs(vec.Z) < s);
    }

}