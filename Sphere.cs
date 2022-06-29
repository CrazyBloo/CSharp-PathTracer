using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Sphere : Hittable
{

    public Vector3 Center { get; set; }
    public float Radius { get; set; }
    
    public Sphere(Vector3 center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    public override bool Hit(Ray ray, float tMin, float tMax, ref HitRecord rec)
    {

        Vector3 oc = ray.Origin - Center;

        var a = ray.Direction.LengthSquared();
        var halfb = Vector3.Dot(oc, ray.Direction);
        var c = oc.LengthSquared() - Radius * Radius;

        var discriminant = halfb * halfb - a * c;

        if (discriminant < 0) return false;
        var sqrtd = MathF.Sqrt(discriminant);

        // Find the nearest root that lies in the acceptable range.
        var root = (-halfb - sqrtd) / a;
        if (root < tMin || tMax < root)
        {
            root = (-halfb + sqrtd) / a;
            if (root < tMin || tMax < root)
                return false;
        }
        
        rec.T = root;
        rec.P = ray.At(rec.T);
        Vector3 outwordnormal = (rec.P - Center) / Radius;
        rec.SetFaceNormal(ray, outwordnormal);

        return true;

    }
}