using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    #region Singleton

    public static HUDManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject shortcutPanel;
    public GameObject equipmentPanel;
    public GameObject statusPanel;
    public GameObject enemyPanel;
    public GameObject inventoryPanel;
    public GameObject messagePanel;

    [HideInInspector] public Slider healthSlider;
    [HideInInspector] public Text healthText;
    [HideInInspector] public Slider foodSlider;
    [HideInInspector] public Text foodText;
    [HideInInspector] public Text enemyTitleText;
    [HideInInspector] public Slider enemySlider;
    [HideInInspector] public Text enemyText;
    [HideInInspector] public Text messageText;

    void Start()
    {
        healthSlider = statusPanel.transform.Find("HealthSlider").GetComponent<Slider>();
        healthText = healthSlider.transform.Find("HealthText").GetComponent<Text>();
        foodSlider = statusPanel.transform.Find("FoodSlider").GetComponent<Slider>();
        foodText = foodSlider.transform.Find("FoodText").GetComponent<Text>();
        enemyTitleText = enemyPanel.transform.Find("EnemyTitleText").GetComponent<Text>();
        enemySlider = enemyPanel.transform.Find("EnemySlider").GetComponent<Slider>();
        enemyText = enemySlider.transform.Find("EnemyText").GetComponent<Text>();
        messageText = messagePanel.transform.Find("MessageText").GetComponent<Text>();
        enemyPanel.SetActive(false);
        messagePanel.SetActive(false);
    }
}
