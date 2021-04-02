using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathExtension;

public class FoxT_Pumpkin_Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 lastDirection;

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
    private bool PCdetected;

    [SerializeField]
    private Transform[] wayPointsPatrol;
    private int wayPointsIndex;
    private float minDistance = 0.1f;
    private bool upDirection, rightDirection;
    [SerializeField]
    private float waintingTime;
    private bool isWaiting;

    [SerializeField]
    private string[] state;
    private string currentState;
    private Animator anim;
    [SerializeField]
    private float dieAnimationDelay;

	private void Awake()
	{
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
	}

	// Start is called before the first frame update
	void Start()
    {
        healthPoint = stat.maxHealth;
		damage = stat.damage;
        StartCoroutine(StartAttack());
    }

    // Update is called once per frame
    void Update()
    {
        //Do his patrol
        if (!PCdetected)
        {
            Patrol();
        }
        //Attack Player
        else
        {
            StopAllCoroutines();
            //suit Pathfounding
            rb.velocity = Vector3.MoveTowards(this.transform.position, pcTransform.position, 10) * speed * Time.deltaTime;
        }
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
            rb.velocity = new Vector2(wayPointsPatrol[wayPointsIndex].position.x - this.transform.position.x, wayPointsPatrol[wayPointsIndex].position.y - this.transform.position.y).normalized * speed * Time.deltaTime;
            if (rb.velocity != Vector2.zero) LastDirection();

        }

        if (isTakingDamage) return;
        if (rb.velocity == Vector2.zero) Debug.Log("0");
        else AnimationUpdate(0);
    }

    private void LastDirection()
    {
        Vector2 move = rb.velocity;

        if (move.x != 0) transform.localScale = new Vector3(-1 * Mathf.Sign(move.x), transform.localScale.y, transform.localScale.z);

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
        /*if (move.x > 0 && move.x > Mathf.Abs(move.y)) lastDirection = Vector2.right;
        else if (move.x < 0 && move.x < Mathf.Abs(move.y) * -1) lastDirection = Vector2.left;
        else if (move.y > 0 && move.y > Mathf.Abs(move.x)) lastDirection = Vector2.up;
        else if (move.y < 0 && move.y < Mathf.Abs(move.x) * -1) lastDirection = Vector2.down;*/
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
        yield return new WaitForSeconds(5);
        Attack();
        StartCoroutine(StartAttack());
    }

    private void Attack()
    {
        //jouer animation
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position + Mathe.Vector2To3(lastDirection), Vector2.one / 2, 45f, lastDirection, pcLayer);

        if (hit.collider != null)
        {
            //hit.collider.GetComponent<FoxT_Controller>().TakeDamage(damage);
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
        StopAllCoroutines();
        rb.velocity = Vector2.zero;
        //Jouer Animation
        yield return new WaitForSeconds(dieAnimationDelay);
        Destroy(this.gameObject);
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
}
