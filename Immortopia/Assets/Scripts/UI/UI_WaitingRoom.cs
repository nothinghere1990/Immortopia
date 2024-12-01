using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_WaitingRoom : MyScene
{
    private Button backBtn;
    private NetworkObject playerWaitingInfoTemplate { get; set; }
    
    protected override void Awake()
    {
        base.Awake();
        
        backBtn = content.Find("Back Button/Button").GetComponent<Button>();
    }

    private void Start()
    {
        backBtn.onClick.AddListener(LeaveSession);
        
        FusionSceneManager.Instance.onPlayerJoined += AddPlayerWaitingInfo;
    }
    
    public override void LoadSubScene()
    {
        base.LoadSubScene();
    }
    
    private void AddPlayerWaitingInfo(NetworkRunner runner, PlayerRef player)
    {
        if (FusionSceneManager.Instance.currentSubSceneIndex != subSceneIndex || !runner.IsServer) return;
        
        playerWaitingInfoTemplate = runner.Spawn
            (GameAssets.i.playerWaitingInfoTemplate, Vector3.zero, Quaternion.identity, player);
    }
    
    private void LeaveSession()
    {
        backBtn.gameObject.SetActive(false);
        FusionSceneManager.Instance.DisconnectFromSession();
    }
    
    public override void LeaveSubScene()
    {
        base.LeaveSubScene();
    }
    
    private void OnDisable()
    {
        FusionSceneManager.Instance.onPlayerJoined -= AddPlayerWaitingInfo;
    }
}
