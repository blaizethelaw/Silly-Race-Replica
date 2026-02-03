using UnityEngine;

public class SkywardProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifeTime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SkywardBoss>(out var boss))
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
