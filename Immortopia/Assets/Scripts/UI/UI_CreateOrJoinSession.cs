using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fusion;
using TMPro;
using UnityEngine.UI;

public class UI_CreateOrJoinSession : MyScene
{
    private TMP_Text playerNameText;
    
    private TMP_Text statusText;
    
    private Button backBtn, enterCreateSessionBtn, joinBtn, refreshBtn;
    
    private Transform sessionScroll;
    private GameObject SessionTemplatePrefab;
    
    
    private List<SessionInfo> sessionList;
    private SessionTemplate clickedSessionTemplate;
    
    private ProgressBar loadingSessionBar;

    protected override void Awake()
    {
        base.Awake();
        
        playerNameText = content.Find("Player Name").GetComponent<TMP_Text>();
        
        statusText = content.Find("Status Text").GetComponent<TMP_Text>();
        
        backBtn = content.Find("Back Button").GetComponent<Button>();
        enterCreateSessionBtn = content.Find("Enter Create Session").GetComponent<Button>();
        joinBtn = content.Find("Join Session").GetComponent<Button>();
        refreshBtn = content.Find("Refresh Session").GetComponent<Button>();
        loadingSessionBar = content.Find("Loading Bar").GetComponent<ProgressBar>();
        
        sessionScroll = content.Find("Session List/Scroll Area");
        SessionTemplatePrefab = GameAssets.i.sessionTemplatePrefab;
        
        camPos = new Vector3(14, .75f, -5.5f);
        camRot = new Vector3(0, -45, 0);
    }
    
    private void Start()
    {
        backBtn.onClick.AddListener(FusionSceneManager.Instance.LoadLastSubScene);
        enterCreateSessionBtn.onClick.AddListener(() => FusionSceneManager.Instance.LoadSubScene(subSceneIndex + 1));
        joinBtn.onClick.AddListener(JoinSession);
        refreshBtn.onClick.AddListener(() => RefreshList(sessionList));

        FusionSceneManager.Instance.onSessionListUpdated += RefreshList;
    }

    public override void LoadSubScene()
    {
        camMove();
        base.LoadSubScene();
        
        //Set player name.
        if (string.IsNullOrWhiteSpace(UI_MainMenu.PlayerNameInput.text))
            playerNameText.text = "Guest";
        else
            playerNameText.text = UI_MainMenu.PlayerNameInput.text;
        
        loadingSessionBar.gameObject.SetActive(false);
        RefreshList(sessionList);
    }

    private void camMove()
    {
        cam.DOMove(camPos, camMoveSpeed);
        cam.DORotate(camRot, camRotSpeed);
    }

    private void RefreshList(List<SessionInfo> sessionList)
    {
        this.sessionList = sessionList;
        Debug.Log("Refresh List");
        OnLookingForGameSessions();
        ClearList();
        AddToList();
        if (this.sessionList == null || 
            this.sessionList.Count <= 0)
            OnNoSessionFound();
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
        if (sessionList == null) return;
        
        foreach (SessionInfo sessionInfo in sessionList)
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
        FusionSceneManager.Instance.ConnectToSession(clickedSessionTemplate.sessionInfo.Name);
        
        loadingSessionBar.gameObject.SetActive(true);
        for (int i = 0; i < loadingSessionBar.maximum; i++)
        {
            loadingSessionBar.current = i;
        }
    }
    
    private void OnNoSessionFound()
    {
        statusText.text = "No session found";
        statusText.gameObject.SetActive(true);
    }
    
    private void OnLookingForGameSessions()
    {
        statusText.text = "Looking for sessions";
        statusText.gameObject.SetActive(true);
    }

    public override void LeaveSubScene()
    {
        base.LeaveSubScene();
    }
    
    private void OnDisable()
    {
        FusionSceneManager.Instance.onSessionListUpdated -= RefreshList;
    }
}