using UnityEngine;

public class UI_Session : Scene
{
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        blackScreen.onFadeInComplete += () => StartCoroutine(blackScreen.FadeOut(.3f));
    }
    
    public override void LoadScene()
    {
        base.LoadScene();
    }
    
    public override void LeaveScene()
    {
        base.LeaveScene();
    }
}
