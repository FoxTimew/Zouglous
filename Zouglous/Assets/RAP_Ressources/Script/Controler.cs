using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{
    public float vitesse = 5f;

    float dashingTimeElapsed;

    public bool isDashing, isAttacking;

    public AnimationCurve dashCurve = AnimationCurve.Constant(0f, 0.25f, 1f);

    Vector2 moveInput;

    Vector3 movement, currentDirection;

    Rigidbody2D pcRB;

    public GameObject InventoryUi;

   public  bool On;
   

    void Start()
    {
        pcRB = this.GetComponent<Rigidbody2D>();

        On = false;
    }

    void Update()
    {
        InputHandler();

        
    }

    private void FixedUpdate()
    {
        AttackPointPosition();
        DashUpdate();
        Move();
    }

    void InputHandler()
    {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        movement = Vector3.Normalize(moveInput) * vitesse * Time.deltaTime;

      //  if (Input.GetButtonDown("Dash")) DashTest();

    //    if (Input.GetButtonDown("Attack")) AttackTest();

        if (Input.GetKeyDown(KeyCode.I) && (On==false))
        {
            StartCoroutine(Time1());
        }

        if (Input.GetKeyDown(KeyCode.I) && (On == true))
        {
            StartCoroutine(Time2());
            On = false;
            InventoryUi.SetActive(false);
        }
       IEnumerator Time1 ()
        {
            InventoryUi.SetActive(true);
            yield return new WaitForSeconds (0.1f);
            On = true;
            InputHandler();
        }

        IEnumerator Time2 ()
        {
            InventoryUi.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            On = false;
            InputHandler();
        }
    }

    void AttackPointPosition()
    {
        Vector3 displacement;
        displacement = currentDirection;
        //attackPoint.localPosition = Vector3.Normalize(displacement * 10);
    }

    void DashTest()
    {
        if (isDashing) return;
        DashStart();
    }

    void DashStart()
    {
        dashingTimeElapsed = 0f;
        isDashing = true;
    }

    void DashUpdate()
    {
        if (!isDashing) return;
        if (movement == new Vector3(0f, 0f, 0f)) movement = currentDirection;

        movement = Vector3.Normalize(movement * 10) * dashCurve.Evaluate(dashingTimeElapsed) * Time.deltaTime;
        dashingTimeElapsed += Time.deltaTime;

        if (dashingTimeElapsed > dashCurve.keys[dashCurve.keys.Length - 1].time)
        {
            isDashing = false;
        }
    }

    void AttackTest()
    {
        if (isAttacking) return;
        AttackStart();
    }

    void AttackStart()
    {
        isAttacking = true;
    }

    void Move()
    {
        if (movement != new Vector3(0f, 0f, 0f)) currentDirection = Vector3.Normalize(movement * 10);
        pcRB.velocity = movement;
    }

    void AnimationControler()
    { 
        //Play animation;
    }
    
   
}
