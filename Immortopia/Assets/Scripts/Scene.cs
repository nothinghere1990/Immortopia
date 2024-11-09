using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{
    public List<Scene> scenes => CustomSceneManager.Instance.scenes;
    
    public int sceneIndex;
    
    public Transform cam => Camera.main.transform;
    public Vector3 camPos, camRot;
    public float camMoveSpeed => 1f;
    public float camRotSpeed => 1f;
}

public interface IScene
{
    void LoadScene();
    void LeaveScene();
}