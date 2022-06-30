using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public struct HitRecord
{
    public float T;
    public Vector3 P;
    public Vector3 Normal;
    public bool FrontFace;
    public Material Material;
    

    public void SetFaceNormal(Ray ray, Vector3 outwardnormal) {
        FrontFace = Vector3.Dot(ray.Direction, outwardnormal) < 0;
        Normal = FrontFace ? outwardnormal : -outwardnormal;
    }

}


public class Hittable
{
    public virtual bool Hit(Ray r, float tMin, float tMax, ref HitRecord rec)
    {
        return false;
    }

    public static Hittable HitWorld(Ray ray, float tMin, float tMax, ref HitRecord rec, List<Hittable> Hittables)
    {
        HitRecord temprec = new HitRecord();
        var closest_so_far = tMax;

        foreach (var hittable in Hittables)
        {
            if (hittable.Hit(ray, tMin, closest_so_far, ref temprec))
            {
                closest_so_far = temprec.T;
                rec = temprec;
                return hittable;
            }
        }

        return null;

    }
}
