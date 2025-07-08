using UnityEngine;

public class Gates : MonoBehaviour
{
    private int _score = 0;

    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("Ball"))
        if(other.GetComponent<Ball>() != null)
        {
            Destroy(other.gameObject);
            _score++;
            Debug.Log($"Score: {_score}");
        }
    }
}
