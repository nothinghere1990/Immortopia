using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI_MainMenu : Scene
{
    private TMP_Text startBtnText;
    private Sequence startBtnSequence;
    
    protected override void Start()
    {
        camPos = new Vector3(-7.25f, 8, -7.25f);
        camRot = new Vector3(15, 45, 0);
        
        startBtn.onClick.AddListener(() => FusionConnection.Instance.ConnectToLobby("Lobby"));
        quitBtn.onClick.AddListener(QuitGame);
        
        FusionConnection.Instance.onConnectToLobby += () => CustomSceneManager.Instance.LoadScene(sceneIndex + 1);
        
        base.Start();
    }

    public override void LoadScene()
    {
        base.LoadScene();
        CamMove();
        StartGameTextAni(true);
    }

    private void CamMove()
    {
        cam.DOMove(camPos, camMoveSpeed);
        cam.DORotate(camRot, camRotSpeed);
    }
    
    private void StartGameTextAni(bool startLoop)
    {
        if (startLoop)
        {
            startBtnText = startBtn.GetComponentInChildren<TMP_Text>();
            startBtnSequence = DOTween.Sequence();
            
            startBtnSequence.SetLoops(-1, LoopType.Restart);
            startBtnSequence.Append(startBtnText.DOFade(0, .7f));
            startBtnSequence.Append(startBtnText.DOFade(1, .7f));
        }
        else
        {
            startBtnSequence.Rewind();
            startBtnSequence.Kill();
        }
    }
    
    public override void LeaveScene()
    {
        StartGameTextAni(false);
        base.LeaveScene();
    }
    
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
