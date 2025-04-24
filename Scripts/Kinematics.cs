using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kinematics
{
    // DH parameters
    private float[] Thetas_Zero = { 0 , 0 };
    private float[] a = { 0.225f, 0.175f };

    public float[] Forward_Kinematics(float[] Joints) 
    {
        float theta_0 = Thetas_Zero[0] + Joints[0] * Mathf.Deg2Rad;
        float theta_1 = Thetas_Zero[1] + Joints[1] * Mathf.Deg2Rad;

        float[] x = new float[Thetas_Zero.Length];
        x[0] = a[0] * Mathf.Cos(theta_0) + a[1] * Mathf.Cos(theta_0 + theta_1);
        x[1] = a[0] * Mathf.Sin(theta_0) + a[1] * Mathf.Sin(theta_0 + theta_1);

        // Returns X Y position (2D) 
        return x;
    }

    public float[,] Inverse_Kinematics(Vector3 Pos)
    {
        // Init array of solutons
        float[,] theta_solutions = {
        { 0, 0 },
        { 0, 0 }
        };

        float[] p = new float[2];
        // Conversion from unity coordinate system
        p[0] = -Pos.x;
        p[1] = Pos.z;

        double beta = ((Math.Pow(a[0], 2f) + (Math.Pow(p[0], 2f) + Math.Pow(p[1], 2f)) - Math.Pow(a[1], 2f))
              / (2f * a[0] * Math.Sqrt(Math.Pow(p[0], 2f) + Math.Pow(p[1], 2f))));

        if (beta > 1)
        {
            theta_solutions[0, 0] = Mathf.Atan2(p[1], p[0]);
        }
        else if (beta < -1)
        {
            theta_solutions[0, 0] = Mathf.Atan2(p[1], p[0]) - Mathf.PI;
        }
        else
        {
            theta_solutions[0, 0] = Mathf.Atan2(p[1], p[0]) - (float)Math.Acos(beta);
            theta_solutions[1, 0] = Mathf.Atan2(p[1], p[0]) + (float)Math.Acos(beta);
        }

        double alpha = ((Math.Pow(a[0], 2f) + Math.Pow(a[1], 2f) - (Math.Pow(p[0], 2f) + Math.Pow(p[1], 2f)))
                       / (2f * a[0] * a[1]));

        if (alpha > 1)
        {
            theta_solutions[0, 1] = Mathf.PI;
        }
        else if (alpha < -1)
        {
            theta_solutions[0, 1] = 0f;
        }
        else
        {
            theta_solutions[0, 1] = Mathf.PI - (float)Math.Acos(alpha);
            theta_solutions[1, 1] = (float)Math.Acos(alpha) - Mathf.PI;
        }

        // Returns array of 2 solutions
        return theta_solutions;
    }

}
