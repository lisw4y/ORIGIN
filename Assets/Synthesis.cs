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
    private Equipment selectedItem;
    private bool goodToSynthesize;

    // Start is called before the first frame update
    void Start()
    {
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
        if (selectedItem != null)
        {
            ShowMaterials(selectedItem);
        }
        else detailPanel.SetActive(false);
    }

    public void ShowMaterials(Equipment item)
    {
        selectedItem = item;
        goodToSynthesize = true;
        foreach(GameObject e in materialSlots)
        {
            e.SetActive(false);
        }
        for (int i = 0; i < item.materials.Length; i++)
        {
            Item material = item.materials[i];
            InventorySlot materialSlot = materialSlots[i].transform.GetChild(0).GetComponent<InventorySlot>();
            int count = inventory.GetCount(material);
            bool isEnough = count >= item.amountNeeded[i];
            materialSlot.CreatSlot(material, count, isEnough);
            if (!isEnough)
                goodToSynthesize = false;

            Text amountNeeded = materialSlots[i].transform.GetChild(1).GetComponent<Text>();
            amountNeeded.text = item.amountNeeded[i].ToString();
            materialSlots[i].SetActive(true);
        }
        detailPanel.SetActive(true);
    }

    public void Synthesize()
    {
        if (goodToSynthesize)
        {
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

}
