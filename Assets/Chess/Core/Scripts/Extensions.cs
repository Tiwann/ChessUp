using UnityEngine;

namespace Chess.Core
{
    public static class Extensions
    {
        public static Color WithOpacity(this Color Color, float Opacity)
        {
            return new Color(Color.r, Color.g, Color.b, Opacity);
        }
        
        public static Ray WithDirection(this Ray Ray, Vector3 Direction)
        {
            return new Ray(Ray.origin, Direction);
        }

        public static Vector3 Clamped(this Vector3 Vec, float MinX, float MaxX, float MinY, float MaxY, float MinZ,
            float MaxZ)
        {
            return new Vector3(Mathf.Clamp(Vec.x, MinX, MaxX), Mathf.Clamp(Vec.y, MinY, MaxY),
                Mathf.Clamp(Vec.z, MinZ, MaxZ));
        }
        
        public static Vector3 Clamped(this Vector3 Vec, Vector3 Min, Vector3 Max)
        {
            return Vec.Clamped(Min.x, Max.x, Min.y, Max.y, Min.z, Max.z);
        }

        public static Vector2 Center(this Texture2D Tex)
        {
            return new Vector2(Tex.width / 2.0f, Tex.height / 2.0f);
        }
    }
}