import bpy
import numpy as np

"""
Robot Type - Epson LS3-B401S:
    Absolute Joint Position:
        Joint 1: [-40, +220.0] [°]
        Joint 2: [+/- 140.0] [°]

    Denavit-Hartenberg (DH) Standard:
        theta_zero = [   0.0,    0.0]
        a          = [ 0.225,  0.175]

    Note:
        Parameters are expressed only for the first two axes of rotation. The structure is defined 
        as a robotic arm with two joints.
"""

def Forward_Kinematics(theta, theta_zero, a):
    th_0 = theta_zero[0] + theta[0]
    th_1 = theta_zero[1] + theta[1]

    x = np.zeros(theta_zero.size, dtype=np.float64)
    x[0] = a[0]*np.cos(th_0) + a[1]*np.cos(th_0 + th_1)
    x[1] = a[0]*np.sin(th_0) + a[1]*np.sin(th_0 + th_1)
    
    return x

def Inverse_Kinematics(p, theta_zero, a):
    theta_solutions = np.zeros((2, theta_zero.size), dtype=np.float64)
    
    beta = ((a[0]**2) + (p[0]**2 + p[1]**2) - (a[1]**2)) \
            / (2*a[0]*np.sqrt(p[0]**2 + p[1]**2))

    if beta > 1:
        theta_solutions[0, 0] = np.arctan2(p[1], p[0]) 
    elif beta < -1:
        theta_solutions[0, 0] = (np.arctan2(p[1], p[0]) - np.float64(np.pi))
    else:
        theta_solutions[0, 0] = (np.arctan2(p[1], p[0]) - np.arccos(beta))
        theta_solutions[1, 0] = (np.arctan2(p[1], p[0]) + np.arccos(beta))
            
    alpha = ((a[0]**2) + (a[1]**2) - (p[0]**2 + p[1]**2)) \
            / (2*(a[0]*a[1]))

    if alpha > 1:
        theta_solutions[0, 1] = np.float64(np.pi)
    elif alpha < -1:
        theta_solutions[0, 1] = 0.0
    else:
        theta_solutions[0, 1] = np.float64(np.pi) - np.arccos(alpha)
        theta_solutions[1, 1] = np.arccos(alpha) - np.float64(np.pi)

    return theta_solutions


def main():
    p_fk = Forward_Kinematics(np.array(np.deg2rad([0.0,0.0]), dtype=np.float64), 
                              np.array([0.0, 0.0], dtype=np.float64), 
                              np.array([0.225, 0.175], dtype=np.float64))

    viewpoint_location = bpy.data.objects['Viewpoint_EE_EPSON_LS3_B401S_ID_001'].location                                         
    th_sol = Inverse_Kinematics(np.array([viewpoint_location.x, viewpoint_location.y], dtype=np.float64), 
                                np.array([0.0, 0.0], dtype=np.float64), 
                                np.array([0.225, 0.175], dtype=np.float64))

    bpy.data.objects['Joint_1_Ghost_EPSON_LS3_B401S_ID_001'].rotation_euler = [0.0, 0.0, th_sol[0, 0]]
    bpy.data.objects['Joint_2_Ghost_EPSON_LS3_B401S_ID_001'].rotation_euler = [0.0, 0.0, th_sol[0, 1]]
    

if __name__ == '__main__':
    main()