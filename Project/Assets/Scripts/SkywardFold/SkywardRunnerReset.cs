using UnityEngine;

public class SkywardRunnerReset : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 0.5f;

    private SkywardRunnerStats runnerStats;
    private SkywardEvolutionSystem evolutionSystem;
    private float respawnTimer;
    private bool waitingForRespawn;

    private void Awake()
    {
        runnerStats = GetComponent<SkywardRunnerStats>();
        evolutionSystem = GetComponent<SkywardEvolutionSystem>();
    }

    private void Update()
    {
        if (!waitingForRespawn)
        {
            return;
        }

        respawnTimer -= Time.deltaTime;
        if (respawnTimer <= 0f)
        {
            Respawn();
        }
    }

    public void FailRun()
    {
        if (waitingForRespawn)
        {
            return;
        }

        waitingForRespawn = true;
        respawnTimer = respawnDelay;
    }

    private void Respawn()
    {
        waitingForRespawn = false;
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
        }

        runnerStats?.ResetStats();
        if (evolutionSystem != null)
        {
            evolutionSystem.ApplyEvolutionDelta(-evolutionSystem.EvolutionLevel + 1);
        }
    }
}
