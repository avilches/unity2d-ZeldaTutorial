using TMPro;
using UnityEngine;

public class RollingTextFade : MonoBehaviour {
    private TMP_Text tm;
    private float FadeSpeed = 3.0F;
    private float WaitTime = 3.0F;

    void Awake() {
        tm = GetComponent<TMP_Text>();
        Init();
    }

    private void OnEnable() {
        Init();
    }

    private void Init() {
        fading = true;
        currentWait = WaitTime;
        alpha = 1;
        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, alpha);
        fading = true;
    }


    private float alpha;
    private bool fading;
    private float currentWait;

    void Update() {
        if (!fading) return;
        if (currentWait > 0) {
            currentWait -= Time.deltaTime;
            return;
        }

        alpha = Mathf.Lerp(alpha, -0.1f, FadeSpeed * Time.deltaTime);
        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, alpha);
        if (alpha < 0) {
            fading = false;
        }
    }
}