using UnityEngine;

public class SkywardRunnerController : MonoBehaviour
{
    [SerializeField] private float lateralSpeed = 8f;
    [SerializeField] private float bounds = 6f;

    private SkywardRunnerStats runnerStats;
    private float targetX;

    private void Awake()
    {
        runnerStats = GetComponent<SkywardRunnerStats>();
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
            targetX = Mathf.Clamp(targetX, -bounds, bounds);
        }
    }

    private void MoveForward()
    {
        float speed = runnerStats != null ? runnerStats.CurrentSpeed : 12f;
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
    }

    private void ApplyLateralMovement()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Lerp(position.x, targetX, Time.deltaTime * 10f);
        transform.position = position;
    }
}
