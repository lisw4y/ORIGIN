﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public float maxFood = 100f;
    public float hungerRate = 1f;
    public float hungerAmount = 1f;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public AudioClip deathClip;
    
    Animator animator;
    AudioSource playerAudio;
    PlayerController playerController;
    float currentFood;
    bool isDamaged;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();
        
        SetHealthSlider(currentHealth);

        currentFood = maxFood;
        SetFoodSlider(currentFood);

        InvokeRepeating("IncreaseHunger", hungerRate, hungerRate);
    }
    
    void Update()
    {
        if (isDamaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        isDamaged = false;
    }

    public override void TakeDamage(float amount)
    {
        isDamaged = true;
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

        SetHealthSlider(currentHealth);
        playerAudio.Play();

        if (currentHealth == 0 && !isDead)
            Die();
    }

    public override void Die()
    {
        isDead = true;
        animator.SetTrigger("death");

        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        playerController.enabled = false;
        PlayerManager.instance.Invoke("KillPlayer", 5f);
    }

    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        SetHealthSlider(currentHealth);
    }

    void IncreaseHunger()
    {
        currentFood -= (playerController.IsRunning ? hungerAmount * 2 : hungerAmount);
        if (currentFood < 0)
            currentFood = 0;

        SetFoodSlider(currentFood);

        if (currentFood == 0 && !isDead)
        {
            CancelInvoke();
            Die();
        }
    }

    public void RestoreFood(float amount)
    {
        currentFood += amount;
        if (currentFood > maxFood)
            currentFood = maxFood;

        SetFoodSlider(currentFood);
    }

    void SetHealthSlider(float amount)
    {
        HUDManager.instance.healthSlider.value = currentHealth;
        HUDManager.instance.healthText.text = currentHealth + "%";
    }

    void SetFoodSlider(float amount)
    {
        HUDManager.instance.foodSlider.value = currentFood;
        HUDManager.instance.foodText.text = currentFood + "%";
    }
}
