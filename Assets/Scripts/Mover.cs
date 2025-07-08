using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 _startLocal;
    [SerializeField] private Vector3 _endLocal;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _delay = 1f;

    private Rigidbody _rb;

    private static readonly WaitForFixedUpdate _waitForFixedUpdate = new ();

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_startLocal, 0.2f);
        Gizmos.DrawSphere(_endLocal, 0.2f);
        Gizmos.DrawLine(_startLocal, _endLocal);
    }

    private IEnumerator Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;

        WaitForSeconds delayWait = new WaitForSeconds(_delay);

        while(true)
        {
            yield return MoveBetweenPoints(_startLocal, _endLocal);
            yield return delayWait;
            yield return MoveBetweenPoints(_endLocal, _startLocal);
            yield return delayWait;
        }
    }

    private IEnumerator MoveBetweenPoints(Vector3 from, Vector3 to)
    {
        float journeyLength = Vector3.Distance(from, to);
        float startTime = Time.time;

        while(true)
        {
            float distanceCovered = (Time.time - startTime) * _speed;
            float fraction = distanceCovered / journeyLength;

            if(fraction >= 1f)
                yield break;

            _rb.MovePosition(Vector3.Lerp(from, to, fraction));
            yield return _waitForFixedUpdate;
        }
    }
}