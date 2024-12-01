using Fusion;
using TMPro;
using UnityEngine;

public class PlayerWaitingInfoNetwork : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnPlayerIndexChanged))] 
    private NetworkString<_4> playerIndexNetwork { get; set; }
    
    private void Start()
    {
        OnPlayerIndexChanged();
    }
    
    public override void Spawned()
    {
        if (!Object.HasInputAuthority) return;
        
        RPC_SetPlayerWaitingInfo();
    }
    
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    private void RPC_SetPlayerWaitingInfo()
    {
        Debug.Log("bruh");
        
        playerIndexNetwork = Object.InputAuthority.PlayerId.ToString();
    }
    
    private void OnPlayerIndexChanged()
    {
        transform.Find("Player Index").GetComponent<TMP_Text>().text =
            $"P{playerIndexNetwork.ToString()}";
    }
}