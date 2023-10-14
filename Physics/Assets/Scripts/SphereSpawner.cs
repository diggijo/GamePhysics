using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private GameObject targetPrefab;
    private PhysicsManager pm;
    float shootSpeed = 20f;
    private float lastSpawnX = 0.0f;
    float range = 20f;
    float gravityMin = 2f;
    float gravityMax = 5f;
    float minY = -20f;
    float timer;
    const float duration = 1f;
    const float delay = 3f;


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

        checkRemove(pm.spawnedSpheres);
    }

    private void checkRemove(List<GameObject> list)
    {
        for (int i = 0; i <= list.Count - 1; i++)
        {
            GameObject sphere = list[i];
            SpherePhysics sp = sphere.GetComponent<SpherePhysics>();
            if (sphere.transform.position.y < minY)
            {
                list.RemoveAt(i);
                pm.spheres.Remove(sphere.GetComponent<SpherePhysics>());
                Destroy(sphere);
            }
        }
    }

    private void spawnTarget()
    {
        float minDistanceBetweenSpheres = 5f;

        float randomX;
        do
        {
            randomX = Random.Range(-range, range);
        }
        while (Mathf.Abs(randomX - lastSpawnX) < minDistanceBetweenSpheres);

        Vector3 spawnPosition = new Vector3(randomX, range, range);

        GameObject sphere = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);
        sphere.SetActive(true);

        SpherePhysics spComponent = sphere.GetComponent<SpherePhysics>();

        float randomGravity = Random.Range(gravityMin, gravityMax);
        spComponent.gravity = randomGravity;
        addToList(spComponent);

        pm.spawnedSpheres.Add(sphere);

        lastSpawnX = randomX;
    }

    private void shootSphere()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePosition);

        GameObject sphere = Instantiate(spherePrefab, spawnPos, Quaternion.identity);
        sphere.SetActive(true);

        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 shootDirection = (spawnPos - cameraPos).normalized;

        SpherePhysics sp = sphere.GetComponent<SpherePhysics>();

        if (sp != null)
        {
            sp.velocity = shootDirection * shootSpeed;
        }

        pm.spawnedSpheres.Add(sphere);
        addToList(sp);
    }

    private void addToList(SpherePhysics spComponent)
    {
        if (spComponent != null)
        {
            pm.spheres.Add(spComponent);
        }
    }
}
