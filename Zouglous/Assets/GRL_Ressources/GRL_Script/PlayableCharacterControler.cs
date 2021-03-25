using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterControler : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rigidBody;
    [SerializeField]
    Vector2 direction;
    [SerializeField]
    PlayableCharacterStat stat;

    float speed;

    private void Awake()
    {
        speed = stat.speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputHandler()
    {
        direction.y = Input.GetAxis("Vertical");
        direction.x = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        rigidBody.velocity = direction.normalized * Time.fixedDeltaTime * speed * 50f;
    }
}
