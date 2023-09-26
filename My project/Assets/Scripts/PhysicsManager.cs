using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsManager : MonoBehaviour, ICollidable
{

    List<SpherePhysics> Spheres;

    // Start is called before the first frame update
    void Start()
    {
        Spheres = FindObjectsOfType<SpherePhysics>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Spheres.Count; i++)
        {
            for (int j = i + 1; j < Spheres.Count; j++)
            {
                if (ICollidable.isColliding(Spheres[i], Spheres[j]))
                {
                    Spheres[j].velocity = Spheres[i].ResolveCollisionWith(Spheres[j]);
                }
            }
        }
    }
}
