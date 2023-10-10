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
                    Debug.Log("COLLISION");

                    SpherePhysics s1 = spheres[i];
                    SpherePhysics s2 = spheres[j];
                    float sumOfRadii = s1.radius + s2.radius;

                    float dPrev = Vector3.Distance(s1.prevPos, s2.prevPos) - sumOfRadii;
                    float dCurrent = Vector3.Distance(s1.transform.position, s2.transform.position) - sumOfRadii;

                    float tAtTOI = dCurrent / (dPrev - dCurrent) * Time.deltaTime;
                  
                    Vector3 s1PosTOI =s1.transform.position - s1.velocity * tAtTOI;//        s1.deltaPos;
                    Vector3 s2PosTOI = s2.transform.position - s2.velocity * tAtTOI;// s2PosTOI = s2.deltaPos;
                    Vector3 s1VelTOI = s1.velocity - s1.acceleration * tAtTOI;
                    Vector3 s2VelTOI = s2.velocity - s2.acceleration * tAtTOI;

                    Vector3 normal = s1.distance(s1PosTOI, s2PosTOI).normalized;
                    Vector3 s1NewVel = ICollidable.rebound(s1VelTOI, normal, s1.CoR);
                    Vector3 s2NewVel = ICollidable.rebound(s2VelTOI, normal, s2.CoR);

                    s1.velocity = s1NewVel - s1.acceleration * tAtTOI;
                    s2.velocity = s2NewVel - s2.acceleration * tAtTOI;
                    s1.transform.position = s1PosTOI + s1NewVel * tAtTOI;
                    s2.transform.position = s2PosTOI + s2NewVel * tAtTOI;

                    s2.velocity = s1.ResolveCollisionWith(s2);
                }
            }
        }
    }
}
