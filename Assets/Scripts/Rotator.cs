using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateSpeed;

    private Rigidbody _rb;
    private static readonly WaitForEndOfFrame _wait = new();

    private IEnumerator Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        while(true)
        {
            _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotateSpeed * Time.fixedDeltaTime));
            yield return _wait;
        }
    }
}