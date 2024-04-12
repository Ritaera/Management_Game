using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform _playerTransform;
    [SerializeField] private float _smoothing = 0.2f;
    private Transform _minCameraBoundary;
    private Transform _maxCameraBoundary;

    private void Awake()
    {
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        _minCameraBoundary = GameObject.Find("MinValueCamera").GetComponent<Transform>();
        _maxCameraBoundary = GameObject.Find("MaxValueCamera").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        Vector3 targetPos = new Vector3(_playerTransform.position.x, _playerTransform.position.y, this.transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, _minCameraBoundary.position.x, _maxCameraBoundary.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, _minCameraBoundary.position.y, _maxCameraBoundary.position.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, _smoothing);
    }
    
}
    
