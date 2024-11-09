using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI_MainMenu : Scene, IScene
{
    private GameObject startBtn;
    private GameObject quitBtn;
    
    private void Start()
    {
        //Scene Info
        sceneIndex = scenes.IndexOf(this);
        
        startBtn = transform.Find("startButton").gameObject;
        quitBtn = transform.Find("quitButton").gameObject;

        camPos = new Vector3(-7.25f, 8, -7.25f);
        camRot = new Vector3(15, 45, 0);
        
        LoadScene();
    }

    public void LoadScene()
    {
        CamMove();
        ActiveMainMenuBtns();
        StartGameTextAni();
    }

    private void CamMove()
    {
        cam.DOMove(camPos, camMoveSpeed);
        cam.DORotate(camRot, camRotSpeed);
    }
    
    private void ActiveMainMenuBtns()
    {
        startBtn.SetActive(true);
        quitBtn.SetActive(true);
    }
    
    private void StartGameTextAni()
    {
        TMP_Text startBtnText = startBtn.GetComponentInChildren<TMP_Text>();
        var startBtnSequence = DOTween.Sequence();
        startBtnSequence.Append(startBtnText.DOFade(0, .7f));
        startBtnSequence.Append(startBtnText.DOFade(1, .7f)).OnComplete(StartGameTextAni);
    }
    
    public void LeaveScene()
    {
        startBtn.SetActive(false);
        quitBtn.SetActive(false);
    }
    
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
