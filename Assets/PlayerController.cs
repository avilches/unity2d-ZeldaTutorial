using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    private Rigidbody2D rb;
    private ToolBox toolBox;

    public float speed = 2F;
    public bool gaming = true;

    void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        toolBox = ToolBox.Instance;
        Debug.Log("AWAKE");
    }

    private void Start() {
        animator.SetBool("running", false);
        animator.SetFloat("dy", -1F);
        toolBox.vCameras.EnableVCam(GameObject.Find("CM vc1"));
        Debug.Log("START");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Player. CollisionEnter to " + other.gameObject.name+ " "+other.contactCount);
    }

    private void OnCollisionExit2D(Collision2D other) {
        Debug.Log("Player. CollisionExit to " + other.gameObject.name);
    }

    private IEnumerator<YieldInstruction> TeletransportTo(Transform wrap, Transform target) {
        gaming = false;
        animator.enabled = false;
        toolBox.fader.FadeIn(1.5F);
        yield return new WaitForSeconds(0.7F); 
        toolBox.vCameras.EnableVCam(FindChildrenByTag(wrap.parent, VCameras.TAG));
        
        transform.position = target.position;
        toolBox.fader.FadeOut(0.4f);
        yield return new WaitForSeconds(0.3F);
        animator.enabled = true;
        gaming = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Player. OnTriggerEnter2D to " + other.name);
        if (other.gameObject.CompareTag("wrap")) {
            var wrap = other.gameObject.GetComponent<WrapTarget>().target;
//            CameraShake.ShakeMainCamera(0.07f, 0.2f);
            StartCoroutine(TeletransportTo(wrap, other.gameObject.GetComponent<WrapTarget>().target));
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