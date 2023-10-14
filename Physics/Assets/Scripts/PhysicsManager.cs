using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsManager : MonoBehaviour, ICollidable
{

    internal List<SpherePhysics> spheres;
    internal List<GameObject> spawnedSpheres = new List<GameObject>();

    void Start()
    {
        spheres = FindObjectsOfType<SpherePhysics>().ToList();
    }

    void FixedUpdate()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            for (int j = i + 1; j < spheres.Count; j++)
            {
                if (ICollidable.isColliding(spheres[i], spheres[j]))
                {
                    SpherePhysics sph1 = spheres[i];
                    SpherePhysics sph2 = spheres[j];
                    float sumOfRadii = sph1.radius + sph2.radius;

                    sph1.ResolveCollisionWithSphere(sph1, sph2, sumOfRadii);
                }
            }
        }
    }
}
