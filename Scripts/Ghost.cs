using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Range(-180f, 180f)]
    public float Joint_1_Angle;
    [Range(-180f, 180f)]
    public float Joint_2_Angle;

    public GameObject Joint_1_Object;
    public GameObject Joint_2_Object;

    private Transform Joint_1_Transform;
    private Transform Joint_2_Transform;


    // Start is called before the first frame update
    void Start()
    {

        Joint_1_Angle = -90f;

        Joint_1_Transform = Joint_1_Object.transform;
        Joint_2_Transform = Joint_2_Object.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Joint_1_Transform.localRotation = Quaternion.Euler(0, 0, Joint_1_Angle);
        Joint_2_Transform.localRotation = Quaternion.Euler(0, 0, Joint_2_Angle);


    }

}
