public class ToolBox : MonoSingleton<ToolBox> {
    public Fader fader { get; private set; }
    public VCameras vCameras { get; private set; }

    public void Awake() {
        fader = gameObject.AddComponent<Fader>();
        vCameras = new VCameras();
        vCameras.Init();
    }
}