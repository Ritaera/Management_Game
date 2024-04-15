using System;
using UnityEngine;


public class MainCamera : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private float _smoothing = 0.2f;
    private Transform minCameraBoundary;
    private Transform maxCameraBoundary;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        minCameraBoundary = GameObject.Find("MinValueCamera").GetComponent<Transform>();
        maxCameraBoundary = GameObject.Find("MaxValueCamera").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.position.x, maxCameraBoundary.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.position.y, maxCameraBoundary.position.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, _smoothing);
    }
    
}
    
