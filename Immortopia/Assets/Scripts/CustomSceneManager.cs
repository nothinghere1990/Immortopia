using System.Collections.Generic;
using UnityEngine;

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
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
        
        //Add all scenes to the list before Start() is called.
        scenes.Add(GameObject.Find("UI Main Menu").GetComponent<Scene>());
        scenes.Add(GameObject.Find("UI Create or Join Session").GetComponent<Scene>());
        scenes.Add(GameObject.Find("UI Create Session").GetComponent<Scene>());
    }

    public void LoadScene(int inputIndex)
    {
        scenes[currentSceneIndex].LeaveScene();
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