using UnityEngine;
using UnityEngine.UI;

public class SkywardHUD : MonoBehaviour
{
    [SerializeField] private Text evolutionLevelText;
    [SerializeField] private Text powerupsText;

    private SkywardEvolutionSystem evolutionSystem;
    private SkywardRunnerStats runnerStats;

    private void Awake()
    {
        evolutionSystem = FindObjectOfType<SkywardEvolutionSystem>();
        runnerStats = FindObjectOfType<SkywardRunnerStats>();
    }

    private void OnEnable()
    {
        if (evolutionSystem != null)
        {
            evolutionSystem.StageChanged += OnStageChanged;
        }

        if (runnerStats != null)
        {
            runnerStats.PowerupsChanged += OnPowerupsChanged;
        }

        Refresh();
    }

    private void OnDisable()
    {
        if (evolutionSystem != null)
        {
            evolutionSystem.StageChanged -= OnStageChanged;
        }

        if (runnerStats != null)
        {
            runnerStats.PowerupsChanged -= OnPowerupsChanged;
        }
    }

    private void OnStageChanged(SkywardEvolutionStage stage, int level)
    {
        if (evolutionLevelText != null)
        {
            evolutionLevelText.text = $"Evolution {level:00}";
        }
    }

    private void OnPowerupsChanged(int amount)
    {
        if (powerupsText != null)
        {
            powerupsText.text = $"Power-ups {amount}";
        }
    }

    private void Refresh()
    {
        if (evolutionSystem != null)
        {
            OnStageChanged(evolutionSystem.CurrentStage, evolutionSystem.EvolutionLevel);
        }

        if (runnerStats != null)
        {
            OnPowerupsChanged(runnerStats.CollectedPowerups);
        }
    }
}
