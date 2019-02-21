using UnityEngine;

public class ResourceStats : CharacterStats
{
    [Tooltip("This kind of resource can be only exploited by this equipment")]
    public string equipmentName;
    public GameObject[] generatedItems;
    public float regeneratedTime;

    AudioSource audioSource;
    float waitRegeneratedTime;
    Interactable interactable;
    MeshRenderer meshRenderer;
    CapsuleCollider capsuleCollider;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<Interactable>();
        meshRenderer = GetComponent<MeshRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    
    void Update()
    {
        if (isDead)
        {
            waitRegeneratedTime -= Time.deltaTime;
            if (waitRegeneratedTime <= 0)
            {
                isDead = false;
                currentHealth = maxHealth;
                meshRenderer.enabled = true;
                capsuleCollider.enabled = true;
            }
        }
    }

    public override void TakeDamage(float amount)
    {
        if (!isDead)
        {
            Equipment equipment = EquipmentManager.instance.CurrentEquipment;
            if (equipment.equipmentType == EquipmentType.Tool && equipment.name == equipmentName)
            {
                currentHealth -= 1;
                if (currentHealth < 0)
                    currentHealth = 0;

                interactable.SetInfo();
                audioSource.Play();

                if (currentHealth == 0)
                {
                    isDead = true;
                    waitRegeneratedTime = regeneratedTime;
                    Invoke("Die", 0.5f);
                }
            }
        }
    }

    public override void Die()
    {
        meshRenderer.enabled = false;
        capsuleCollider.enabled = false;
        interactable.CloseInfo();
        foreach (GameObject item in generatedItems)
        {
            Instantiate(item, new Vector3(transform.position.x + Random.Range(-1f, 1f),
                transform.position.y + 3f, transform.position.z + Random.Range(-1f, 1f)), transform.rotation);
        }
    }
}
