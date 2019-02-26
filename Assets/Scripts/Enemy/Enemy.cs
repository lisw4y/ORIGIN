using UnityEngine;
using UnityEngine.UI;

public class Enemy : Interactable
{
    new public string name;

    PlayerManager playerManager;
    CharacterStats stats;

    void Start()
    {
        interactableType = InteractableType.Enemy;
        playerManager = PlayerManager.instance;
        stats = GetComponent<CharacterStats>();
    }

    public override void ShowInfo()
    {
        HUDManager.instance.enemyTitleText.text = name;
        SetInfo();
        HUDManager.instance.enemyPanel.SetActive(true);
    }

    public override void SetInfo()
    {
        HUDManager.instance.enemySlider.value = Mathf.Round(stats.currentHealth / stats.maxHealth * 100);
        HUDManager.instance.enemyText.text = HUDManager.instance.enemySlider.value + "%";
    }

    public override void CloseInfo()
    {
        HUDManager.instance.enemyPanel.SetActive(false);
    }

    public override void Interact()
    {
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(stats);
        }
    }
}
