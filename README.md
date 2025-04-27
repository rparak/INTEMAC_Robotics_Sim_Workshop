# INTEMAC Robotics Simulation Workshop for Master Students at Brno University of Technology

## About

This repository provides materials for a 3-hour Robotics Simulation Workshop designed for Master’s students at the Brno University of Technology, Faculty of Mechanical Engineering, Institute of Automation and Computer Science. The workshop introduces fundamental concepts of robotics simulation using Blender and Unity3D. It focuses on the simulation and control of the EPSON SCARA robot in a 2D plane (X, Y), with an emphasis on calculating forward and inverse kinematics.

Students will learn how to create and manipulate kinematic models in Blender and then transfer those models to Unity3D for simulation and control. The workshop introduces both Python and C# programming languages, demonstrating how to calculate robot joint movements using mathematical kinematics models and how to control those movements in Unity3D with physics-based simulations.

Key topics include:
1. Implementing inverse and forward kinematics for the EPSON SCARA robot using Python in Blender.
2. Exporting 3D models from Blender to Unity3D for simulation.
3. Basic Unity3D physics and object manipulation using C#.
4. Controlling robot movements in Unity3D with inverse kinematics and using SmoothDampAngle to smoothly rotate joints.

The workshop aims to equip BUT students with foundational skills in Blender, Unity3D, and robotics simulation, and is designed for beginners with no prior or basic experience in these areas.

# Workshop Content Overview

**Part 1: Blender for Robotics Simulation**

The first part of the workshop focuses on using Blender for robotics simulation. Students will begin by familiarizing themselves with the Blender interface and its functionalities, followed by the development of inverse and forward kinematics models for the EPSON SCARA robot in a 2D plane (X, Y). The kinematics will be implemented using Python scripting, allowing for the calculation of joint angles based on input or target positions. Students will also work on modeling and rigging the EPSON SCARA robot, including the creation of constraints and relationships between robot joints. All necessary files for this part of the workshop, including the .blend file and Python script, are provided in the [Blender](https://github.com/rparak/INTEMAC_Robotics_Sim_Workshop/tree/main/Blender) folder. Finally, the models and kinematic data will be exported in FBX format for use in Unity3D.

<p align="center">
  <img src="https://github.com/rparak/INTEMAC_Robotics_Sim_Workshop//blob/main/images/Blender.png?raw=true" width="800" height="480">
</p>

**Part 2: Unity3D for Robotics Simulation**

The second part of the workshop shifts to Unity3D, where the imported FBX files from Blender will be used to set up a new project and configure the scene for robotics simulation. The physics engine in Unity3D will be applied to simulate realistic movements of the robotic arm. The Transform system in Unity3D will be used to manipulate and control the robot’s joint positions, along with the Unity Inspector and public variables for dynamic adjustments. Students will implement C# scripts for robot control, utilizing inverse kinematics to drive joint movements. Additionally, the **SmoothDampAngle** function will be used to ensure smooth and fluid rotation of the robot joints, creating realistic motion. All required scripts and models are provided in the [Unity3D](https://github.com/rparak/INTEMAC_Robotics_Sim_Workshop/tree/main/Unity3D) folder, including all necessary C# scripts and the FBX models exported from Blender.

<p align="center">
  <img src="https://github.com/rparak/INTEMAC_Robotics_Sim_Workshop//blob/main/images/Unity3D.png?raw=true" width="800" height="480">
</p>

## Contact Info
parak@intemac.cz 

moravansky@intemac.cz

## License
[MIT](https://choosealicense.com/licenses/mit/)
