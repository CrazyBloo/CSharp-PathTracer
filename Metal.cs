using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Metal : Material
{

    public Vector3 Albedo;

    public Metal(Vector3 albedo)
    {
        Albedo = albedo;
    }
    
    public override bool Scatter(Ray rayin, HitRecord rec, ref Vector3 attenuation, ref Ray scattered) {
        Vector3 reflected = Reflect(Program.UnitVector(rayin.Direction), rec.Normal);
        scattered = new Ray(rec.P, reflected);
        attenuation = Albedo;
        return (Vector3.Dot(scattered.Direction, rec.Normal) > 0);
   }


}