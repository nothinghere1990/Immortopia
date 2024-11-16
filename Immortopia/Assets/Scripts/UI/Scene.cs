using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Scene : MonoBehaviour
{
    public List<Scene> scenes => CustomSceneManager.Instance.scenes;
    public int sceneIndex => scenes.IndexOf(this);
    
    public Transform content => transform.Find("Content");
    
    public Transform cam => Camera.main.transform;
    public Vector3 camPos, camRot;
    public float camMoveSpeed => 1f;
    public float camRotSpeed => 1f;
    
    public Button startBtn => content.Find("Start Button").GetComponent<Button>();
    public Button quitBtn => content.Find("Quit Button").GetComponent<Button>();
    public Button backBtn => content.Find("Back Button").GetComponent<Button>();

    protected virtual void Start()
    {
        //Show only the first scene.
        if (sceneIndex == 0) LoadScene();
        else LeaveScene();
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