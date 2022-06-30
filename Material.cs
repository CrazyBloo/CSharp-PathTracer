using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Material
{
    public virtual bool Scatter(Ray ray, HitRecord rec, ref Vector3 attenuation, ref Ray scattered)
    {
        return false;
    }
    
    public Vector3 Reflect(Vector3 v, Vector3 n) {
        return v - 2*Vector3.Dot(v, n)*n;
    }

}