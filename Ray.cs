using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Ray
{
    public Vector3 Origin { get; set; }
    public Vector3 Direction { get; set; }
    
    public Ray(Vector3 origin, Vector3 direction, float time = 0f)
    {
        Origin = origin;
        Direction = direction;
    }
    
    public Vector3 At(float t)
    {
        return Origin + t * Direction;
    }

    public static Vector3 RayColor(Ray ray, List<Hittable> world, int depth)
    {
        HitRecord rec = new();
        // If we've exceeded the ray bounce limit, no more light is gathered.
        if (depth <= 0)
            return new Vector3(0, 0, 0);

        var hit = Hittable.HitWorld(ray, 0.001f, float.PositiveInfinity, ref rec, world);
        
        if (hit != null)
        {
            //Vector3 target = rec.P + rec.Normal + Program.RandomUnitVector();
            //return 0.5f * RayColor(new Ray(rec.P, target - rec.P), world, depth - 1);
            
            Ray scattered = new Ray(Vector3.Zero, Vector3.Zero);
            Vector3 attenuation = new Vector3();
            if (rec.Material.Scatter(ray, rec, ref attenuation, ref scattered))
                return attenuation * RayColor(scattered, world, depth - 1);
            return new Vector3(0, 0, 0);

        }


        var dir = Program.UnitVector(ray.Direction);


        var t = 0.5f * (dir.Y + 1.0f);

        var negt = 1.0f - t;

        return new Vector3(1.0f * negt, 1.0f * negt, 1.0f * negt) + new Vector3(0.5f * t, 0.7f * t, 1.0f * t);

    }

}
