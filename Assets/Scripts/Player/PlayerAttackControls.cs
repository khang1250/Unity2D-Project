using UnityEngine;

public class PlayerAttackControls : MonoBehaviour
{
    private PlayerMoveControls pMC;
    private GatherInput gI;
    private Animator anim;

    public bool attackStarted = false;
    public BoxCollider2D boxCol;

    public AudioSource source;


    void Start()
    {
        pMC = GetComponent<PlayerMoveControls>();
        gI = GetComponent<GatherInput>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
        //ResetAttack();
    }

    void Attack()
    {
        //press attack button
        if (gI.attackInput)
        {
            //check condition
            //(if we already attacking or we dont have control or knockback is true: then return and dont attack)
            if (attackStarted || pMC.hasControls == false || pMC.knockBack || pMC.onLadders)
                return;
            //condition are met: start attack
            //start animatio, set "attackStarted" to TRUE
            anim.SetBool("Attack", true);
            attackStarted = true;
            source.Play();

        }
    }

    public void ActivateAttack()
    {
        boxCol.enabled = true;
    }
    public void ResetAttack()
    {
        anim.SetBool("Attack", false);
        gI.attackInput = false;
        attackStarted = false;
        boxCol.enabled = false;
    }
}
