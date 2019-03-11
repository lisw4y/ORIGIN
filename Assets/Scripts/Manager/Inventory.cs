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
    public List<int> counts = new List<int>();
    public int[] shortcuts = { -1, -1, -1 };
    public int itemDetail = -1;

    public bool Add(Item item)
    {
        bool isAdded = false;
        if (item.isStackable)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].name == item.name) 
                {
                    if (counts[i] >= maxCount)
                        return false;
                    else
                        counts[i]++;
                    isAdded = true;
                }
            }
        }

        if (!isAdded)
        {
            if (items.Count >= space)
                return false;

            items.Add(item);
            counts.Add(1);
        }

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(int index)
    {
        items.RemoveAt(index);
        counts.RemoveAt(index);

        for (int i = 0; i < shortcuts.Length; i++)
        {
            if (index == shortcuts[i])
                shortcuts[i] = -1;
            else if (index < shortcuts[i])
                shortcuts[i]--;
        }

        if (index == itemDetail)
            itemDetail = -1;
        else if (index < itemDetail)
            itemDetail--;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void ReduceStock(Item item, int num)
    {
        int index = items.FindIndex(x => x.name.Equals(item.name));
        counts[index] -= num;
        if (counts[index] <= 0)
        {
            Remove(index);
        }
        else
        {
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }
    }

    public int GetCount(Item item)
    {
        int index = items.FindIndex(x => x.name.Equals(item.name));
        return index != -1 ? counts[index] : 0;
    }

    public void PutShortcut(int shortcutIndex, int itemIndex)
    {
        shortcuts[shortcutIndex] = itemIndex;

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
