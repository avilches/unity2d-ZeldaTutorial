public class ToolBox : MonoSingleton<ToolBox> {
    public Fader fader;
    public VCameras vCameras;
    
    public void Awake() {
        fader = gameObject.AddComponent<Fader>();
        vCameras = new VCameras();
        vCameras.Init();
    }

}