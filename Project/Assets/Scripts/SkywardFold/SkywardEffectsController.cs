using UnityEngine;

public class SkywardEffectsController : MonoBehaviour
{
    private SkywardEvolutionSystem evolutionSystem;
    private Camera mainCamera;
    private float baseFOV;
    private TrailRenderer plasmaTrail;

    private void Awake()
    {
        evolutionSystem = GetComponent<SkywardEvolutionSystem>();
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            baseFOV = mainCamera.fieldOfView;
        }
    }

    private void OnEnable()
    {
        if (evolutionSystem != null)
        {
            evolutionSystem.StageChanged += OnStageChanged;
        }
    }

    private void OnDisable()
    {
        if (evolutionSystem != null)
        {
            evolutionSystem.StageChanged -= OnStageChanged;
        }
    }

    private void Update()
    {
        UpdateMotionBlur();
        UpdateSwingWing();
    }

    private void OnStageChanged(SkywardEvolutionStage stage, int level)
    {
        UpdatePlasmaTrail(stage);
    }

    private void UpdateMotionBlur()
    {
        if (mainCamera == null || evolutionSystem == null || evolutionSystem.CurrentStage == null)
        {
            return;
        }

        float targetFOV = evolutionSystem.CurrentStage.hasMotionBlur ? baseFOV * 1.25f : baseFOV;
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * 5f);
    }

    private void UpdateSwingWing()
    {
        if (evolutionSystem == null || evolutionSystem.CurrentStage == null || !evolutionSystem.CurrentStage.hasSwingWingAnimation)
        {
            return;
        }

        // Dummy swing wing effect: just oscillating the scale slightly or rotation
        // In a real scenario, this would affect specific child bones
    }

    private void UpdatePlasmaTrail(SkywardEvolutionStage stage)
    {
        if (stage.hasPlasmaTrail)
        {
            if (plasmaTrail == null)
            {
                GameObject trailObj = new GameObject("PlasmaTrail");
                trailObj.transform.SetParent(transform);
                trailObj.transform.localPosition = Vector3.zero;
                plasmaTrail = trailObj.AddComponent<TrailRenderer>();
                plasmaTrail.time = 0.5f;
                plasmaTrail.startWidth = 0.2f;
                plasmaTrail.endWidth = 0f;
                plasmaTrail.material = new Material(Shader.Find("Sprites/Default"));
                plasmaTrail.startColor = Color.cyan;
                plasmaTrail.endColor = new Color(0, 1, 1, 0);
            }
            plasmaTrail.enabled = true;
        }
        else if (plasmaTrail != null)
        {
            plasmaTrail.enabled = false;
        }
    }
}
