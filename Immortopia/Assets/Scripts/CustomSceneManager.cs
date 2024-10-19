using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance {  get; private set; }

    private Transform cam;
    
    [SerializeField] private float camMoveSpeed;
    [SerializeField] private float camRotSpeed;
    [TableList, SerializeField] private List<CamPosRot> camPosRots;
    [HideInInspector] public int camPosRotIndex;

    public static Action OnSceneLoaded;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this);
    }

    private void Start()
    {
        cam = Camera.main.transform;
        
        cam.position = camPosRots[0].pos;
        cam.rotation = Quaternion.Euler(camPosRots[0].rot);
    }

    public void LoadCameraPosRot(int inputIndex)
    {
        camPosRotIndex = inputIndex;
        OnSceneLoaded?.Invoke();
    }
    
    public void LoadLastCameraPosRot()
    {
        if (camPosRotIndex > 0) camPosRotIndex -= 1;
        OnSceneLoaded?.Invoke();
    }
    
    private void Update()
    {
        //Camera moves
        cam.position = Vector3.Lerp(cam.position, camPosRots[camPosRotIndex].pos, camMoveSpeed * Time.deltaTime);
        Quaternion camRotQua = Quaternion.Euler(camPosRots[camPosRotIndex].rot);
        cam.rotation = Quaternion.Slerp(cam.rotation, camRotQua, camRotSpeed * Time.deltaTime);
    }
}