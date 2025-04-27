using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    // Number of joints for the robot (adjustable).
    private const int q_num = 2;

    // Target angles for each joint (in degrees).
    [Range(-180f, 180f)]
    public float[] q;

    // Transforms representing the ghost models of the robot's joints.
    public Transform q_1_transform_ghost;
    public Transform q_2_transform_ghost;

    // The viewpoint to be moved via forward kinematics (FK) or inverse kinematics (IK).
    public GameObject viewpoint_obj;
    private Transform viewpoint_transform;

    // Flags to trigger forward kinematics (FK) and inverse kinematics (IK) calculations.
    public bool fk_flag;
    public bool ik_flag;

    // Kinematics calculations class (Handles both FK and IK).
    private Kinematics Kinematics_Cls;

    // Storage for calculated joint angles (in radians) during inverse kinematics.
    private float[,] q_in_radians = new float[2, 2];

    void Start()
    {
        // Initialize arrays and references.
        q = new float[q_num];
        viewpoint_transform = viewpoint_obj.transform;
        Kinematics_Cls = new Kinematics();
    }

    void Update()
    {
        // Update the ghost joints rotations based on the current target angles.
        Update_Ghost();

        // Execute Forward Kinematics if triggered.
        if (fk_flag)
        {
            Forward_Kinematics();

            // Reset the flag after execution.
            fk_flag = false;
        }

        // Execute Inverse Kinematics if triggered.
        if (ik_flag)
        {
            Inverse_Kinematics();

            // Reset the flag after execution.
            ik_flag = false;
        }
    }

    private void Update_Ghost()
    {
        q_1_transform_ghost.localRotation = Quaternion.Euler(0, 0, q[0]);
        q_2_transform_ghost.localRotation = Quaternion.Euler(0, 0, q[1]);
    }

    private void Forward_Kinematics()
    {
        // Get the target position of the viewpoint based on current joint angles.
        float[] viewpoint_target = Kinematics_Cls.FK(q);

        // Move the viewpoint in the X and Z axes (Y remains constant).
        float y_position = viewpoint_transform.position.y;
        viewpoint_transform.position = new Vector3(-viewpoint_target[0], y_position, viewpoint_target[1]);
    }

    private void Inverse_Kinematics()
    {
        // Get the current position of the viewpoint.
        Vector3 viewpoint_position = viewpoint_transform.position;

        // Calculate the joint angles needed to reach the viewpoint position.
        q_in_radians = Kinematics_Cls.IK(viewpoint_position);

        // Set the target angles based on the first set of IK solutions (convert radians to degrees)..
        q[0] = q_in_radians[0, 0] * Mathf.Rad2Deg;
        q[1] = q_in_radians[0, 1] * Mathf.Rad2Deg;
    }
}