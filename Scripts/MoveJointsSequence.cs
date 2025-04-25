using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJointsSequence : MonoBehaviour
{
    public Transform joint1; // assign in Inspector
    public Transform joint2; // assign in Inspector


    private float targetAngle1; // Degrees, 
    private float targetAngle2; // Degrees

    public Transform joint1_ghost;  // Assign in Inspector
    public Transform joint2_ghost;  

    
    public bool Execute_Movement;   // Start movement, Target is ghost position

    public float smoothTime = 0.5f; // Damping time
    private bool isMoving = false;  // State variable
    private float velocity1 = 0f;   // Angular velocity for joint1
    private float velocity2 = 0f;   // Angular velocity for joint2

    private int Program_State;
    public bool Execute_Sequence;


    private void Start()
    {
        Program_State = 1;
    }


    void Update()
    {

        if (Execute_Sequence && !isMoving)
        {
            SequenceProgram();
        }
        else if (!Execute_Sequence)
        {
        targetAngle1 = joint1_ghost.localEulerAngles.z;
        targetAngle2 = joint2_ghost.localEulerAngles.z;
        }


        if (isMoving)
        {
            bool joint1Done = RotateJointSmooth(joint1, ref velocity1, targetAngle1);
            bool joint2Done = RotateJointSmooth(joint2, ref velocity2, targetAngle2);

            if (joint1Done && joint2Done)
            {
                isMoving = false;
            }
        }

        if (Execute_Movement) // trigger motion
        {
            isMoving = true;
            Execute_Movement = false;
        }
    }

    bool RotateJointSmooth(Transform joint, ref float velocity, float targetAngle)
    {
        float currentZ = joint.localEulerAngles.z;

        // SmoothDampAngle gradually changes an angle given in degrees towards a desired goal angle over time.
        // https://docs.unity3d.com/ScriptReference/Mathf.SmoothDampAngle.html
        float smoothZ = Mathf.SmoothDampAngle(currentZ, targetAngle, ref velocity, smoothTime);

        joint.localEulerAngles = new Vector3(0f, 0f, smoothZ);

        return Mathf.Abs(Mathf.DeltaAngle(currentZ, targetAngle)) < 0.1f;
    }


    private void SequenceProgram() 
    {
        switch(Program_State)
            {
            case 1:
                targetAngle1 = 90;
                targetAngle2 = 0f;
                Program_State = 2;
                Execute_Movement = true;
                break;

            case 2:
                targetAngle1 = 180f;
                targetAngle2 = 90f;
                Program_State = 3;
                Execute_Movement = true;
                break;

            case 3:
                targetAngle1 = 120f;
                targetAngle2 = -90f;
                Program_State = 1;
                Execute_Movement = true;
                break;

        }
    
    }



}