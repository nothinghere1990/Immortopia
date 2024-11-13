using UnityEngine;
using DG.Tweening;
using Fusion;
using TMPro;
using UnityEngine.UI;

public class UI_CreateOrJoinSession : Scene
{
    private TMP_Text statusText;
    
    private Button enterCreateSessionBtn;
    protected Button JoinBtn;
    
    private Transform sessionList;
    private GameObject SessionTemplatePrefab;
    
    private SessionTemplate clickedSessionTemplate;
    
    protected override void Start()
    {
        statusText = content.transform.Find("Status Text").GetComponent<TMP_Text>();
        
        enterCreateSessionBtn = content.transform.Find("Enter Create Session").GetComponent<Button>();
        JoinBtn = content.transform.Find("Join Session").GetComponent<Button>();
        
        sessionList = content.transform.Find("Session List/Scroll Area");
        SessionTemplatePrefab = GameAssets.i.sessionTemplatePrefab;
        
        camPos = new Vector3(14, .75f, -5.5f);
        camRot = new Vector3(0, -45, 0);

        enterCreateSessionBtn.onClick.AddListener(() => CustomSceneManager.Instance.LoadScene(sceneIndex + 1));
        JoinBtn.onClick.AddListener(JoinSession);

        FusionConnection.Instance.OnCreateSession += RPC_AddToList;
        FusionConnection.Instance.OnJoinSession += RPC_UpdateList;
        
        base.Start();
    }

    public override void LoadScene()
    {
        camMove();
        base.LoadScene();
        RefreshList();
    }

    private void camMove()
    {
        cam.DOMove(camPos, camMoveSpeed);
        cam.DORotate(camRot, camRotSpeed);
    }

    public void RefreshList()
    {
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_AddToList()
    {
        SessionTemplate sessionTemplate = Instantiate(SessionTemplatePrefab, sessionList.transform).GetComponent<SessionTemplate>();
        sessionTemplate.SetupSessionTemplate();
    }

    protected void GetClickedSession(SessionTemplate sessionTemplate)
    {
        clickedSessionTemplate = sessionTemplate;
        ActiveJoinBtn();
    }
    
    protected void ActiveJoinBtn()
    {
        if (clickedSessionTemplate.sessionInfo.PlayerCount >= clickedSessionTemplate.sessionInfo.MaxPlayers)
            JoinBtn.interactable = false;
        else
            JoinBtn.interactable = true;
    }
    
    public void JoinSession()
    {
        FusionConnection.Instance.ConnectToSession(clickedSessionTemplate.sessionInfo.Name);
    }
    
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_UpdateList()
    {
        clickedSessionTemplate.playerCountText.text = $"{clickedSessionTemplate.sessionInfo.PlayerCount.ToString()} / {clickedSessionTemplate.sessionInfo.MaxPlayers.ToString()}";
    }

    public void ClearList()
    {
        foreach (Transform sessionTemplate in sessionList)
        {
            Destroy(sessionTemplate.gameObject);
        }
        
        statusText.gameObject.SetActive(false);
    }
    
    public void OnNoSessionFound()
    {
        statusText.text = "No game session found";
        statusText.gameObject.SetActive(true);
    }
    
    public void OnLookingForGameSessions()
    {
        statusText.text = "On looking for game sessions";
        statusText.gameObject.SetActive(true);
    }

    public override void LeaveScene()
    {
        base.LeaveScene();
    }
}