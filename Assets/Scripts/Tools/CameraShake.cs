using UnityEngine;
using UnityEngine.Assertions;

public class CameraShake : MonoBehaviour {
    private Transform camTransform;

    private float pendingDuration;
    private float amount;
    private float decreaseFactor;
    private Vector3 originalPos;
    private bool shaking = false;

    void Awake() {
        if (camTransform == null) {
            camTransform = GetComponent<Transform>();
        }
    }

    void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    public void Shake(float amount, float duration, float decreaseFactor = 1.0F) {
        originalPos = camTransform.localPosition;

        pendingDuration = duration;
        this.amount = amount;
        this.decreaseFactor = decreaseFactor;
        shaking = true;
    }

    public void Stop() {
        camTransform.localPosition = originalPos;
        shaking = false;
    }

    void Update() {
        if (shaking) {
            if (pendingDuration > 0) {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * amount;
                pendingDuration -= Time.deltaTime * decreaseFactor;
            } else {
                Stop();
            }
        }
    }

    public static void ShakeMainCamera(float amount, float duration, float decreaseFactor = 1.0F) {
        ShakeCamera(Camera.main, amount, duration);
    }

    public static void ShakeCamera(Camera camera, float amount, float duration, float decreaseFactor = 1.0F) {
        var cameraShake = camera.GetComponent<CameraShake>();
        Assert.IsNotNull(cameraShake, "Camera " + camera.name + " doesn't have CameraShake component");
        cameraShake.Shake(amount, duration, decreaseFactor);
    }
}