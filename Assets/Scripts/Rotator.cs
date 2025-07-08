using System.Collections;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotate;
    private Rigidbody _rb;

    private IEnumerator Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        while(true)
        {
            _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotate * Time.fixedDeltaTime));
            //yield return new WaitForFixedUpdate();

            yield return new WaitForEndOfFrame();
        }
    }
}