using UnityEngine;
using UnityEngine.Events;

public class SkywardScoreReporter : MonoBehaviour
{
    [SerializeField] private UnityEvent<int> onFinalScore;

    public void ReportFinalScore(SkywardEvolutionSystem evolutionSystem, SkywardRunnerStats runnerStats)
    {
        if (evolutionSystem == null || runnerStats == null)
        {
            return;
        }

        int finalScore = evolutionSystem.EvolutionLevel * Mathf.Max(1, runnerStats.CollectedPowerups);
        onFinalScore?.Invoke(finalScore);
        Debug.Log($"Final Score: {finalScore}");
    }
}
