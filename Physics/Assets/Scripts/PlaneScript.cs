using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour, ICollidable
{
    internal Vector3 normal;
    [SerializeField] Material flashMaterial;
    private float flashDuration = 0.2f;
    private Material originalMaterial;
    private Renderer rndr;

    private void Start()
    {
        rndr = GetComponent<Renderer>();
        originalMaterial = rndr.material;
    }

    public void Flash()
    {
        rndr.material = flashMaterial;

        StartCoroutine(ResetMaterial());
    }

    private IEnumerator ResetMaterial()
    {
        yield return new WaitForSeconds(flashDuration);

        rndr.material = originalMaterial;
    }
    void Update()
    {
        normal = transform.up.normalized;
    }
}
