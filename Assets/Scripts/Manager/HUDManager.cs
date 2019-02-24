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

    public GameObject HUD;
    public GameObject shortcutPanel;
    public GameObject equipmentPanel;
    public GameObject statusPanel;
    public GameObject enemyPanel;
    public GameObject inventoryPanel;
    public GameObject ItemDetailPanel;
    public GameObject messagePanel;

    [HideInInspector] public Canvas hudCanvas;
    [HideInInspector] public Slider healthSlider;
    [HideInInspector] public Text healthText;
    [HideInInspector] public Slider foodSlider;
    [HideInInspector] public Text foodText;
    [HideInInspector] public Text enemyTitleText;
    [HideInInspector] public Slider enemySlider;
    [HideInInspector] public Text enemyText;
    [HideInInspector] public Image DetailImage;
    [HideInInspector] public Text DetailCount;
    [HideInInspector] public Text DetailText;
    [HideInInspector] public Button DetailUseButton;
    [HideInInspector] public Button DetailShortcutButton1;
    [HideInInspector] public Button DetailShortcutButton2;
    [HideInInspector] public Button DetailShortcutButton3;
    [HideInInspector] public Text messageText;

    void Start()
    {
        hudCanvas = HUD.GetComponent<Canvas>();
        healthSlider = statusPanel.transform.Find("HealthSlider").GetComponent<Slider>();
        healthText = healthSlider.transform.Find("HealthText").GetComponent<Text>();
        foodSlider = statusPanel.transform.Find("FoodSlider").GetComponent<Slider>();
        foodText = foodSlider.transform.Find("FoodText").GetComponent<Text>();
        enemyTitleText = enemyPanel.transform.Find("EnemyTitleText").GetComponent<Text>();
        enemySlider = enemyPanel.transform.Find("EnemySlider").GetComponent<Slider>();
        enemyText = enemySlider.transform.Find("EnemyText").GetComponent<Text>();
        DetailImage = ItemDetailPanel.transform.Find("ItemSlot/ItemImage").GetComponent<Image>();
        DetailCount = ItemDetailPanel.transform.Find("ItemSlot/CountText").GetComponent<Text>();
        DetailText = ItemDetailPanel.transform.Find("DetailText").GetComponent<Text>();
        DetailUseButton = ItemDetailPanel.transform.Find("UseButton").GetComponent<Button>();
        DetailShortcutButton1 = ItemDetailPanel.transform.Find("ShortcutButtons/ShortcutButton").GetComponent<Button>();
        DetailShortcutButton2 = ItemDetailPanel.transform.Find("ShortcutButtons/ShortcutButton (1)").GetComponent<Button>();
        DetailShortcutButton3 = ItemDetailPanel.transform.Find("ShortcutButtons/ShortcutButton (2)").GetComponent<Button>();
        messageText = messagePanel.transform.Find("MessageText").GetComponent<Text>();
        enemyPanel.SetActive(false);
        ItemDetailPanel.SetActive(false);
        messagePanel.SetActive(false);
    }
}
