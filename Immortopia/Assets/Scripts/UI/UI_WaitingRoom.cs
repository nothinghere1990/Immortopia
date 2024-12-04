using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class UI_WaitingRoom : MyScene
{
    private Button backBtn;
    private GameObject playerWaitingInfoTemplatePrefab;
    
    protected override void Awake()
    {
        base.Awake();
        
        backBtn = content.Find("Back Button/Button").GetComponent<Button>();
        playerWaitingInfoTemplatePrefab = GameAssets.i.playerWaitingInfoTemplate;
    }

    private void Start()
    {
        backBtn.onClick.AddListener(LeaveSession);
        
        NetworkPlayerManager.Instance.onPlayerJoined += AddPlayerWaitingInfo;
        NetworkPlayerManager.Instance.onPlayerLeft += RemovePlayerWaitingInfo;
    }
    
    public override void LoadSubScene()
    {
        base.LoadSubScene();
    }
    
    private void AddPlayerWaitingInfo(NetworkRunner runner, PlayerRef player)
    {
        if (NetworkSceneManager.Instance.currentSubSceneIndex != subSceneIndex || !runner.IsServer) return;
        
        NetworkPlayerManager.Instance.playerList.Add(new NetworkPlayerManager.PlayerInfo
        {
            player = player,
            networkObject = null,
            playerName = PlayerPrefs.GetString("PlayerName"),
            isReady = false
        });

        NetworkPlayerManager.PlayerInfo updatedPlayerInfo = NetworkPlayerManager.Instance.playerList[^1];
        updatedPlayerInfo.networkObject =
            runner.Spawn(playerWaitingInfoTemplatePrefab, Vector3.zero, Quaternion.identity, player);
        NetworkPlayerManager.Instance.playerList[^1] = updatedPlayerInfo;
    }
    
    private void RemovePlayerWaitingInfo(NetworkRunner runner, PlayerRef player)
    {
        if (NetworkSceneManager.Instance.currentSubSceneIndex != subSceneIndex || !runner.IsServer) return;
        
        int leavePlayerIndex =
            NetworkPlayerManager.Instance.playerList.FindIndex(playerInfo => playerInfo.player == player);
        
        runner.Despawn(NetworkPlayerManager.Instance.playerList[leavePlayerIndex].networkObject);
        NetworkPlayerManager.Instance.playerList.RemoveAll(playerInfo => playerInfo.player == player);
    }
    
    private void LeaveSession()
    {
        backBtn.gameObject.SetActive(false);
        NetworkSceneManager.Instance.DisconnectFromSession();
    }
    
    public override void LeaveSubScene()
    {
        base.LeaveSubScene();
    }
    
    private void OnDisable()
    {
        NetworkPlayerManager.Instance.onPlayerJoined -= AddPlayerWaitingInfo;
        NetworkPlayerManager.Instance.onPlayerLeft -= RemovePlayerWaitingInfo;
    }
}
