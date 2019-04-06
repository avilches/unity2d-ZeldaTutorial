using UnityEngine;

public abstract class GUIEffect : MonoBehaviour {

    enum State { stopped, starting, ending }
    
    private Vector2 resolution;
    private State state;
    
    public void Awake() {
        resolution = new Vector2(Screen.width, Screen.height);
        state = State.stopped;
        Configure();
    }

    protected abstract void Configure();
    protected abstract void DoEffect();

    public bool IsStarting() { return state == State.starting; }
    public bool IisEnding() { return state == State.ending; }
    public bool IsStopped() { return state == State.stopped; }


    public void OnGUI() {
        if (IsStopped()) return;
        if (resolution.x != Screen.width || resolution.y != Screen.height) {
            Configure();
        }
        DoEffect();
    }

    // Método para activar la transición de entrada
    public void StartEffect() {
        state = State.starting;
    }

    public void EndEffect() {
        state = State.ending;
    }

    public void StopEffect() {
        state = State.stopped;
    }

}