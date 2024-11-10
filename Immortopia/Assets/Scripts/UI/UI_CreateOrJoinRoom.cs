using UnityEngine;
using DG.Tweening;

public class UI_CreateOrJoinRoom : Scene
{
    private GameObject serverList;
    private Transform roomTemplate;
    
    protected override void Start()
    {
        serverList = content.transform.Find("Server List").gameObject;
        roomTemplate = serverList.transform.Find("Scroll View/Room Template");
        
        camPos = new Vector3(14, .75f, -5.5f);
        camRot = new Vector3(0, -45, 0);
        
        base.Start();
    }

    public override void LoadScene()
    {
        camMove();
        base.LoadScene();
        LoadServerList();
    }
    
    private void camMove()
    {
        cam.DOMove(camPos, camMoveSpeed);
        cam.DORotate(camRot, camRotSpeed);
    }
    
    public void LoadServerList()
    {
        
    }

    public override void LeaveScene()
    {
        base.LeaveScene();
    }
}
