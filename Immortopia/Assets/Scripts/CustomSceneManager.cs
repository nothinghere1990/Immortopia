using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance;

    private Transform cam;
    
    [SerializeField] private float camMoveSpeed;
    [SerializeField] private float camRotSpeed;
    [TableList, SerializeField] private List<CamPosRot> camPosRots;
    private int camPosRotIndex;
    
    private Animator camAnimator;

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
        camAnimator = cam.GetComponent<Animator>();

        cam.position = camPosRots[0].pos;
        cam.rotation = Quaternion.Euler(camPosRots[0].rot);
    }

    public void LoadCameraPosRot(int inputIndex)
    {
        camPosRotIndex = inputIndex;
    }
    
    public void LoadLastCameraPosRot()
    {
        if (camPosRotIndex > 0) camPosRotIndex -= 1;
    }

    public void StartGameAni()
    {
        
    }

    private void Update()
    {
        //Camera moves
        cam.position = Vector3.MoveTowards(cam.position, camPosRots[camPosRotIndex].pos, camMoveSpeed * Time.deltaTime);
        Quaternion camRotQua = Quaternion.Euler(camPosRots[camPosRotIndex].rot);
        cam.rotation = Quaternion.RotateTowards(cam.rotation, camRotQua, camRotSpeed * Time.deltaTime);
    }
}