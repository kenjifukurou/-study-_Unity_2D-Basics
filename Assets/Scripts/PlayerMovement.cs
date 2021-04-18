using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public SpriteRenderer srCharacter;
    public Animator animCharacter;

    public Rigidbody2D rigCharacter;
    public float jumpForce;

    [SerializeField]
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        //isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        //MovementByAxis();
        AccelerateFalling();

    }

    private void AccelerateFalling() {
        // while falling
        if (rigCharacter.velocity.y < 0) {
            //multiply the falling speed with gravity
            rigCharacter.velocity += Vector2.up * Physics2D.gravity * 10 * Time.deltaTime;
        }
    }

    private void Movement() {

        if (Input.GetKey(KeyCode.D)) {
            //move right
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
            srCharacter.flipX = true;
            animCharacter.SetBool("isRun", true);

        } else if (Input.GetKey(KeyCode.A)) {
            //move left
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            srCharacter.flipX = false;
            animCharacter.SetBool("isRun", true);

        } else if (Input.GetKeyDown(KeyCode.Space)) {
            //jump
            if (isGrounded) {
                rigCharacter.AddForce(Vector2.up * jumpForce);
                isGrounded = false;
            }
            
        } else {
            animCharacter.SetBool("isRun", false);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
        }
    }

    private void MovementByAxis() {

        float movementH = speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.Translate(movementH, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGrounded) {
                rigCharacter.AddForce(Vector2.up * jumpForce);
                isGrounded = false;
            }
        }

    }
}
