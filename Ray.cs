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

    public static Vector3 RayColor(Ray ray, List<Hittable> world)
    {
        HitRecord rec = new();

        var hit = Hittable.HitWorld(ray, 0, float.PositiveInfinity, rec, world);

        if (hit != null)
        {
            hit.Hit(ray, 0, float.PositiveInfinity, ref rec);
            Console.WriteLine(rec.Normal);
            return 0.5f * (rec.Normal + new Vector3(1, 1, 1));
        }


        var dir = Program.UnitVector(ray.Direction);


        var t = 0.5f * (dir.Y + 1.0f);

        var negt = 1.0f - t;

        return new Vector3(1.0f * negt, 1.0f * negt, 1.0f * negt) + new Vector3(0.5f * t, 0.7f * t, 1.0f * t);

    }

}
