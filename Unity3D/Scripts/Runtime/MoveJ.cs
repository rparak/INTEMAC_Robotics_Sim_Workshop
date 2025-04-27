using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoints : MonoBehaviour
{
    // Public references assigned in the Inspector.
    // These are the transforms of the robotic joints and their ghost representations.
    public Transform q_1_transform;
    public Transform q_2_transform;
    public Transform q_1_transform_ghost;
    public Transform q_2_transform_ghost;

    // Determines whether the movement should be executed.
    // Set to true to trigger the movement between target angles.
    public bool execute_movement;

    // Smooth time for damping the rotation of the joints.
    public float smooth_time = 0.5f;

    // Target angles for each joint (in degrees).
    private float q_1_target;
    private float q_2_target;

    // Indicates whether the joints are in motion.
    private bool is_moving = false;

    // Stores angular velocity for smooth damping of joint rotations.
    private float velocity_1 = 0f;
    private float velocity_2 = 0f;

    void Update()
    {
        // Update target angles from the ghost transforms.
        q_1_target = q_1_transform_ghost.localEulerAngles.z;
        q_2_target = q_2_transform_ghost.localEulerAngles.z;

        // If the joints are currently moving, rotate them smoothly.
        if (is_moving)
        {
            bool q_1_done = RotateJointSmooth(q_1_transform, ref velocity_1, q_1_target);
            bool q_2_done = RotateJointSmooth(q_2_transform, ref velocity_2, q_2_target);

            // If both joints are done rotating, stop the movement.
            if (q_1_done && q_2_done)
            {
                is_moving = false;
            }
        }

        // If movement is triggered, start the movement.
        if (execute_movement)
        {
            is_moving = true;
            execute_movement = false; // Reset the flag to avoid continuous triggering.
        }
    }

    private bool RotateJointSmooth(Transform joint, ref float velocity, float target_angle)
    {
        /* 
        Description:
            Smoothly rotates a joint to the target angle using damping.

        Args:
            joint (Transform): The joint to rotate.
            velocity (ref float): The angular velocity (modified by the function).
            target_angle (float): The desired target angle in degrees.

        Returns:
            bool: Returns true if the rotation is close enough to the target, otherwise false.
        */

        float current_z = joint.localEulerAngles.z;
        // Smoothly damp the rotation towards the target angle.
        float smooth_z = Mathf.SmoothDampAngle(current_z, target_angle, ref velocity, smooth_time);

        // Apply the smoothed rotation to the joint.
        joint.localEulerAngles = new Vector3(0f, 0f, smooth_z);

        // Check if the joint is sufficiently close to the target angle.
        return Mathf.Abs(Mathf.DeltaAngle(current_z, target_angle)) < 0.1f;
    }
}