using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public abstract class Scene : NetworkBehaviour, IScene
{
    public List<Scene> scenes => CustomSceneManager.Instance.scenes;
    public int sceneIndex => scenes.IndexOf(this);
    
    public GameObject content => transform.Find("Content").gameObject;
    
    public Transform cam => Camera.main.transform;
    public Vector3 camPos, camRot;
    public float camMoveSpeed => 1f;
    public float camRotSpeed => 1f;
    
    public Button startBtn => content.transform.Find("Start Button").GetComponent<Button>();
    public Button quitBtn => content.transform.Find("Quit Button").GetComponent<Button>();
    public Button backBtn => content.transform.Find("Back Button").GetComponent<Button>();

    protected virtual void Start()
    {
        //Show only the first scene.
        if (sceneIndex == 0) GetComponent<IScene>().LoadScene();
        else GetComponent<IScene>().LeaveScene();
    }

    public virtual void LoadScene()
    {
        content.SetActive(true);
    }

    public virtual void LeaveScene()
    {
        content.SetActive(false);
    }
}

public interface IScene
{
    void LoadScene();
    void LeaveScene();
}