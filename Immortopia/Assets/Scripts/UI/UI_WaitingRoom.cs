using UnityEngine;
using UnityEngine.UI;

public class UI_WaitingRoom : MyScene
{
    private Button backBtn;
    
    protected override void Awake()
    {
        base.Awake();
        backBtn = content.Find("Back Button/Button").GetComponent<Button>();
    }

    private void Start()
    {
        backBtn.onClick.AddListener(LeaveSession);
    }
    
    public override void LoadSubScene()
    {
        base.LoadSubScene();
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
}
