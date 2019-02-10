using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    public float attackDelay = .6f;
    public delegate void OnAttack();
    public OnAttack onAttack;

    CharacterStats stats;
    float attackCooldown = 0f;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack (CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (onAttack != null)
                onAttack.Invoke();

            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(EquipmentManager.instance.CurrentEquipment.attackPower);
    }

    public bool Attackable()
    {
        return attackCooldown <= 0f;
    }

    public void ResetCooldown()
    {
        attackCooldown = 1f / attackSpeed;
    }
}
