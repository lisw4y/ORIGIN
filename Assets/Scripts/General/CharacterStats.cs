using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public bool isDead;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public abstract void TakeDamage(float amount);

    public abstract void Die();
}
