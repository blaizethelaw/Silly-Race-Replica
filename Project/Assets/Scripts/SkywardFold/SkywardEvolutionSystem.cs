using System;
using System.Collections.Generic;
using UnityEngine;

public class SkywardEvolutionSystem : MonoBehaviour
{
    [SerializeField] private Transform modelAnchor;
    [SerializeField] private List<SkywardEvolutionStage> stages = new List<SkywardEvolutionStage>();
    [SerializeField] private int startingStageIndex;

    public event Action<SkywardEvolutionStage, int> StageChanged;

    private int currentStageIndex;
    private GameObject currentModelInstance;

    public int EvolutionLevel => currentStageIndex + 1;
    public int StageCount => stages.Count;
    public SkywardEvolutionStage CurrentStage => stages.Count > 0 ? stages[currentStageIndex] : null;

    private void Awake()
    {
        if (stages.Count == 0)
        {
            stages = BuildDefaultStages();
        }

        currentStageIndex = Mathf.Clamp(startingStageIndex, 0, stages.Count - 1);
        ApplyStageVisuals();
        RaiseStageChanged();
    }

    public void ApplyEvolutionDelta(int delta)
    {
        if (stages.Count == 0)
        {
            return;
        }

        int nextIndex = Mathf.Clamp(currentStageIndex + delta, 0, stages.Count - 1);
        if (nextIndex == currentStageIndex)
        {
            return;
        }

        currentStageIndex = nextIndex;
        ApplyStageVisuals();
        RaiseStageChanged();
    }

    public void Downgrade(int delta)
    {
        ApplyEvolutionDelta(-Mathf.Abs(delta));
    }

    private void ApplyStageVisuals()
    {
        if (modelAnchor == null || CurrentStage == null || CurrentStage.modelPrefab == null)
        {
            return;
        }

        if (currentModelInstance != null)
        {
            Destroy(currentModelInstance);
        }

        currentModelInstance = Instantiate(CurrentStage.modelPrefab, modelAnchor);
        currentModelInstance.transform.localPosition = Vector3.zero;
        currentModelInstance.transform.localRotation = Quaternion.identity;
        currentModelInstance.transform.localScale = Vector3.one * Mathf.Max(0.01f, CurrentStage.modelScale);
    }

    private void RaiseStageChanged()
    {
        StageChanged?.Invoke(CurrentStage, EvolutionLevel);
    }

    private static List<SkywardEvolutionStage> BuildDefaultStages()
    {
        return new List<SkywardEvolutionStage>
        {
            new SkywardEvolutionStage { stageName = "Basic Dart", tierName = "Desktop Era", description = "White notebook paper dart." },
            new SkywardEvolutionStage { stageName = "Stable Glider", tierName = "Desktop Era", description = "Nose tape for stability." },
            new SkywardEvolutionStage { stageName = "Cardboard Flyer", tierName = "Desktop Era", description = "Durable brown cardboard texture." },
            new SkywardEvolutionStage { stageName = "Rubber Band Launcher", tierName = "Desktop Era", description = "Built-in speed boost." },
            new SkywardEvolutionStage { stageName = "Balsa Wood Glider", tierName = "Desktop Era", description = "Ultra-lightweight frame." },
            new SkywardEvolutionStage { stageName = "Wright Flyer", tierName = "Propeller Era", description = "First auto-fire pellets.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "WWI Biplane", tierName = "Propeller Era", description = "Double wings for control.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "Red Baron Triplane", tierName = "Propeller Era", description = "Triple wings, high fire rate.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "WWII Spitfire", tierName = "Propeller Era", description = "Metal fuselage, machine gun bursts.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "P-51 Mustang", tierName = "Propeller Era", description = "High fuel efficiency.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "F-86 Sabre", tierName = "Jet Era", description = "Early jet engine.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "MiG-21", tierName = "Jet Era", description = "Delta wing, slim hit-box.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "F-4 Phantom", tierName = "Jet Era", description = "Heavy armored jet.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "F-14 Tomcat", tierName = "Jet Era", description = "Swing-wing animation.", enablesAutoFire = true, hasSwingWingAnimation = true },
            new SkywardEvolutionStage { stageName = "Harrier Jump Jet", tierName = "Jet Era", description = "Vertical lift capability.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "F-117 Nighthawk", tierName = "Stealth/Future Era", description = "Stealth frame.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "F-22 Raptor", tierName = "Stealth/Future Era", description = "Advanced thrust vectoring.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "SR-71 Blackbird", tierName = "Stealth/Future Era", description = "Extreme speed with motion blur.", enablesAutoFire = true },
            new SkywardEvolutionStage { stageName = "F-35 Lightning II", tierName = "Stealth/Future Era", description = "Deploys drone wingmen.", enablesAutoFire = true, hasDroneWingmen = true },
            new SkywardEvolutionStage { stageName = "Aurora Darkstar", tierName = "Stealth/Future Era", description = "Plasma trails at hypersonic speed.", enablesAutoFire = true, hasPlasmaTrail = true }
        };
    }
}
