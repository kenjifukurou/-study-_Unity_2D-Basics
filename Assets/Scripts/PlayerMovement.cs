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

    [SerializeField]
    private bool canShoot;
    [SerializeField]
    private float shootCoolDown;

    public Transform leftBulletSpawn;
    public Transform rightBulletSpawn;
    public GameObject bulletFireBall;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
        shootCoolDown = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        //MovementByAxis();
        AccelerateFalling();
        Shooting();

        shootCoolDown -= Time.deltaTime;
        if (shootCoolDown < 0) {
            canShoot = true;
            animCharacter.SetBool("isShoot", false);
        }
    }

    private void Shooting() {
        if (Input.GetMouseButtonDown(0)) {

            if (canShoot) {
                //shoot!
                if (srCharacter.flipX) {
                    //shoot right
                    GameObject rightFireBall = Instantiate(bulletFireBall, rightBulletSpawn.transform.position, Quaternion.identity);
                    //rightFireBall.GetComponent<Rigidbody2D>().velocity = Vector2.right * 1000;
                    rightFireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(50, 10);
                } else {
                    //shoot left
                    GameObject leftFireBall = Instantiate(bulletFireBall, leftBulletSpawn.transform.position, Quaternion.identity);
                    //leftFireBall.GetComponent<Rigidbody2D>().velocity = Vector2.left * 1000;
                    leftFireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(-50, 10);

                }

                animCharacter.SetBool("isShoot", true);
                canShoot = false;
                shootCoolDown = 0.2f;

            }
        }
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
            animCharacter.SetBool("isShoot", false);

        } else if (Input.GetKey(KeyCode.A)) {
            //move left
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
            srCharacter.flipX = false;
            animCharacter.SetBool("isRun", true);
            animCharacter.SetBool("isShoot", false);

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
