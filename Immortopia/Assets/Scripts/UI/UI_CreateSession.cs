using System;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CreateSession : Scene
{
    private Transform createSessionWindow;
    private TMP_InputField inputSessionName;
    private Button createSessionBtn;
    
    protected override void Start()
    {
        createSessionBtn = content.transform.Find("Create Session").GetComponent<Button>();
        createSessionWindow = content.transform.Find("Create Session Window");
        inputSessionName = createSessionWindow.transform.Find("Scroll Area/InputField (TMP)").GetComponent<TMP_InputField>();
        
        backBtn.onClick.AddListener(CustomSceneManager.Instance.LoadLastScene);
        createSessionBtn.onClick.AddListener(CreateSession);
        
        base.Start();
    }

    public override void LoadScene()
    {
        base.LoadScene();
    }
    
    public void CreateSession()
    {
        FusionConnection.Instance.ConnectToSession(inputSessionName.text);
    }

    public override void LeaveScene()
    {
        base.LeaveScene();
    }
}
