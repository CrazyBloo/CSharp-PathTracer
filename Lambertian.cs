using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Lambertian : Material
{
    
    public Vector3 Albedo;
    
    public Lambertian(Vector3 a)
    {
        Albedo = a;
    }
    
    public override bool Scatter(Ray ray, HitRecord rec, ref Vector3 attenuation, ref Ray scattered)
    {
        Vector3 target = rec.Normal + Program.RandomUnitVector();
        
        // Catch degenerate scatter direction
        if (Program.NearZero(target))
            target = rec.Normal;

        scattered = new Ray(rec.P, target);
        attenuation = Albedo;
        return true;
    }
}