using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance {  get; private set; }
    
    public int currentSceneIndex;
    public List<Scene> scenes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }
    
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        FusionConnection.Instance.onLoadSceneCompleted += AddScenes;
        
        AddScenes();
        
        LoadScene(0);
    }

    private void AddScenes()
    {
        scenes.Clear();
        
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                scenes.Add(GameObject.Find("UI Main Menu").GetComponent<Scene>());
                scenes.Add(GameObject.Find("UI Create or Join Session").GetComponent<Scene>());
                scenes.Add(GameObject.Find("UI Create Session").GetComponent<Scene>());
                break;
            case 1:
                scenes.Add(GameObject.Find("UI Session").GetComponent<Scene>());
                break;
        }
    }

    public void LoadScene(int inputIndex)
    {
        foreach (Scene scene in scenes)
        {
            scene.LeaveScene();
        }
        currentSceneIndex = inputIndex;
        scenes[currentSceneIndex].LoadScene();
    }
    
    public void LoadLastScene()
    {
        scenes[currentSceneIndex].LeaveScene();
        if (currentSceneIndex > 0) currentSceneIndex -= 1;
        scenes[currentSceneIndex].LoadScene();
    }
}