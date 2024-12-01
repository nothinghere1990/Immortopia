using Fusion;
using UnityEngine;

public abstract class MyScene : MonoBehaviour
{
    protected Transform content;
    
    protected int subSceneIndex => FusionSceneManager.Instance.subScenes.IndexOf(this);
    
    protected Transform cam;
    public Vector3 camPos, camRot;
    protected float camMoveSpeed => 1f;
    protected float camRotSpeed => 1f;
    
    protected UI_BlackScreen blackScreen;

    protected virtual void Awake()
    {
        content = transform.Find("Content");
        cam = Camera.main.transform;
        blackScreen = GameObject.Find("UI Black Screen").GetComponent<UI_BlackScreen>();
    }
    
    public virtual void LoadSubScene()
    {
        content.gameObject.SetActive(true);
    }

    public virtual void LeaveSubScene()
    {
        content.gameObject.SetActive(false);
    }
}