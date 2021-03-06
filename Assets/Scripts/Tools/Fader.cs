﻿using UnityEngine;
/**
 * 1) Add the Fader component to any object and call to o.GetComponent<Fader>.FadeIn()/FadeOut()
 * 2) You can add to the ToolBox singleton with fader = gameObject.AddComponent<Fader>() and use the
 * fader reference to call to the FadeIn()/FadeOut()
 */
public class Fader : GUIEffect {
    private Texture2D texture;
    private Rect rect;
    private float alpha;
    private float fadeTime;

    
    protected override void Configure() {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.black);
        texture.Apply();
        rect = new Rect(0, 0, Screen.width, Screen.height);
    }

    protected override void DoEffect() {
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.DrawTexture(rect, texture);

        if (IsStarting()) {
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        } else {
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);
            if (alpha < 0) {
                StopEffect();
            }
        }
    }

    // Método para activar la transición de entrada
    public void FadeIn(float fadeTime = 1f) {
        this.fadeTime = fadeTime;
        alpha = 0F;
        StartEffect();
    }

    // Método para activar la transición de salida
    public void FadeOut(float fadeTime = 1f) {
        this.fadeTime = fadeTime;
        alpha = 1F;
        EndEffect();
    }
}