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
        
        //Add all scenes to the list before Start() is called.
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                scenes.Add(GameObject.Find("UI Main Menu").GetComponent<Scene>());
                scenes.Add(GameObject.Find("UI Create or Join Session").GetComponent<Scene>());
                scenes.Add(GameObject.Find("UI Create Session").GetComponent<Scene>());
                break;
            case 1:
                break;
        }
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