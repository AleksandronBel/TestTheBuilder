using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _targetToFollow;
    [SerializeField] float _lerpRate;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetToFollow.position, Time.deltaTime * _lerpRate);
    }
}
