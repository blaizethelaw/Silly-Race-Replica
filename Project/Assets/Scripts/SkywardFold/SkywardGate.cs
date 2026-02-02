using UnityEngine;

public class SkywardGate : MonoBehaviour
{
    public enum GateType
    {
        Evolution,
        Firepower,
        Speed,
        Gravity,
        Shredder
    }

    [SerializeField] private GateType gateType = GateType.Evolution;
    [SerializeField] private int evolutionDelta = 1;
    [SerializeField] private int firepowerMultiplier = 2;
    [SerializeField] private float speedMultiplier = 1.1f;
    [SerializeField] private int powerupReward = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        var evolutionSystem = other.GetComponent<SkywardEvolutionSystem>();
        var runnerStats = other.GetComponent<SkywardRunnerStats>();
        var runnerReset = other.GetComponent<SkywardRunnerReset>();

        switch (gateType)
        {
            case GateType.Evolution:
                evolutionSystem?.ApplyEvolutionDelta(evolutionDelta);
                runnerStats?.AddPowerups(powerupReward);
                break;
            case GateType.Firepower:
                runnerStats?.MultiplyFirepower(firepowerMultiplier);
                runnerStats?.AddPowerups(powerupReward);
                break;
            case GateType.Speed:
                runnerStats?.MultiplySpeed(speedMultiplier);
                runnerStats?.AddPowerups(powerupReward);
                break;
            case GateType.Gravity:
                evolutionSystem?.Downgrade(Mathf.Abs(evolutionDelta));
                runnerStats?.MultiplySpeed(0.85f);
                break;
            case GateType.Shredder:
                runnerReset?.FailRun();
                break;
        }
    }
}
