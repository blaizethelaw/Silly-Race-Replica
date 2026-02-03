using UnityEngine;

public class SkywardRunnerController : MonoBehaviour
{
    [SerializeField] private float lateralSpeed = 8f;
    [SerializeField] private float bounds = 6f;

    private SkywardRunnerStats runnerStats;
    private SkywardEvolutionSystem evolutionSystem;
    private float targetX;
    private float externalForce;

    private void Awake()
    {
        runnerStats = GetComponent<SkywardRunnerStats>();
        evolutionSystem = GetComponent<SkywardEvolutionSystem>();
        targetX = transform.position.x;
    }

    private void Update()
    {
        HandleInput();
        MoveForward();
        ApplyLateralMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
            float delta = Input.GetAxis("Mouse X");
            targetX += delta * lateralSpeed;
        }

        targetX += externalForce * Time.deltaTime;
        targetX = Mathf.Clamp(targetX, -bounds, bounds);
    }

    public void ApplyExternalForce(float force)
    {
        externalForce = force;
    }

    private void MoveForward()
    {
        float speed = runnerStats != null ? runnerStats.CurrentSpeed : 12f;
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }

    private void ApplyLateralMovement()
    {
        bool hasThrustVectoring = evolutionSystem != null && evolutionSystem.CurrentStage != null && evolutionSystem.CurrentStage.hasThrustVectoring;

        Vector3 position = transform.position;
        if (hasThrustVectoring)
        {
            position.x = targetX; // Instant movement
        }
        else
        {
            position.x = Mathf.Lerp(position.x, targetX, Time.deltaTime * 10f);
        }
        transform.position = position;
    }
}
