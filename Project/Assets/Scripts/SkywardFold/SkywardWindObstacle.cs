using UnityEngine;

public class SkywardWindObstacle : MonoBehaviour
{
    [SerializeField] private float windForce = 5f;
    [SerializeField] private bool windToRight = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var controller = other.GetComponent<SkywardRunnerController>();
            if (controller != null)
            {
                float force = windToRight ? windForce : -windForce;
                controller.ApplyExternalForce(force);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var controller = other.GetComponent<SkywardRunnerController>();
            if (controller != null)
            {
                controller.ApplyExternalForce(0f);
            }
        }
    }
}
