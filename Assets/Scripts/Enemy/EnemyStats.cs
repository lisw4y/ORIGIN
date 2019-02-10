using UnityEngine;

public class EnemyStats : CharacterStats
{
    public Item[] generatedItems;
    
    AudioSource audioSource;
    Interactable interactable;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<Interactable>();
    }

    public override void TakeDamage(float amount)
    {
        if (!isDead)
        {
            Equipment equipment = EquipmentManager.instance.CurrentEquipment;
            if (equipment.equipmentType == EquipmentType.Tool)
            {
                currentHealth -= equipment.attackPower;
                if (currentHealth < 0)
                    currentHealth = 0;

                interactable.SetInfo();
                audioSource.Play();

                if (currentHealth == 0)
                {
                    isDead = true;
                    Invoke("Die", 0.5f);
                }
            }
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
        interactable.CloseInfo();
        foreach (Item item in generatedItems)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
    }
}
