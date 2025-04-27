using System;
using System.Drawing;
using UnityEngine;

public class Kinematics
{
    // Denavit-Hartenberg parameters.
    private float[] theta_offset = { 0f, 0f };
    private float[] link_length = { 0.225f, 0.175f };

    public float[] FK(float[] q)
    {
        /* 
        Description:
            Calculates forward kinematics for a 2-joint robotic arm.

        Args:
            q (float[]): Array of two joint angles in degrees.

        Returns:
            Vector2: End-effector position in 2D (x, y) world space.
        */

        if (q == null || q.Length != 2)
        {
            Debug.LogError("q must be an array of length 2.");
            return new float[2];
        }

        float theta_0 = theta_offset[0] + q[0] * Mathf.Deg2Rad;
        float theta_1 = theta_offset[1] + q[1] * Mathf.Deg2Rad;

        float x = link_length[0] * Mathf.Cos(theta_0) + link_length[1] * Mathf.Cos(theta_0 + theta_1);
        float y = link_length[0] * Mathf.Sin(theta_0) + link_length[1] * Mathf.Sin(theta_0 + theta_1);

        return new float[2] {x, y};
    }

    public float[,] IK(Vector3 ee_pos)
    {
        /* 
        Description:
            Calculates inverse kinematics for a 2-joint robotic arm.

        Args:
            ee_pos (Vector3): Target position in 3D space (XZ-plane is used).

        Returns:
            float[,]: Two possible solutions of joint angles in radians.
                      Each row represents one solution (shoulder, elbow).
        */

        float[,] q_solution = new float[2, 2];

        // Project target position onto the 2D plane (XZ plane in Unity).
        float x_new = -ee_pos.x;
        float y_new = ee_pos.z;

        float d_squared = x_new * x_new + y_new * y_new;
        float d = Mathf.Sqrt(d_squared);

        // Calculate intermediate angles using cosine law.
        double beta = (Math.Pow(link_length[0], 2) + d_squared - Math.Pow(link_length[1], 2)) / (2.0 * link_length[0] * d);
        double alpha = (Math.Pow(link_length[0], 2) + Math.Pow(link_length[1], 2) - d_squared) / (2.0 * link_length[0] * link_length[1]);

        // Solve for the first joint angle (shoulder).
        if (beta > 1)
        {
            q_solution[0, 0] = Mathf.Atan2(y_new, x_new);
        }
        else if (beta < -1)
        {
            q_solution[0, 0] = Mathf.Atan2(y_new, x_new) - Mathf.PI;
        }
        else
        {
            float acosBeta = (float)Math.Acos(beta);
            q_solution[0, 0] = Mathf.Atan2(y_new, x_new) - acosBeta;
            q_solution[1, 0] = Mathf.Atan2(y_new, x_new) + acosBeta;
        }

        // Solve for the second joint angle (elbow).
        if (alpha > 1)
        {
            q_solution[0, 1] = Mathf.PI;
        }
        else if (alpha < -1)
        {
            q_solution[0, 1] = 0f;
        }
        else
        {
            q_solution[0, 1] = Mathf.PI - (float)Math.Acos(alpha);
            q_solution[1, 1] = (float)Math.Acos(alpha) - Mathf.PI;
        }

        return q_solution;
    }
}