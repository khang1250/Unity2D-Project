using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : EnemyAttack
{
    PlayerMoveControls playerMove;
    public float forceX;
    public float forceY;
    public float duration;
    public override void SpeacialAttack()
    {
        playerMove = PlayerStats.instance.GetComponentInParent<PlayerMoveControls>();
        StartCoroutine(playerMove.KnockBack(forceX, forceY, duration, transform.parent));
    }
}
