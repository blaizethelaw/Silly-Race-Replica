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
        if (modelAnchor == null || CurrentStage == null)
        {
            return;
        }

        if (currentModelInstance != null)
        {
            Destroy(currentModelInstance);
        }

        if (CurrentStage.modelPrefab != null)
        {
            currentModelInstance = Instantiate(CurrentStage.modelPrefab, modelAnchor);
        }
        else
        {
            currentModelInstance = CreateFallbackModel(CurrentStage);
            currentModelInstance.transform.SetParent(modelAnchor);
        }

        currentModelInstance.transform.localPosition = Vector3.zero;
        currentModelInstance.transform.localRotation = Quaternion.identity;
        currentModelInstance.transform.localScale = Vector3.one * Mathf.Max(0.01f, CurrentStage.modelScale);
    }

    private GameObject CreateFallbackModel(SkywardEvolutionStage stage)
    {
        PrimitiveType type = PrimitiveType.Cube;
        Color color = Color.white;

        switch (stage.tierName)
        {
            case "Desktop Era":
                type = PrimitiveType.Cube;
                color = stage.stageName.Contains("Cardboard") ? new Color(0.6f, 0.4f, 0.2f) : Color.white;
                break;
            case "Propeller Era":
                type = PrimitiveType.Capsule;
                color = Color.red;
                break;
            case "Jet Era":
                type = PrimitiveType.Cylinder;
                color = Color.blue;
                break;
            case "Stealth/Future Era":
                type = PrimitiveType.Sphere;
                color = Color.black;
                break;
        }

        GameObject go = GameObject.CreatePrimitive(type);
        if (go.TryGetComponent<Renderer>(out var renderer))
        {
            renderer.material = new Material(Shader.Find("Standard"));
            renderer.material.color = color;
        }

        // Remove collider from fallback model to avoid self-collision issues if any
        if (go.TryGetComponent<Collider>(out var collider))
        {
            Destroy(collider);
        }

        return go;
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
            new SkywardEvolutionStage { stageName = "Harrier Jump Jet", tierName = "Jet Era", description = "Vertical lift capability.", enablesAutoFire = true, hasVerticalLift = true },
            new SkywardEvolutionStage { stageName = "F-117 Nighthawk", tierName = "Stealth/Future Era", description = "Stealth frame.", enablesAutoFire = true, hasStealth = true },
            new SkywardEvolutionStage { stageName = "F-22 Raptor", tierName = "Stealth/Future Era", description = "Advanced thrust vectoring.", enablesAutoFire = true, hasThrustVectoring = true },
            new SkywardEvolutionStage { stageName = "SR-71 Blackbird", tierName = "Stealth/Future Era", description = "Extreme speed with motion blur.", enablesAutoFire = true, hasMotionBlur = true },
            new SkywardEvolutionStage { stageName = "F-35 Lightning II", tierName = "Stealth/Future Era", description = "Deploys drone wingmen.", enablesAutoFire = true, hasDroneWingmen = true },
            new SkywardEvolutionStage { stageName = "Aurora Darkstar", tierName = "Stealth/Future Era", description = "Plasma trails at hypersonic speed.", enablesAutoFire = true, hasPlasmaTrail = true }
        };
    }
}
