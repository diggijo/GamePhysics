using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollidable
{
    static public Vector3 parallel(Vector3 v, Vector3 normal)
    {
        return (Vector3.Dot(v, normal) * normal);
    }

    static public Vector3 perpendicular(Vector3 v, Vector3 normal)
    {
        return (v - parallel(v, normal));
    }

    static bool isColliding(SpherePhysics s1, SpherePhysics s2)
    {
        return Vector3.Distance(s1.transform.position,
                                s2.transform.position) <
                                s1.radius + s2.radius;
    }
}
