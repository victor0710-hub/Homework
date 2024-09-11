using UnityEngine;

[CreateAssetMenu(fileName = "NewLifeData", menuName = "Game Data/Life Data")]
public class Life : ScriptableObject
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    // You can add methods to modify health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}
