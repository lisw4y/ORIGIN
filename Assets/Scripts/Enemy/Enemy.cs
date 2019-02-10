using UnityEngine;
using UnityEngine.UI;

public class Enemy : Interactable
{
    new public string name;
    public GameObject enemyPanel;
    public AudioSource audioClip;

    PlayerManager playerManager;
    CharacterStats stats;
    Text enemyTitleText;
    Slider enemySlider;
    Text enemyText;

    void Start()
    {
        interactableType = InteractableType.Enemy;
        playerManager = PlayerManager.instance;
        stats = GetComponent<CharacterStats>();
        enemyTitleText = enemyPanel.transform.Find("EnemyTitleText").GetComponentInChildren<Text>();
        enemySlider = enemyPanel.transform.Find("EnemySlider").GetComponentInChildren<Slider>();
        enemyText = enemySlider.transform.Find("EnemyText").GetComponentInChildren<Text>();
    }

    public override void ShowInfo()
    {
        enemyTitleText.text = name;
        SetInfo();
        enemyPanel.SetActive(true);
    }

    public override void SetInfo()
    {
        enemySlider.value = Mathf.Round(stats.currentHealth / stats.maxHealth * 100);
        enemyText.text = enemySlider.value + "%";
    }

    public override void CloseInfo()
    {
        enemyPanel.SetActive(false);
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
