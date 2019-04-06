using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    // Start is called before the first frame update

    private Animator animator;
    private Rigidbody2D rb;
    private GameObject[] cameraList;
    private Fader _fader;

    const string TAG_cinemachine = "cinemachine";

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("running", false);
        animator.SetFloat("dy", -1F);
        rb = GetComponent<Rigidbody2D>();
        _fader = Fader.Instance;

        cameraList = GameObject.FindGameObjectsWithTag(TAG_cinemachine);
        for (int i = 0; i < cameraList.Length; i++) {
            Debug.Log(cameraList[i].transform.name);
        }
    }

    public void EnableVCam(GameObject vcam) {
        Assert.IsNotNull(vcam);
        for (int i = 0; i < cameraList.Length; i++) {
            cameraList[i].SetActive(cameraList[i].gameObject == vcam);
        }
    }

    public float speed = 2F;
    private float lastDx = 2F;
    private float lastDy = -2F;
    public bool gaming = true;


    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Player. CollisionEnter to " + other.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D other) {
        Debug.Log("Player. CollisionExit to " + other.gameObject.name);
    }

    private IEnumerator<YieldInstruction> TeletransportTo(Transform wrap, Transform target) {
        gaming = false;
        animator.enabled = false;
        _fader.FadeIn(1.5F);
        yield return new WaitForSeconds(0.7F); 
        EnableVCam(FindChildrenByTag(wrap.parent, TAG_cinemachine));
        transform.position = target.position;
        _fader.FadeOut(0.4f);
        yield return new WaitForSeconds(0.3F);
        animator.enabled = true;
        gaming = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Player. TriggerEnter to " + other.gameObject.name);
        if (other.gameObject.CompareTag("wrap")) {
            var wrap = other.GetComponent<WrapTarget>().target;
            StartCoroutine(TeletransportTo(wrap, other.GetComponent<WrapTarget>().target));
        }
    }

    // Update is called once per frame
    void Update() {
        if (!gaming) return;
        float dx = 0;
        float dy = 0;
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (IsPressedRight() ^ IsPressedLeft()) {
            dx = speed * Time.deltaTime;
            if (IsPressedLeft()) {
                dx = -dx;
            }
        }

        if (IsPressedDown() ^ IsPressedUp()) {
            dy = speed * Time.deltaTime;
            if (IsPressedDown()) {
                dy = -dy;
            }
        }

        if (dx != 0 || dy != 0) {
            animator.SetBool("running", true);
            animator.SetFloat("dx", dx);
            animator.SetFloat("dy", dy);

            rb.MovePosition((Vector2) transform.position + new Vector2(dx, dy));
        } else {
            animator.SetBool("running", false);
        }

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

    public static GameObject FindChildrenByTag(GameObject parent, string tag) {
        return FindChildrenByTag(parent.transform, tag);
    }

    public static GameObject FindChildrenByTag(Transform parent, string tag) {
        foreach (Transform t in parent) {
            if (t.CompareTag(tag)) return t.gameObject;
        }
        return null;
    }

    public static List<Transform> listTransformChildren(GameObject obj) {
        var path = new List<Transform>();
        foreach (Transform t in obj.transform) {
            path.Add(t);
        }

        return path;
    }
    
}