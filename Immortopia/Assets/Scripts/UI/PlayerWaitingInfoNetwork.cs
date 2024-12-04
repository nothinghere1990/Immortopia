using Fusion;
using TMPro;
using UnityEngine;

public class PlayerWaitingInfoNetwork : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnPlayerInfoChanged))] 
    private int playerIndexNetwork { get; set; }
    
    private TMP_Text playerIndexText;
    
    private void Awake()
    {
        playerIndexText = transform.Find("Player Index").GetComponent<TMP_Text>();
    }
    
    private void Start()
    {
        OnPlayerInfoChanged();
    }
    
    public override void Spawned()
    {
        if (!Object.HasInputAuthority) return;
        
        RPC_SetPlayerWaitingInfo();
    }
    
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_SetPlayerWaitingInfo()
    {
        playerIndexNetwork = NetworkPlayerManager.Instance.playerList.
            FindIndex(playerInfo => playerInfo.player == Object.InputAuthority) + 1;
    }
    
    private void OnPlayerInfoChanged()
    {
        transform.SetParent(GameObject.Find("UI Waiting Room/Content").transform);
        transform.localRotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
        transform.localScale = Vector3.one;
        transform.localPosition = playerIndexNetwork switch
        {
            1 => new Vector3(0f, -1f, 7f),
            2 => new Vector3(9.5f, -1f, 0f),
            3 => new Vector3(0f, -1f, -7f),
            4 => new Vector3(-9.5f, -1f, 0f),
            _ => transform.position
        };
        
        playerIndexText.text = $"P{playerIndexNetwork.ToString()}";
    } 
}