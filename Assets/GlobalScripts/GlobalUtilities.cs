using UnityEngine;

public static class GlobalUtilities
{
    public static Vector3 CloneVector3(Vector3 vector) => new(vector.x, vector.y, vector.z);
}
