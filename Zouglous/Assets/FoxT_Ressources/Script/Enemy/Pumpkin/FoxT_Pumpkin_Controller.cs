using MathExtension;
using Pathfinding;
using System.Collections;
using UnityEngine;

public class FoxT_Pumpkin_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 lastDirection;
    [SerializeField]
    private FoxT_Pumpkin_Attack pumpkinAttack;

    [SerializeField]
    private FoxT_Pumpkin_Stat stat;

    [SerializeField]
    private Transform pcTransform;

    [SerializeField]
    private LayerMask pcLayer;

    [SerializeField]
    private float speed;

    private int healthPoint;
    private int damage;
    private bool isTakingDamage;

    [SerializeField]
    private float attackWindUpDelay, endAnimation, attackCoolDownDelay;
    private bool isAttacking;
    [SerializeField]
    private float startAttackDistance;

    public bool PCdetected;

    public Transform[] wayPointsPatrol;
    private int wayPointsIndex;
    private float minDistance = 0.1f;
    //[HideInInspector]
    public bool upDirection, rightDirection;
    [SerializeField]
    private float waintingTime;
    private bool isWaiting;

    [SerializeField]
    private string[] state;
    private string currentState;
    private Animator anim;
    [SerializeField]
    private float dieAnimationDelay;
    private Seeker seeker;
    [SerializeField]
    private Respawn respawn;

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Player")
        {
            rb.isKinematic = false;
        }
	}

	private void Awake()
	{
        respawn.enemy = gameObject;
        seeker = GetComponent<Seeker>();
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
	}

	// Start is called before the first frame update
	void Start()
    {
        //Stat Attribution
        speed = stat.speed;
        healthPoint = stat.maxHealth;
		damage = stat.damage;
        //check quel ennemi
        if (pumpkinAttack != null) startAttackDistance = pumpkinAttack.attackRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacking)
        {
            GetComponent<EnemyPathAI>().enabled = false;
            return;
        }
        //Do his patrol
        if (PCdetected)
        {
            GetComponent<EnemyPathAI>().enabled = true;

            //Attack Test
            if (Vector2.Distance(transform.position, pcTransform.position) < startAttackDistance)
            {
                StartCoroutine(StartAttack());
            }
        }
        
        Patrol();
    }

	private void Patrol()
    {
        if (isWaiting) return;
        if (!Mathe.IsBetweenRange(-Vector3.Distance(this.transform.position, wayPointsPatrol[wayPointsIndex].position), Vector3.Distance(this.transform.position, wayPointsPatrol[wayPointsIndex].position), minDistance))
        {
            wayPointsIndex = (wayPointsIndex + 1) % wayPointsPatrol.Length;
            StartCoroutine(Waiting());
        }
        else
        {
            rb.velocity = new Vector2(wayPointsPatrol[wayPointsIndex].position.x - this.transform.position.x, wayPointsPatrol[wayPointsIndex].position.y - this.transform.position.y).normalized * speed * 50 * Time.deltaTime;
            if (rb.velocity != Vector2.zero) LastDirection();

        }

        if (isTakingDamage || isAttacking) return;
        if (rb.velocity == Vector2.zero)
        { }
        else AnimationUpdate(0);
    }

    private void LastDirection()
    {
        bool test = upDirection;
        Vector2 move = rb.velocity;

        if (move.x != 0) transform.localScale = new Vector3(-1 * Mathf.Sign(move.x), transform.localScale.y, transform.localScale.z);

        if (move.x > 0) rightDirection = true;
        else if (move.x < 0) rightDirection = false;
        if (move.y > 0) upDirection = true;
        else if (move.y < 0) upDirection = false;

        //if (upDirection != test) Debug.Log(move.y);
        /*if (move.x > 0 && move.x > Mathf.Abs(move.y)) lastDirection = Vector2.right;
        else if (move.x < 0 && move.x < Mathf.Abs(move.y) * -1) lastDirection = Vector2.left;
        else if (move.y > 0 && move.y > Mathf.Abs(move.x)) lastDirection = Vector2.up;
        else if (move.y < 0 && move.y < Mathf.Abs(move.x) * -1) lastDirection = Vector2.down;*/
    }

    public void Move(Vector2 direction)
    {
        rb.velocity = direction * speed * Time.fixedDeltaTime * 50;

        if (rb.velocity == Vector2.zero) { }
        else
        {
            LastDirection();
            if (isAttacking || isTakingDamage) return;
            AnimationUpdate(0);
        }
    }

    private IEnumerator Waiting()
    {
        rb.velocity = Vector2.zero;
        isWaiting = true;
        yield return new WaitForSeconds(waintingTime);
        isWaiting = false;
    }

    public void TakeDamage(int damage)
    {
        if (healthPoint - damage <= 0)
        {
            healthPoint = 0;
            //Update HUD
            StartCoroutine(Die());
        }
        else
        {
            healthPoint -= damage;

            //Play animation
            AnimationUpdate(2);
            StartCoroutine(AnimationDamageDelay());
        }
    }

    private IEnumerator StartAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            AnimationUpdate(4);
            rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(attackWindUpDelay);

            //Check qui est l'ennemi
            if (pumpkinAttack != null) pumpkinAttack.Attack(damage);
            // else if () else

            yield return new WaitForSeconds(endAnimation);
            Debug.Log("Passe");
            AnimationUpdate(0 /*A changer avec l'IDLE*/);

            yield return new WaitForSeconds(attackCoolDownDelay);
            isAttacking = false;
        }
    }

    //Animator Manager
    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        anim.Play(currentState);
    }

    private IEnumerator AnimationDamageDelay()
    {
        isTakingDamage = true;
        yield return new WaitForSeconds(0.4f);
        isTakingDamage = false;
    }

    private IEnumerator Die()
    {
        AnimationUpdate(6);
        rb.velocity = Vector2.zero;
        //Jouer Animation
        yield return new WaitForSeconds(dieAnimationDelay);
        //loot
        Respawn();
        respawn.RespawnEvent();
        gameObject.SetActive(false);
    }

    //Manage Animation
    private void AnimationUpdate(int index)
    {
        if (upDirection)
        {

            ChangeAnimationState(state[index]);
        }
        else
        {
            ChangeAnimationState(state[index + 1]);
        }
    }

    public void Respawn()
    {
        StopAllCoroutines();
        //remettre les pendules à l'heure
        GetComponent<EnemyPathAI>().enabled = false;

        speed = stat.speed;
        damage = stat.damage;
        healthPoint = stat.maxHealth;
        PCdetected = false;

        lastDirection = Vector2.zero;
        isWaiting = false;
        upDirection = false;
        rightDirection = false;
        isAttacking = false;
        isTakingDamage = false;

        transform.position = wayPointsPatrol[0].position;
    }
}
