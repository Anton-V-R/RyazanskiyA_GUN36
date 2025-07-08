// скрипт-маркер для идентификации мяча

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private float _startVelocity = 10f;

    private Rigidbody _rb;
    private static readonly WaitForEndOfFrame _waitForEndOfFrame = new ();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 direction)
    {
        StartCoroutine(LaunchRoutine(direction));
    }

    private IEnumerator LaunchRoutine(Vector3 direction)
    {
        _rb.isKinematic = false;
        _rb.velocity = direction * _startVelocity;

        float timer = 0f;
        while(timer < _lifetime)
        {
            timer += Time.deltaTime;
            yield return _waitForEndOfFrame;
        }

        Destroy(gameObject);
    }
}