using UnityEngine;
using DG.Tweening;
using Fusion;
using TMPro;
using UnityEngine.UI;

public class UI_CreateOrJoinSession : Scene
{
    private TMP_Text statusText;
    
    private Button enterCreateSessionBtn;
    private Button joinBtn;
    private Button refreshBtn;
    
    private Transform sessionScroll;
    private GameObject SessionTemplatePrefab;
    
    private SessionTemplate clickedSessionTemplate;
    
    protected override void Start()
    {
        statusText = content.transform.Find("Status Text").GetComponent<TMP_Text>();
        
        enterCreateSessionBtn = content.transform.Find("Enter Create Session").GetComponent<Button>();
        joinBtn = content.transform.Find("Join Session").GetComponent<Button>();
        refreshBtn = content.transform.Find("Refresh Session").GetComponent<Button>();
        
        sessionScroll = content.transform.Find("Session List/Scroll Area");
        SessionTemplatePrefab = GameAssets.i.sessionTemplatePrefab;
        
        camPos = new Vector3(14, .75f, -5.5f);
        camRot = new Vector3(0, -45, 0);

        backBtn.onClick.AddListener(CustomSceneManager.Instance.LoadLastScene);
        enterCreateSessionBtn.onClick.AddListener(() => CustomSceneManager.Instance.LoadScene(sceneIndex + 1));
        joinBtn.onClick.AddListener(JoinSession);
        refreshBtn.onClick.AddListener(RefreshList);

        FusionConnection.Instance.onSessionListUpdated += RefreshList;
        
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

    private void RefreshList()
    {
        Debug.Log("Refresh List");
        OnLookingForGameSessions();
        ClearList();
        AddToList();
        if (FusionConnection.Instance.sessionList.Count <= 0) OnNoSessionFound();
        else statusText.gameObject.SetActive(false);
    }
    
    private void ClearList()
    {
        if (sessionScroll.childCount <= 0) return;
        
        foreach (Transform sessionTemplate in sessionScroll)
        {
            Destroy(sessionTemplate.gameObject);
        }
    }
    
    private void AddToList()
    {
        if (FusionConnection.Instance.sessionList == null) return;
        
        foreach (SessionInfo sessionInfo in FusionConnection.Instance.sessionList)
        {
            SessionTemplate sessionTemplate = Instantiate(SessionTemplatePrefab, sessionScroll.transform).GetComponent<SessionTemplate>();
            sessionTemplate.SetupSessionTemplate(sessionInfo);
        }
    }

    public void GetClickedSession(SessionTemplate sessionTemplate)
    {
        clickedSessionTemplate = sessionTemplate;
        ActivejoinBtn();
    }
    
    private void ActivejoinBtn()
    {
        if (clickedSessionTemplate.sessionInfo.PlayerCount >= clickedSessionTemplate.sessionInfo.MaxPlayers)
            joinBtn.interactable = false;
        else
            joinBtn.interactable = true;
    }
    
    private void JoinSession()
    {
        FusionConnection.Instance.ConnectToSession(clickedSessionTemplate.sessionInfo.Name);
    }
    
    public void OnNoSessionFound()
    {
        statusText.text = "No session found";
        statusText.gameObject.SetActive(true);
    }
    
    public void OnLookingForGameSessions()
    {
        statusText.text = "Looking for sessions";
        statusText.gameObject.SetActive(true);
    }

    public override void LeaveScene()
    {
        base.LeaveScene();
    }
}