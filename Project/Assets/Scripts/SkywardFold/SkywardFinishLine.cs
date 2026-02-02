using UnityEngine;

public class SkywardFinishLine : MonoBehaviour
{
    [SerializeField] private float overdriveDuration = 4f;

    private bool triggered;
    private SkywardAutoFire cachedAutoFire;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player"))
        {
            return;
        }

        triggered = true;
        cachedAutoFire = other.GetComponent<SkywardAutoFire>();
        var evolutionSystem = other.GetComponent<SkywardEvolutionSystem>();
        var runnerStats = other.GetComponent<SkywardRunnerStats>();
        var scoreReporter = other.GetComponent<SkywardScoreReporter>();

        cachedAutoFire?.SetOverdrive(true);
        scoreReporter?.ReportFinalScore(evolutionSystem, runnerStats);
        Invoke(nameof(DisableOverdrive), overdriveDuration);
    }

    private void DisableOverdrive()
    {
        triggered = false;
        cachedAutoFire?.SetOverdrive(false);
    }
}
