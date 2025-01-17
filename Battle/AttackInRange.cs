﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInRange : MonoBehaviour {

    public GameObject ImpactVisual;
    public float FindTargetDelay = 1;
    public float AttackRange = 20;
    public float AttackFrequency = 0.25f;
    public float AttackDamage = 1;

    private ShowUnitInfo target;
    public PlayerSetupDefinition player;
    public float findTargetCounter = 0;
    public float attackCounter = 0;

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>().Info;
	}
	
    void FindTarget()
    {
        if (target != null)
            return;

        foreach (var p in RtsManager.Current.Players)
        {
            if (p == player)
            {
                continue;
            }

            foreach (var unit in p.ActiveUnits)
            {
                if (Vector3.Distance(unit.transform.position,transform.position) < AttackRange)
                {
                    target = unit.GetComponent<ShowUnitInfo>();
                    return;
                }
            }
        }
    }

    void Attack()
    {
        if (target == null)
        {
            return;
        }

       /* if (Vector3.Distance(target.transform.position, transform.position) > AttackRange)
        {
            target = null;
            return;
        }*/

        target.CurrentHealth -= AttackDamage;
        GameObject.Instantiate(ImpactVisual, target.transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update () {
        findTargetCounter += Time.deltaTime;
        if (findTargetCounter > FindTargetDelay)
        {
            FindTarget();
            findTargetCounter = 0;
        }

        attackCounter += Time.deltaTime;
        if (attackCounter > AttackFrequency)
        {
            Attack();
            attackCounter = 0;
        }

	}
}
