using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreateSession : Scene
{
    private Transform createSessionWindow;
    private TMP_InputField inputSessionName;
    private Button createSessionBtn, backBtn;
    private ProgressBar loadingSessionBar;
    
    protected override void Awake()
    {
        base.Awake();
        
        backBtn = content.Find("Back Button").GetComponent<Button>();
        createSessionBtn = content.Find("Create Session").GetComponent<Button>();
        
        createSessionWindow = content.Find("Create Session Window");
        inputSessionName = createSessionWindow.transform.Find("Scroll Area/InputField (TMP)").GetComponent<TMP_InputField>();
        
        loadingSessionBar = content.Find("Loading Bar").GetComponent<ProgressBar>();
    }
    
    private void Start()
    {
        backBtn.onClick.AddListener(CustomSceneManager.Instance.LoadLastScene);
        createSessionBtn.onClick.AddListener(CreateSession);
    }

    public override void LoadScene()
    {
        base.LoadScene();
        loadingSessionBar.gameObject.SetActive(false);
        createSessionBtn.gameObject.SetActive(true);
        inputSessionName.Select();
    }
    
    public void CreateSession()
    {
        createSessionBtn.gameObject.SetActive(false);
        
        FusionConnection.Instance.ConnectToSession(inputSessionName.text);
        
        loadingSessionBar.gameObject.SetActive(true);
        for (int i = 0; i < loadingSessionBar.maximum; i++)
        {
            loadingSessionBar.current = i;
        }
    }

    public override void LeaveScene()
    {
        base.LeaveScene();
    }
}
