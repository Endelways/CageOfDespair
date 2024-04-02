using Core;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform looker;
    public Teleportation Teleportation;
    public Portal ConnectedPortal;
    public Camera PortalView;
    public MeshRenderer Frame;
    public RenderTexture Texture;
    public bool Logging = false;
    private void Start()
    { 
        Texture = new RenderTexture(Screen.width, Screen.height, 24); 
        ConnectedPortal.PortalView.targetTexture = Texture;
        Frame.sharedMaterial.mainTexture = ConnectedPortal.PortalView.targetTexture;
    }

    private void Render()
    {
        if (!Frame.isVisible)
        {
            return;
        }
        ConnectedPortal.PortalView.transform.SetMirrorPosition(looker, Frame.transform, ConnectedPortal.Frame.transform,true);

        var rotationDifference =
            transform.rotation * Quaternion.Inverse(ConnectedPortal.transform.rotation.Inverse());
        ConnectedPortal.PortalView.transform.rotation = rotationDifference * looker.rotation;
    }
    void Update()
    {
       if (Logging)
       { 
           Debug.Log(Frame.transform.position);
           Debug.Log(looker.position);
           Debug.Log(looker.RelativePosition(Frame.transform));
       }
       Render();
    }
    
}