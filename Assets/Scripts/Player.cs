using System.Collections;

using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _ready = true;
    private Rigidbody _ball;

    [SerializeField]
    private Rigidbody _ballPrefab;
    [SerializeField]
    private float _startVelocity;
    [SerializeField]
    private float _lifetime;

    [SerializeField]
    private float _respawnDelay;

    private void Update()
    {
        Debug.Log($"Update ready: {_ready}");
        if(!_ready)
            return;
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space down");

            StartCoroutine(Reloader());
            _ball.isKinematic = false;
            _ball.transform.parent = null;
            _ball.velocity = transform.forward * _startVelocity;
            Destroy(_ball.gameObject, _lifetime);
        }
    }

    private IEnumerator Reloader()
    {
        Debug.Log("Reloader");
        _ready = false;
        yield return new WaitForSeconds(_respawnDelay);
        Spawn();
    }

    private void Spawn()
    {
        Debug.Log("Spawn start");
        _ball = Instantiate(_ballPrefab, transform);
        _ball.isKinematic = true;
        _ready = true;
        Debug.Log("Spawn end");
    }

}
