using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class Camera
{
    
    private Vector3 Origin;
    private Vector3 LowerLeftCorner;
    private Vector3 Horizontal;
    private Vector3 Vertical;
    
    public static float AspectRatio = 16.0f / 9.0f;
    public static float ViewportHeight = 2.0f;
    public static float ViewportWidth = AspectRatio * ViewportHeight;
    public static float FocalLength = 1.0f;

    public Camera()
    {

        Origin = new Vector3(0, 0, 0);
        Horizontal = new Vector3(ViewportWidth, 0.0f, 0.0f);
        Vertical = new Vector3(0.0f, ViewportHeight, 0.0f);
        LowerLeftCorner = Origin - Horizontal / 2 - Vertical / 2 - new Vector3(0, 0, FocalLength);
    }

    public Ray GetRay(float u, float v)
    {
        return new Ray(Origin, LowerLeftCorner + u * Horizontal + v * Vertical - Origin);
    }
}