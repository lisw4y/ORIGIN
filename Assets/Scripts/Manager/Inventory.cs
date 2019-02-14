using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public int space = 15;
    public int maxCount = 99;
    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (items.Count >= space)
            return false;

        bool isAdded = false;
        if (item.isStackable)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == item.name)
                {
                    if (items[i].getCount() >= maxCount)
                        return false;
                    else
                        items[i].addCount(1);
                    isAdded = true;
                }
            }
        }

        if (!isAdded)
            items.Add(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void ReduceStock(Item item, int num)
    {
        Item stock = items.Find(x => x.name.Equals(item.name));
        stock.addCount(-num);
        if (stock.getCount() <= 0)
        {
            Remove(item);
        }
    }

    public Item GetItem(Item item)
    {
        return items.Find(x => x.name.Equals(item.name));
    }
}
