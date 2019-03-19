using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds; //Array of all back and foregrounds tp be parallexed
    private float[] parallexscales; //proportion of the camera's movement to move the backgrounds by
    public float smoothing; //how smooth the paralllec in going to be,make sure to set this above zero
    private Transform cam;  //referrence to the main cameras transform
    private Vector3 previousCamPos;//the position of the camera in the previous frame
    //is called before Start().great for referrences
    void Awake()
    {
        //set up the referrence
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        //the previous frame had the cuurent frame's camera position
        previousCamPos = cam.position;
        //assigning cooresponding parallexscales
        parallexscales = new float[backgrounds.Length];
        for(int i=0;i<backgrounds.Length;i++)
        {
            parallexscales[i] = backgrounds[i].position.z*-1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<backgrounds.Length;i++)
        {
            //parallex in the opposite of the camera movement because the previous frame multiplied by the scale
            float parallex = (previousCamPos.x - cam.position.x) * parallexscales[i];
            //set a target x position  which is the current position plus the parallex
            float backgroundTargetPosX = backgrounds[i].position.x + parallex;
            // create a target pposition which is the background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            //fade between the current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);

        }
        //set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
