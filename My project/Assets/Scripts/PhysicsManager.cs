using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhysicsManager : MonoBehaviour, ICollidable
{

    List<SpherePhysics> spheres;

    // Start is called before the first frame update
    void Start()
    {
        spheres = FindObjectsOfType<SpherePhysics>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            for (int j = i + 1; j < spheres.Count; j++)
            {
                if (ICollidable.isColliding(spheres[i], spheres[j]))
                {
                    spheres[j].velocity = spheres[i].ResolveCollisionWith(spheres[j]);
                }
            }
        }
    }
}
