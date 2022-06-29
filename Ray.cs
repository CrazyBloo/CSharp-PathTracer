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

    public static Vector3 RayColor(Ray ray)
    {
        var hit = HitSphere(new Vector3(0, 0, -1), 0.5f, ray);
        if (hit > 0.0f)
        {
            Vector3 N = Program.UnitVector(ray.At(hit) - new Vector3(0, 0, -1));
            return 0.5f * new Vector3(N.X + 1, N.Y + 1, N.Z + 1f);
        }

        var dir = Program.UnitVector(ray.Direction);


        var t = 0.5f * (dir.Y + 1.0f);

        var negt = 1.0f - t;

        return new Vector3(1.0f * negt, 1.0f * negt, 1.0f * negt) + new Vector3(0.5f * t, 0.7f * t, 1.0f * t);

    }

    public static float HitSphere(Vector3 Center, float radius, Ray ray) {
        
        Vector3 oc = ray.Origin - Center;
        
        var a = ray.Direction.LengthSquared();
        var halfb = Vector3.Dot(oc, ray.Direction);
        var c = oc.LengthSquared() - radius * radius;
        
        var discriminant = halfb*halfb - a * c;

        if (discriminant < 0)
        {
            return -1.0f;
        }
        else
        {
            return (-halfb - MathF.Sqrt(discriminant)) / a;
        }

    }

}
