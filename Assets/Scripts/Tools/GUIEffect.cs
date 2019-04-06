using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GUIEffect<T> : Singleton<T> where T :MonoBehaviour {

    enum State { disabled, starting, ending }
    
    private Vector2 resolution;
    private State state;
    
    public void Awake() {
        resolution = new Vector2(Screen.width, Screen.height);
        state = State.disabled;
        Configure();
    }

    protected abstract void Configure();
    protected abstract void DoEffect();

    public bool IsStarting() { return state == State.starting; }
    public bool IisEnding() { return state == State.ending; }
    public bool IsDisabled() { return state == State.disabled; }


    public void OnGUI() {
        if (IsDisabled()) return;
        if (resolution.x != Screen.width || resolution.y != Screen.height) {
            Configure();
        }
        DoEffect();
    }

    // Método para activar la transición de entrada
    public void Start() {
        state = State.starting;
    }

    public void End() {
        state = State.ending;
    }

    public void Disable() {
        state = State.disabled;
    }

}