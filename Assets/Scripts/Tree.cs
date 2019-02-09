using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private GameObject Player;

    private MeshRenderer Rend;

    private CapsuleCollider Coll;

    private int MaxHealthPoint = 5;

    private int HealthPoint;

    private int RegenTime = 2000;

    private float TimeToTegen = 0;

    private bool mIsDead = false;

    private AudioSource sound;

    public GameObject[] ItemsDeadState = null;

    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        HealthPoint = MaxHealthPoint;
        Rend = GetComponent<MeshRenderer>();
        Coll = GetComponent<CapsuleCollider>();
        sound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (mIsDead) return;
        InteractableItemBase item = collision.collider.gameObject.GetComponent<InteractableItemBase>();
        if (item != null)
        {
            // Hit by a weapon
            if (item.ItemType == EItemType.Weapon)
            {
                if (Player.GetComponent<PlayerController>().IsAttacking)
                {
                    if (HealthPoint > 0)
                    {
                        sound.Play();
                        HealthPoint--;
                        print("hit" + HealthPoint);
                    }
                    if (HealthPoint == 0)
                    {
                        mIsDead = true;
                        TimeToTegen = RegenTime;
                        Invoke("ShowItemsDeadState", 0.5f);
                    }
                }
            }
        }
    }

    void ShowItemsDeadState()
    {
        // Activate the items
        foreach (var item in ItemsDeadState)
        {
            item.SetActive(true);
        }
        // Hide the tree
        Rend.enabled = false;
        Coll.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsDead)
        {
            TimeToTegen -= Time.deltaTime;
            if (TimeToTegen <= 0)
            {
                mIsDead = false;
                HealthPoint = MaxHealthPoint;
                Rend.enabled = true;
                Coll.enabled = true;
            }
        }
    }
}
