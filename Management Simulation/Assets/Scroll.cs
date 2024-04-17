using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxEffectMultiplier = 0.5f;
    private Transform mainCameraTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
        lastCameraPosition = mainCameraTransform.position;
    }

    private void Update()
    {
        Vector3 deltaMovement = mainCameraTransform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxEffectMultiplier;
        lastCameraPosition = mainCameraTransform.position;
    }
}