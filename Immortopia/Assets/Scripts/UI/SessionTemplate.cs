using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionTemplate : UI_CreateOrJoinSession
{
    private Button sessionBtn;
    
    private TMP_Text sessionNameText;
    public TMP_Text playerCountText;

    public SessionInfo sessionInfo;

    private void Start()
    {
        sessionBtn = GetComponent<Button>();
        sessionNameText = transform.Find("Session Name").GetComponent<TMP_Text>();
        playerCountText = transform.Find("Player Count").GetComponent<TMP_Text>();
    }
    
    public void SetupSessionTemplate()
    {
        sessionInfo = FusionConnection.Instance.sessionInfo;

        sessionNameText.text = sessionInfo.Name;
        playerCountText.text = $"{sessionInfo.PlayerCount.ToString()} / {sessionInfo.MaxPlayers.ToString()}";
        
        //Send the clicked session info.
        sessionBtn.onClick.AddListener(() => GetClickedSession(this));
    }
}
