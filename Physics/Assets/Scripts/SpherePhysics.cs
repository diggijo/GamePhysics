using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePhysics : MonoBehaviour, ICollidable
{
    [SerializeField] private float mass = 1;
    [SerializeField] private float gravity = 9.81f;
    private Vector3 velocity;
    private Vector3 acceleration;
    internal float radius
    {
        get { return transform.localScale.x / 2.0f; }
        private set { transform.localScale = 2 * value * Vector3.one; }
    }
    private float CoR = .75f;
    private PlaneScript ps;
    private Vector3 ballToPlane;
    private Vector3 parallelLine;
    private Vector3 deltaS;
    private Vector3 prevPos;
    private PhysicsManager pm;

    void Start()
    {
        acceleration = gravity * Vector3.down;
        ps = FindObjectOfType<PlaneScript>();
        pm = FindObjectOfType<PhysicsManager>();
    }

    void FixedUpdate()
    {
        if(gameObject.tag.Equals("Player") || pm.hasCollided)
        {
            moveSphere();
        }

        ballToPlane = ICollidable.distance(ps.transform.position, transform.position);
        parallelLine = ICollidable.parallel(ballToPlane, ps.normal);

        float d0 = ballToPlane.magnitude;
        float d1 = parallelLine.magnitude - radius;

        if (d1 <= 0)
        {
            ResolveCollisionWithPlane(d0, d1);
        }
    }

    private void moveSphere()
    {
        prevPos = transform.position;
        deltaS = velocity * Time.deltaTime;
        velocity += acceleration * Time.deltaTime;
        transform.position += deltaS;
    }

    internal void ResolveCollisionWithPlane(float d0, float d1)
    {
        float t1 = d1 * (Time.deltaTime / (d1 - d0));
        Vector3 posAtTOI = transform.position - deltaS * t1;
        Vector3 velocityAtTOI = velocity - acceleration * t1;
        Vector3 newVelocityAtTOI = ICollidable.rebound(velocityAtTOI, ps.normal, CoR);
        velocity = newVelocityAtTOI - acceleration * t1;
        transform.position = posAtTOI + newVelocityAtTOI * t1;
    }

    internal void ResolveCollisionWithSphere(SpherePhysics sph1, SpherePhysics sph2, float sumOfRadii)
    {
        float dPrev = Vector3.Distance(sph1.prevPos, sph2.prevPos) - sumOfRadii;
        float dCurrent = Vector3.Distance(sph1.transform.position, sph2.transform.position) - sumOfRadii;

        float tAtTOI = dCurrent * (Time.deltaTime / (dCurrent - dPrev));
        Vector3 s1PosTOI = sph1.transform.position - sph1.velocity * tAtTOI;
        Vector3 s2PosTOI = sph2.transform.position - sph2.velocity * tAtTOI;
        Vector3 s1VelTOI = sph1.velocity - sph1.acceleration * tAtTOI;
        Vector3 s2VelTOI = sph2.velocity - sph2.acceleration * tAtTOI;

        Vector3 normal = ICollidable.distance(s1PosTOI, s2PosTOI).normalized;
        Vector3 u1 = ICollidable.parallel(s1VelTOI, normal);
        Vector3 u2 = ICollidable.parallel(s2VelTOI, normal);
        Vector3 s1 = ICollidable.perpendicular(s1VelTOI, normal);
        Vector3 s2 = ICollidable.perpendicular(s2VelTOI, normal);

        float m1 = sph1.mass;
        float m2 = sph2.mass;

        Vector3 v1 = ((m1 - m2) / (m1 + m2)) * u1 + (2 * m2 / (m1 + m2)) * u2;
        Vector3 v2 = (2 * m1 / (m1 + m2)) * u1 + ((m2 - m1) / (m1 + m2)) * u2;

        v1 = (v1 * sph1.CoR) + (sph1.acceleration * tAtTOI);
        v2 = (v2 * sph2.CoR) + (sph2.acceleration * tAtTOI);

        sph1.velocity = v1 + s1;
        sph2.velocity = v2 + s2;

        sph1.transform.position = s1PosTOI + sph1.velocity * tAtTOI;
        sph2.transform.position = s2PosTOI + sph2.velocity * tAtTOI;
    }
}
