using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectilePrefab;
    [Range(0, 100)] [SerializeField] private float _launchSpeed = 10.0f;
    [SerializeField] private Transform _offset;
    [Range(0, 10)] private float _projectedLength = 1f;

    public Rigidbody ProjectilePrefab {
        get { return _projectilePrefab; }
    }

    public float LaunchSpeed {
        get { return _launchSpeed; }
    }

    public Transform Offset {
        get { return _offset; }
        set { _offset = value; }
    }

    public float ProjectedLength {
        get { return _projectedLength; }
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Fire();
        }
    }

    [ContextMenu("Fire")]
    public void Fire() {
        Debug.Log("Fire");

        var ball = Instantiate(_projectilePrefab);
        ball.transform.position = _offset.position;
        ball.velocity = transform.forward * _launchSpeed;
    }
}
