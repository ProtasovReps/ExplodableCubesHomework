using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private Rigidbody _cube;
    [SerializeField] private ParticleSystem _explosion;

    private int _minCloneValue = 2;
    private int _maxCloneValue = 6;

    private void OnMouseUpAsButton() => Crumble();

    private void Crumble()
    {
        int randomCloneValue = Random.Range(_minCloneValue, _maxCloneValue);

        if (Random.value <= transform.localScale.x)
        {
            for (int i = 0; i < randomCloneValue; i++)
                CreateClone();
        }
        else
        {
            Explode();
        }

        Destroy(gameObject);
    }

    private void CreateClone()
    {
        var clone = Instantiate(_cube);

        clone.transform.localScale = transform.localScale / 2;
        clone.GetComponent<Renderer>().material.color = Random.ColorHSV();
        clone.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }

    private void Explode()
    {
        float scaleBuff = 1 / transform.localScale.x;

        foreach (Collider hit in Physics.OverlapSphere(transform.position, _explosionRadius))
        {
            if (hit.TryGetComponent<Cube>(out _))
                hit.attachedRigidbody.AddExplosionForce(_explosionForce * scaleBuff, transform.position, _explosionRadius * scaleBuff);
        }

        Instantiate(_explosion, transform.position, Quaternion.identity);
    }
}
