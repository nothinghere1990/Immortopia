using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreateSession : Scene
{
    private TMP_Text playerNameText;
    
    private Transform createSessionWindow;
    private TMP_InputField inputSessionName;
    private Button createSessionBtn, backBtn;
    private ProgressBar loadingSessionBar;
    
    protected override void Awake()
    {
        base.Awake();
        
        playerNameText = content.Find("Player Name").GetComponent<TMP_Text>();
        
        backBtn = content.Find("Back Button").GetComponent<Button>();
        createSessionBtn = content.Find("Create Session").GetComponent<Button>();
        
        createSessionWindow = content.Find("Create Session Window");
        inputSessionName = createSessionWindow.transform.Find("Scroll Area/Session Name Input Field").GetComponent<TMP_InputField>();
        
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
        
        //Set player name.
        if (string.IsNullOrWhiteSpace(UI_MainMenu.PlayerNameInput.text))
            playerNameText.text = "Guest";
        else
            playerNameText.text = UI_MainMenu.PlayerNameInput.text;
        
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
