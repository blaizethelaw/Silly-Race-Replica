using UnityEngine;
using UnityEngine.Events;

public class SkywardBoss : MonoBehaviour
{
    [SerializeField] private float maxHealth = 1000f;
    [SerializeField] private UnityEvent onBossDestroyed;

    private float currentHealth;
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"Boss took {damage} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Boss Destroyed!");
        onBossDestroyed?.Invoke();

        // Visual feedback
        if (TryGetComponent<Renderer>(out var renderer))
        {
            renderer.material.color = Color.gray;
        }

        // Disable boss
        // In a real game we would play an explosion effect
    }
}
