using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SessionTemplate : MonoBehaviour
{
    private UI_CreateOrJoinSession ui_CreateOrJoinSession;
    
    private Button sessionBtn;
    
    public SessionInfo sessionInfo;
    
    private TMP_Text sessionNameText;
    [HideInInspector] public TMP_Text playerCountText;

    //Start() won't execute when session list updated.
    private void Awake()
    {
        ui_CreateOrJoinSession = FindObjectOfType<UI_CreateOrJoinSession>();
        sessionBtn = GetComponent<Button>();
        sessionNameText = transform.Find("Session Name").GetComponent<TMP_Text>();
        playerCountText = transform.Find("Player Count").GetComponent<TMP_Text>();
    }
    
    public void SetupSessionTemplate(SessionInfo sessionInfo)
    {
        this.sessionInfo = sessionInfo;
        sessionNameText.text = this.sessionInfo.Name;
        playerCountText.text = $"{this.sessionInfo.PlayerCount.ToString()} / {this.sessionInfo.MaxPlayers.ToString()}";
        
        //Send the clicked session info when clicked.
        sessionBtn.onClick.AddListener(() => ui_CreateOrJoinSession.GetClickedSession(this));
    }
}
