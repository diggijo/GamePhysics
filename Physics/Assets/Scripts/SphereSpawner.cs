using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spherePrefab;
    private PhysicsManager pm;

    float rangeX = 10f;
    float gravityMin = 2f;
    float gravityMax = 5f;
    float heightY = 12f;
    float spawnZ = 7f;
    float timer;
    const float duration = 1f;
    float minY = -10f;
    private List<GameObject> spawnedSpheres = new List<GameObject>();

    void Start()
    {
        pm = FindObjectOfType<PhysicsManager>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            spawnTarget();
            timer = 0f;
        }

        if (Input.GetMouseButtonDown(0))
        {
            shootSphere();
        }

        checkRemove();
    }

    private void checkRemove()
    {
        for (int i = 0; i<= spawnedSpheres.Count - 1; i++)
        {
            GameObject sphere = spawnedSpheres[i];
            if (sphere.transform.position.y < minY)
            {
                spawnedSpheres.RemoveAt(i);
                pm.spheres.Remove(sphere.GetComponent<SpherePhysics>());
                Destroy(sphere);
            }
        }
    }

    private void spawnTarget()
    {
        {
            float randomX = Random.Range(-rangeX, rangeX);

            Vector3 spawnPosition = new Vector3(randomX, heightY, spawnZ);

            GameObject sphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
            sphere.SetActive(true);

            SpherePhysics spComponent = sphere.GetComponent<SpherePhysics>();

            float randomGravity = Random.Range(gravityMin, gravityMax);
            spComponent.gravity = randomGravity;
            addToList(spComponent);

            spawnedSpheres.Add(sphere);
        }
    }

    private void shootSphere()
    {
        Vector3 mousePosition = Input.mousePosition;

        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        GameObject sphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
        sphere.SetActive(true);

        SpherePhysics spComponent = sphere.GetComponent<SpherePhysics>();

        addToList(spComponent);
    }

    private void addToList(SpherePhysics spComponent)
    {
        if (spComponent != null)
        {
            pm.spheres.Add(spComponent);
        }
    }
}
