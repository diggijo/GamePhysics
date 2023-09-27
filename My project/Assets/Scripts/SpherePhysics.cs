using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePhysics : MonoBehaviour, ICollidable
{
    internal Vector3 velocity;
    internal Vector3 acceleration;
    float gravity = 9.81f;
    internal float radius
    {
        get { return transform.localScale.x / 2.0f; }
        private set { transform.localScale = 2 * value * Vector3.one; }
    }
    internal float CoR = .75f;
    PlaneScript ps;
    Vector3 ballToPlane;
    Vector3 parallelLine;
    internal Vector3 deltaS;
    internal Vector3 deltaPos;
    internal Vector3 prevPos;
    internal Vector3 normal;
    float mass = 1;

    void Start()
    {
        ps = FindObjectOfType<PlaneScript>();
    }

    void FixedUpdate()
    {
        deltaS = velocity * Time.deltaTime;
        acceleration = gravity * Vector3.down;
        velocity += acceleration * Time.deltaTime;
        deltaPos = transform.position + deltaS;
        prevPos = transform.position;
        transform.position = deltaPos;

        ballToPlane = distance(ps.transform.position, transform.position);
        parallelLine = ICollidable.parallel(ballToPlane, ps.normal);

        float d0 = ballToPlane.magnitude;
        float d1 = parallelLine.magnitude - radius;

        if(d1 <= 0)
        {
            float t1 = (d1 / (d0 - d1)) * Time.deltaTime;
            Vector3 posAtTOI = deltaPos;
            Vector3 velocityAtTOI = velocity - acceleration * t1;
            Vector3 newVelocityAtTOI = ICollidable.rebound(velocityAtTOI, ps.normal, CoR);
            velocity = newVelocityAtTOI - acceleration * t1;
            transform.position = posAtTOI + newVelocityAtTOI * t1;
        }
    }

    internal Vector3 distance(Vector3 o, Vector3 p)
    {
        return o - p;
    }

    internal Vector3 ResolveCollisionWith(SpherePhysics otherSphere)
    {
        Vector3 normal = (transform.position - otherSphere.transform.position).normalized;
        Vector3 u1 = ICollidable.parallel(velocity, normal);
        Vector3 u2 = ICollidable.parallel(otherSphere.velocity, normal);
        Vector3 s1 = ICollidable.perpendicular(velocity, normal);
        Vector3 s2 = ICollidable.perpendicular(otherSphere.velocity, normal);

        float m1 = mass;
        float m2 = otherSphere.mass;

        Vector3 v1 = ((m1 - m2) / (m1 + m2)) * u1 + (2 * m2 / (m1 + m2)) * u2;

        Vector3 v2 = (2 * m1 / (m1 + m2)) * u1 + ((m2 - m1) / (m1 + m2)) * u2;

        velocity = v1 + s1;

        return v2 + s2;
    }
}
