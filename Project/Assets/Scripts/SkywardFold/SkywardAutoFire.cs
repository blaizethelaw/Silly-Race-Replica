using UnityEngine;

public class SkywardAutoFire : MonoBehaviour
{
    [SerializeField] private Transform fireOrigin;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRate = 6f;
    [SerializeField] private float projectileSpeed = 30f;
    [SerializeField] private float spreadAngle = 8f;

    private SkywardRunnerStats runnerStats;
    private SkywardEvolutionSystem evolutionSystem;
    private float fireTimer;
    private bool overdrive;

    private void Awake()
    {
        runnerStats = GetComponent<SkywardRunnerStats>();
        evolutionSystem = GetComponent<SkywardEvolutionSystem>();
    }

    private void Update()
    {
        if (projectilePrefab == null || fireOrigin == null)
        {
            return;
        }

        if (evolutionSystem != null && evolutionSystem.CurrentStage != null && !evolutionSystem.CurrentStage.enablesAutoFire)
        {
            return;
        }

        float effectiveRate = overdrive ? fireRate * 2.5f : fireRate;
        fireTimer += Time.deltaTime * effectiveRate;
        if (fireTimer >= 1f)
        {
            fireTimer = 0f;
            FireBurst();
        }
    }

    public void SetOverdrive(bool active)
    {
        overdrive = active;
    }

    private void FireBurst()
    {
        int shots = runnerStats != null ? runnerStats.CurrentFirepower : 1;
        shots = Mathf.Max(1, shots);
        float step = shots > 1 ? spreadAngle / (shots - 1) : 0f;
        float startAngle = -spreadAngle * 0.5f;

        for (int i = 0; i < shots; i++)
        {
            float angle = startAngle + step * i;
            Quaternion rotation = fireOrigin.rotation * Quaternion.Euler(0f, angle, 0f);
            GameObject projectile = Instantiate(projectilePrefab, fireOrigin.position, rotation);
            if (projectile.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                rigidbody.velocity = projectile.transform.forward * projectileSpeed;
            }
        }
    }
}
