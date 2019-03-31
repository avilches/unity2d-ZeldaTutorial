using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update

    private Animator animator;
    private Rigidbody2D rb;

    void Start() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    public float speed = 2F;
    private float lastDx = 2F;
    private float lastDy = 2F;


    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("a");
    }

    private void OnCollisionExit2D(Collision2D other) {
        Debug.Log("a");
    }

    // Update is called once per frame
    void Update() {
        float dx = 0;
        float dy = 0;
        Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (IsPressedRight() ^ IsPressedLeft())
            if (IsPressedRight()) {
                dx = speed * Time.deltaTime;
                animator.enabled = true;
                animator.Play("Player_R");
            } else if (IsPressedLeft()) {
                dx = -speed * Time.deltaTime;
                animator.enabled = true;
                animator.Play("Player_L");
            }

        if (IsPressedDown() ^ IsPressedUp())
            if (IsPressedDown()) {
                dy = -speed * Time.deltaTime;
                if (dx == 0) {
                    animator.enabled = true;
                    animator.Play("Player_D");
                }
            } else if (IsPressedUp()) {
                dy = speed * Time.deltaTime;
                if (dx == 0) {
                    animator.enabled = true;
                    animator.Play("Player_U");
                }
            }


        if (dx == 0F && dy == 0F) {
            // Idle
            if (lastDx != 0 || lastDy != 0) {
                animator.enabled = false;
            }

//
//            if (lastDx > 0F) {
//                animator.Play("Player_RIdle");
//            } else if (lastDx < 0F) {
//                animator.Play("Player_LIdle");
//            } else if (lastDy > 0F) {
//                animator.Play("Player_UIdle");
//            } else if (lastDy < 0F) {
//                animator.Play("Player_DIdle");
//            }
        }

        animator.SetFloat("dx", dx);
        animator.SetFloat("dy", dy);
        transform.Translate(new Vector2(dx, dy));

        lastDx = dx;
        lastDy = dy;
    }

    private bool IsPressedDown() {
        return Input.GetKey(KeyCode.DownArrow);
    }

    private bool IsPressedLeft() {
        return Input.GetKey(KeyCode.LeftArrow);
    }

    private bool IsPressedRight() {
        return Input.GetKey(KeyCode.RightArrow);
    }

    private bool IsPressedUp() {
        return Input.GetKey(KeyCode.UpArrow);
    }
}