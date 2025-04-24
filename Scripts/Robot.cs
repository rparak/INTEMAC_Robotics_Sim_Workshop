using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    // Joints
    private int J = 2;                                  // Number of Joints
    [Range(-180f, 180f)]
    public float[] Target_Angle;                        // Target Angle [Degrees]
    public Transform Joint_1_Ghost;
    public Transform Joint_2_Ghost;


    // ViewPoint
    public GameObject ViewPoint;                        // ViewPoint gameobject reference
    private Transform ViewPoint_Transform;
    public bool Execute_FK;                             // Button sends ViewPoint to Ghost
    public bool Execute_IK;                             // Button sends Ghost to ViewPoint


    // Kinematics
    private Kinematics Robot_Kinematics;
    private float[,] Angles = 
                {
                { 0, 0 },
                { 0, 0 }
                };

    // Start is called before the first frame update
    void Start()
    {
        Target_Angle = new float[J];                   // Array init
        ViewPoint_Transform = ViewPoint.transform;
        Robot_Kinematics = new Kinematics();
    }

    // Update is called once per frame
    void Update()
    {
        Joint_1_Ghost.localRotation = Quaternion.Euler(0, 0, Target_Angle[0]);
        Joint_2_Ghost.localRotation = Quaternion.Euler(0, 0, Target_Angle[1]);

        if (Execute_FK)
        {
            // Calculate Forward Kinematics acording to actual Angle values in Joint_Angles[]
            float[] Viewpoint_Target = new float[2];
            Viewpoint_Target = Robot_Kinematics.Forward_Kinematics(Target_Angle);

            // Move Viewpoint
            // * Viewpoint moves in X and Z axis, Y is constant
            float actual_y = ViewPoint_Transform.position.y;
            ViewPoint_Transform.position = new Vector3(-Viewpoint_Target[0], actual_y, Viewpoint_Target[1]);
            // Reset button
            Execute_FK = false;
        }

        if (Execute_IK)
        {
            // Gets position from viewpoint
            Vector3 Pos = ViewPoint_Transform.position;
            // Calculates Inverse Kinematics according to  ViewPoint_Transform
            Angles = Robot_Kinematics.Inverse_Kinematics(Pos);

            // Takes first solutions of IK
            Target_Angle[0] = Angles[0, 0] * Mathf.Rad2Deg;                                 // Changes value on input slider
            Target_Angle[1] = Angles[0, 1] * Mathf.Rad2Deg;

            Execute_IK = false;
        }

        
    }
}
