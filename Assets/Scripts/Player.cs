using System.Collections;

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private float _respawnDelay = 1f;

    private bool _ready = true;
    private WaitForSeconds _respawnWait;
    private static readonly WaitForEndOfFrame _waitForEndOfFrame = new ();

    private void Awake()
    {
        // Инициализируем здесь, так как можем получить доступ к полю _respawnDelay
        _respawnWait = new WaitForSeconds(_respawnDelay);
    }

    private void Update()
    {
        if(_ready && Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBall();
        }
    }

    private void SpawnBall()
    {
        _ready = false;
        Ball ball = Instantiate(_ballPrefab, transform.position + transform.forward, Quaternion.identity);
        ball.Launch(transform.forward);
        StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        yield return _respawnWait;
        _ready = true;
    }
}