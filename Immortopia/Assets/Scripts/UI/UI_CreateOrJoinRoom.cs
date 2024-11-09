using UnityEngine;
using DG.Tweening;

public class UI_CreateOrJoinRoom : Scene, IScene
{
    private GameObject backBtn;
    private GameObject serverList;
    
    private void Start()
    {
        //Scene Info
        sceneIndex = scenes.IndexOf(this);
        
        backBtn = GameObject.Find("backButton");
        serverList = GameObject.Find("Server List");
        
        camPos = new Vector3(14, .75f, -5.5f);
        camRot = new Vector3(0, -45, 0);
        
        LeaveScene();
    }

    public void LoadScene()
    {
        camMove();
        backBtn.SetActive(true);
        serverList.SetActive(true);
    }
    
    private void camMove()
    {
        cam.DOMove(camPos, camMoveSpeed);
        cam.DORotate(camRot, camRotSpeed);
    }

    public void LeaveScene()
    {
        backBtn.SetActive(false);
        serverList.SetActive(false);
    }
}
