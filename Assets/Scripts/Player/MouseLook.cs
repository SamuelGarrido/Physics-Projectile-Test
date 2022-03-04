using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    #region Fields

    [Header("Mouse")]
    [SerializeField]
    private float MouseSens;

    private float MouseX;
    private float MouseY;
    private float XRotation;

    [Header("Player")]
    [SerializeField]
    private Transform PlayerBody;

    #endregion

    void Start() {
        //Default mouse sensivity 
        MouseSens = 150f;
        //Lock cursor 
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        //Set Camera movement 
        MouseX = Input.GetAxis("Mouse X") * MouseSens * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * MouseSens * Time.deltaTime;
        //Rotation around X axis
        XRotation -= MouseY;
        XRotation = Mathf.Clamp(XRotation, -90f, 90f); //Block rotation between -90 degrees and 90 degrees
        transform.localRotation = Quaternion.Euler(XRotation, 0f, 0f);
        //Rotation around Y axis
        PlayerBody.Rotate(Vector3.up * MouseX);
    }
}
