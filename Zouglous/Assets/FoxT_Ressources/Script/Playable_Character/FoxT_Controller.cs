using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxT_Controller : MonoBehaviour
{
    [SerializeField]
    private FoxT_PlayableCharacterStats stat;

    private Rigidbody2D PCrb;
    private Animator anim;

    private Vector2 move;
    private bool rightDirection, upDirection;

    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private float speed;
    private Vector2 lastDirection = Vector2.right;

    [SerializeField]
    private string[] state;
    private string currentState;

    [SerializeField]
    private float meleeAttackDelay, meleeAttackCoolDown, dieAnimationDelay;
    [SerializeField]
    private Vector2 MeleeAttackColliderSize;
    private bool isAttackingMelee;
    private int meleeAttackDamage;

    [SerializeField]
    private int healthPoint;
    private bool isDying, isTakingDamage;
    private void Awake()
    {
        //Refere to the PC's rigidbody
        PCrb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();

        healthPoint = stat.maxHealth;
        meleeAttackDamage = stat.meleeAttackDamage;
    }

    void Start()
    {
        //Update HUD

    }


    void Update()
    {
        if (isDying) return;
        //Check input
        InputHandler();
        //Move
        MovementUpdate();
    }

    void InputHandler()
    {
        //check Joystick value
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        //Attack Button
        if (Input.GetButtonDown("Melee Attack"))
        {
            MeleeAttackStart();
        }

        //Test DIE
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            TakeDamage(1);
        }

        if (move == Vector2.zero || move.x == 0 || isAttackingMelee) return;
        transform.localScale = new Vector3(1 * Mathf.Sign(Input.GetAxis("Horizontal")), transform.localScale.y, transform.localScale.z);
    }

    void MeleeAttackStart()
    {
        if (isAttackingMelee) return;

        isAttackingMelee = true;
        StartCoroutine(MeleeAttackCoolDown());
        StartCoroutine(MeleeAttackUpdate());
    }

    private IEnumerator MeleeAttackUpdate()
    {
        yield return new WaitForSeconds(meleeAttackDelay);

        RaycastHit2D[] meleeAttack = Physics2D.BoxCastAll(this.transform.position + new Vector3(MeleeAttackColliderSize.x * lastDirection.x / 2, MeleeAttackColliderSize.y * lastDirection.y / 2), MeleeAttackColliderSize, 0f, lastDirection, 1f, enemyLayer);

        if (meleeAttack.Length != 0)
        {
            Debug.Log("TOUCHE");
            Debug.Log(lastDirection);
        }
    }

    private IEnumerator MeleeAttackCoolDown()
    {
        //Manage Animation
        AnimationUpdate(6);
        yield return new WaitForSeconds(meleeAttackCoolDown);
        isAttackingMelee = false;
    }

    void MovementUpdate()
    {
        //Movement fonctions
        PCrb.velocity = move.normalized * speed * Time.deltaTime;

        //calculer moovement direction;
        if (move.x > 0)
        {
            rightDirection = true;
            if (move.y > 0)
            {
                upDirection = true;
            }
            else if (move.y < 0)
            {
                upDirection = false;
            }
        }
        else if (move.x < 0)
        {
            rightDirection = false;
            if (move.y > 0)
            {
                upDirection = true;
            }
            else if (move.y < 0)
            {
                upDirection = false;
            }
        }
        else
        {
            if (move.y > 0)
            {
                upDirection = true;
            }
            else if (move.y < 0)
            {
                upDirection = false;
            }
        }

        if (isTakingDamage || isAttackingMelee) return;
        if (move == Vector2.zero) AnimationUpdate(0);
        else AnimationUpdate(4);
    }

    //Animation Update function
    void ChangeAnimationState(string newState)
    {
        if (newState == currentState) return;

        currentState = newState;

        anim.Play(currentState);
    }

    public void TakeDamage(int damage)
    {
        //healthPoint = -1 is god mod
        if (healthPoint - damage <= 0 && healthPoint != -1)
        {
            healthPoint = 0;
            Die();
        }
        else
        {
            healthPoint -= damage;

            //Playanimation
            AnimationUpdate(2);
            StartCoroutine(AnimationDamageDelay());
        }
        //Update HUD
    }

    private void Die()
    {
        isDying = true;
        //Jouer animation;
        StopAllCoroutines();
        StartCoroutine(DieAnimationDelay());
    }

    private IEnumerator DieAnimationDelay()
    {
        ChangeAnimationState(state[8]);
        yield return new WaitForSeconds(dieAnimationDelay);
        //Changer de scene
    }

    //Manage Animation
    private void AnimationUpdate(int index)
    {
        if (/*lastDirection.y > 0 || lastDirection.y == 0 && */upDirection)
        {

            ChangeAnimationState(state[index]);
        }
        else
        {
            ChangeAnimationState(state[index + 1]);
        }
    }

    private IEnumerator AnimationDamageDelay()
    {
        isTakingDamage = true;
        yield return new WaitForSeconds(0.4f);
        isTakingDamage = false;
    }

    //Mod in Game
    public void GodMod() {healthPoint = -1;}
}
