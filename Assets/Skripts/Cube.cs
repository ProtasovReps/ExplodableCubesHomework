using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private int _minCloneValue = 2;
    private int _maxCloneValue = 6;

    private void OnMouseUpAsButton() => Crumble();

    private void Crumble()
    {
        int randomCloneValue = Random.Range(_minCloneValue, _maxCloneValue);

        if (Random.value <= transform.localScale.x)
        {
            for (int i = 0; i < randomCloneValue; i++)
                CreateClone().GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }

        Destroy(gameObject);
    }

    private GameObject CreateClone()
    {
        var clone = GameObject.CreatePrimitive(PrimitiveType.Cube);

        clone.transform.position = transform.position;
        clone.transform.localScale = transform.localScale / 2;

        clone.GetComponent<Renderer>().material.color = Random.ColorHSV();
        clone.AddComponent<Cube>();

        return clone;
    }
}
