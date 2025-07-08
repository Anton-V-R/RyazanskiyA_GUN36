using UnityEngine;

public class Gates : MonoBehaviour
{
    private int _score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Ball ball))
        {
            Destroy(ball.gameObject);
            _score++;
            Debug.Log($"Score: {_score}");
        }
    }
}
