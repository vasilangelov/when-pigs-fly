using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float _moved = 0;
    private bool _isReverse = false;

    public Transform _pointA;
    public Transform _pointB;
    public float _speed = .25f;

    private void Update()
    {
        float multiplier = this._isReverse ? -1 : 1;

        this._moved += multiplier * this._speed * Time.deltaTime;

        if (this._moved < 0)
        {
            this._moved = 0;
            this._isReverse = false;
        }

        if (this._moved > 1)
        {
            this._moved = 1;
            this._isReverse = true;
        }

        transform.position = Vector3.Lerp(this._pointA.position, this._pointB.position, this._moved);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.Tags.Player))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag(GlobalConstants.Tags.Player))
        {
            other.transform.SetParent(null);
        }
    }
}
