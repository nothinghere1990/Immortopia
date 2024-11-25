using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : Scene
{
    public static TMP_InputField PlayerNameInput;
    
    private Button startBtn, quitBtn;
    private TMP_Text startBtnText;
    private Sequence startBtnSequence;
    
    private ProgressBar LoadingLobbyBar;

    protected override void Awake()
    {
        base.Awake();
        
        PlayerNameInput = content.Find("Player Name Input Field").GetComponent<TMP_InputField>();
        startBtn = content.Find("Start Button").GetComponent<Button>();
        quitBtn = content.Find("Quit Button").GetComponent<Button>();
        LoadingLobbyBar = content.Find("Loading Bar").GetComponent<ProgressBar>();
        
        camPos = new Vector3(-7.25f, 8, -7.25f);
        camRot = new Vector3(15, 45, 0);
    }

    private void Start()
    {
        PlayerNameInput.Select();
        //Last Player Name
        if (PlayerPrefs.HasKey("PlayerName"))
            PlayerNameInput.text = PlayerPrefs.GetString("PlayerName");
        
        startBtn.onClick.AddListener(UpdatePlayerName);
        startBtn.onClick.AddListener(() => ConnectToLobby(false));
        quitBtn.onClick.AddListener(QuitGame);
        
        FusionConnection.Instance.onConnectedToLobby += () => ConnectToLobby(true);
    }

    public override void LoadScene()
    {
        base.LoadScene();
        LoadingLobbyBar.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
        CamMove();
        StartGameTextAni(true);
        StartCoroutine(blackScreen.FadeOut(.3f));
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
    
    private void UpdatePlayerName()
    {
        if (string.IsNullOrWhiteSpace(PlayerNameInput.text))
        {
            PlayerPrefs.SetString("PlayerName", "Guest");
            PlayerPrefs.Save();
            return;
        }
        
        PlayerPrefs.SetString("PlayerName", PlayerNameInput.text);
        PlayerPrefs.Save();
    }

    private void ConnectToLobby(bool isDone)
    {
        if (isDone)
        {
            LoadingLobbyBar.current = LoadingLobbyBar.maximum;
            CustomSceneManager.Instance.LoadScene(sceneIndex + 1);
            return;
        }
        
        //Loading
        startBtn.gameObject.SetActive(false);
        
        FusionConnection.Instance.ConnectToLobby("Lobby");
        
        LoadingLobbyBar.gameObject.SetActive(true);
        for (int i = 0; i < LoadingLobbyBar.maximum; i++)
        {
            LoadingLobbyBar.current = i;
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
