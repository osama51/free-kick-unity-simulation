using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchCameras : MonoBehaviour
{
    
    public Camera MainCamera;
    public Camera FrontCamera;
    public Camera SideCamera;

    // camera will follow this object
    public Transform Target;
    //camera transform
    public Transform camTransform;
    // offset between camera and target
    public Vector3 Offset;
    // change this value to get desired smoothness
    public float SmoothTime = 0.2f;

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        Offset = new Vector3(-9,0,-9);//camTransform.position.x - Target.position.x;
    }

    
    // Update is called once per frame
    int camera_count = 0;
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.C)){
            switch(camera_count)
            {
            case 0:
                MainView();
                ++camera_count;
                break;

            case 1:
                SideView();
                ++camera_count;
                break;

            case 2:
                FrontView();
                camera_count = 0;
                break;
            
            }

        }
    }
    private void LateUpdate()
    {
        // update position
        Vector3 targetPosition = new Vector3(Target.position[0] + Offset[0], camTransform.position.y, Target.position[2] + Offset[2]);
        camTransform.position = targetPosition;//Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

        // update rotation
        transform.LookAt(Target);
    }

    public void MainView() {
        MainCamera.enabled = true;
        FrontCamera.enabled = false;
        SideCamera.enabled = false;
    }
    public void FrontView() {
        MainCamera.enabled = false;
        FrontCamera.enabled = true;
        SideCamera.enabled = false;
    }
    
    public void SideView() {
        MainCamera.enabled = false;
        FrontCamera.enabled = false;
        SideCamera.enabled = true;
    }
}
