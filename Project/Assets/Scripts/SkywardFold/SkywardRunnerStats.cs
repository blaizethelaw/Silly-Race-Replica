using System;
using UnityEngine;

public class SkywardRunnerStats : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 12f;
    [SerializeField] private int baseFirepower = 1;

    public event Action<int> FirepowerChanged;
    public event Action<float> SpeedChanged;
    public event Action<int> PowerupsChanged;

    private float speedMultiplier = 1f;
    private int firepowerMultiplier = 1;
    private int collectedPowerups;

    public float CurrentSpeed => baseSpeed * speedMultiplier;
    public int CurrentFirepower => Mathf.Max(1, baseFirepower * firepowerMultiplier);
    public int CollectedPowerups => collectedPowerups;

    public void AddSpeedMultiplier(float delta)
    {
        speedMultiplier = Mathf.Max(0.1f, speedMultiplier + delta);
        SpeedChanged?.Invoke(CurrentSpeed);
    }

    public void MultiplySpeed(float multiplier)
    {
        speedMultiplier = Mathf.Max(0.1f, speedMultiplier * multiplier);
        SpeedChanged?.Invoke(CurrentSpeed);
    }

    public void SetFirepowerMultiplier(int multiplier)
    {
        firepowerMultiplier = Mathf.Max(1, multiplier);
        FirepowerChanged?.Invoke(CurrentFirepower);
    }

    public void MultiplyFirepower(int multiplier)
    {
        firepowerMultiplier = Mathf.Max(1, firepowerMultiplier * Mathf.Max(1, multiplier));
        FirepowerChanged?.Invoke(CurrentFirepower);
    }

    public void AddPowerups(int amount)
    {
        collectedPowerups = Mathf.Max(0, collectedPowerups + amount);
        PowerupsChanged?.Invoke(collectedPowerups);
    }

    public void ResetStats()
    {
        speedMultiplier = 1f;
        firepowerMultiplier = 1;
        collectedPowerups = 0;
        SpeedChanged?.Invoke(CurrentSpeed);
        FirepowerChanged?.Invoke(CurrentFirepower);
        PowerupsChanged?.Invoke(collectedPowerups);
    }
}
