using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private CharacterController charCon;
    
    private Vector3 mouseInput;
    
    private float camMoveSpeed;
    private float camZoomSpeed;

    private void Awake()
    {
        charCon = GetComponent<CharacterController>();
    }

    private void Start()
    {
        SceneManager.sceneUnloaded += ActiveCursor;
        
        camMoveSpeed = 5;
        camZoomSpeed = 7;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        charCon.Move(new Vector3(mouseInput.x * camMoveSpeed, 0, mouseInput.z * camMoveSpeed));
    }

    private void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse X")) < 1) mouseInput.x = Input.GetAxis("Mouse X");
        if (Mathf.Abs(Input.GetAxis("Mouse Y")) < 1) mouseInput.z = Input.GetAxis("Mouse Y");
        if (Input.GetAxis("Mouse ScrollWheel") > 0) LiftAndDrop(-1);
        if (Input.GetAxis("Mouse ScrollWheel") < 0) LiftAndDrop(1);
    }

    private void LiftAndDrop(float mouseInputY)
    {
        charCon.Move(mouseInputY * Vector3.up);
    }

    private void ActiveCursor(Scene scene)
    {
        Cursor.lockState = CursorLockMode.None;
    }
}