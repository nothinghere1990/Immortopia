using UnityEngine;

public abstract class Scene : MonoBehaviour
{
    protected Transform content;
    
    protected int sceneIndex => CustomSceneManager.Instance.scenes.IndexOf(this);
    
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
    
    public virtual void LoadScene()
    {
        content.gameObject.SetActive(true);
    }

    public virtual void LeaveScene()
    {
        content.gameObject.SetActive(false);
    }
}