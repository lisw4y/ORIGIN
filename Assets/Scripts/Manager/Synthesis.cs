using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Synthesis : MonoBehaviour
{
    private Inventory inventory;
    public Equipment[] items;
    public GameObject[] selectContents;
    public GameObject detailPanel;
    public GameObject[] materialSlots;
    public GameObject createButton;
    private AudioSource audioSource;
    private Equipment selectedItem;
    private bool goodToSynthesize;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inventory = Inventory.instance;
        for (int i = 0; i < selectContents.Length; i++)
        {
            selectContents[i].SetActive(i < items.Length);
        }
        ResetDetailPanel();

        // instantiate selectContent for each item in items?
        // or layout in Unity

    }

    private void OnEnable()
    {
        ResetDetailPanel();
    }

    private void ResetDetailPanel()
    {
        HUDManager.instance.ItemDetailPanel.SetActive(false);
        /*if (selectedItem != null)
        {
            ShowMaterials(selectedItem);
        }
        else */detailPanel.SetActive(false);
    }

    public void OnSelectItemButton(Equipment item)
    {
        ShowMaterials(item);
    }

    public void ShowMaterials(Equipment item)
    {
        StartCoroutine(FlashDetailPanel());
        //detailPanel.SetActive(false);
        if (inventory == null)
            inventory = Inventory.instance;

        selectedItem = item;
        goodToSynthesize = true;
        foreach(GameObject e in materialSlots)
        {
            e.SetActive(false);
        }
        for (int i = 0; i < item.materials.Length; i++)
        {
            Item material = item.materials[i];
            DisplaySlot materialSlot = materialSlots[i].transform.GetChild(0).GetComponent<DisplaySlot>();
            int count = inventory.GetCount(material);
            bool isEnough = count >= item.amountNeeded[i];
            materialSlot.CreateSlot(material, count, isEnough);
            if (!isEnough)
                goodToSynthesize = false;

            Text amountNeeded = materialSlots[i].transform.GetChild(1).GetComponent<Text>();
            amountNeeded.text = item.amountNeeded[i].ToString();
            materialSlots[i].SetActive(true);
        }
        createButton.SetActive(goodToSynthesize);
    }

    public void OnCreateButton()
    {
        if (goodToSynthesize)
        {
            audioSource.Play();
            // subtract amount of material by amount needed
            for (int i = 0; i < selectedItem.materials.Length; i++)
            {
                inventory.ReduceStock(selectedItem.materials[i], selectedItem.amountNeeded[i]);
            }
            // add selected Item to inventory
            inventory.Add(selectedItem);
            ShowMaterials(selectedItem);
        }
    }

    IEnumerator FlashDetailPanel()
    {
        detailPanel.SetActive(false);
        yield return new WaitForSeconds(0.05f);
        detailPanel.SetActive(true);
    }

}
