using System.Collections;
using System.Linq;

using UnityEngine;

public class Mover : MonoBehaviour
{
	[SerializeField]
	private float _moveTime = 1f;

	[SerializeField]
	private Vector3[] _positions;

    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _delay = 2f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_positions.FirstOrDefault(), 0.2f);
        Gizmos.DrawSphere(_positions.LastOrDefault(), 0.2f);
        Gizmos.DrawLine(_positions.FirstOrDefault(), _positions.LastOrDefault());
    }

    private IEnumerator Start()
    {
        if(_positions.Length < 2)
            yield break;
        int prev = 0, curr = 1;
        var time = 0f;
        var transform = this.transform;
        while(true)
        {
            transform.position = Vector3.Lerp(_positions[prev], _positions[curr], time / _moveTime);
            time += Time.deltaTime;
            if(time >= _moveTime)
            {
                time = 0f;
                prev = curr;
                curr = (curr + 1) % _positions.Length;
                yield return new WaitForSeconds(_delay);
            }

            yield return null;
        }

    }

}
