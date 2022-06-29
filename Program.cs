using System;
using System.Linq;
using System.Numerics;

public class Program
{
    
    // Image Setup
    static float AspectRatio = 16.0f / 9.0f;
    static int ImageWidth = 400;
    static int ImageHeight = (int)(ImageWidth / AspectRatio);

    //Viewport Setup
    static float ViewportHeight = 2.0f;
    static float ViewportWidth = AspectRatio * ViewportHeight;
    static float FocalLength = 1.0f;
    
    //Viewport Transforms
    static Vector3 Origin = new Vector3(0, 0, 0);
    static Vector3 Horizontal = new Vector3(ViewportWidth, 0, 0);
    static Vector3 Vertical = new Vector3(0, ViewportHeight, 0);
    static Vector3 LowerLeftCorner = Origin - Horizontal / 2 - Vertical / 2 - new Vector3(0, 0, FocalLength);

    public static string ImageBuffer = "P3\n" + ImageWidth + ' ' + ImageHeight + "\n255\n";
    
    static void Main()
    {
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
        float u = (float)i / ((float)ImageWidth - 1f);
        float v = (float)j / ((float)ImageHeight - 1f);

        var ray = new Ray(Origin, LowerLeftCorner + u * Horizontal + v * Vertical - Origin);

        Vector3 pixel_color = Ray.RayColor(ray);

        WriteColor(pixel_color);

        float percent = j / (float)ImageHeight;

        Console.WriteLine($"Drawing new pixel {i}, {j}, {percent * 100}% remaining");

    }


    static void WriteColor(Vector3 color)
    {
        //convert the color from 0.0 to 1.0 to 0 to 255
        int x = (int)(255 * color.X);
        int y = (int)(255 * color.Y);
        int z = (int)(255 * color.Z);

        //append to end of string in ppm format
        ImageBuffer = ImageBuffer + x + ' ' + y + ' ' + z + '\n';
    }


   

    public static Vector3 UnitVector(Vector3 vec)
    {
        return vec / vec.Length();
    }

}