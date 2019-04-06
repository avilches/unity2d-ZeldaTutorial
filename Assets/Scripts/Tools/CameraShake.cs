using UnityEngine;

public class CameraShake : MonoBehaviour {
    private Transform camTransform;

    public float shakeDuration = 0f;
    public float shakeAmount = 0.07f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    private bool shaking = false;

    void Awake() {
        if (camTransform == null) {
            camTransform = GetComponent<Transform>();
        }
    }

    void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    public void Shake(float shakeAmount, float shakeDuration) {
        this.shakeDuration = shakeDuration;
        this.shakeAmount = shakeAmount;
        originalPos = camTransform.localPosition;
        shaking = true;
    }

    public void Stop() {
        camTransform.localPosition = originalPos;
        shaking = false;
    }

    void Update() {
        if (shaking) {
            if (shakeDuration > 0) {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime * decreaseFactor;
            } else {
                Stop();
            }
        }
    }
}