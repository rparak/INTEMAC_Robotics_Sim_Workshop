using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Joint Settings")]
    [Range(-180f, 180f)]
    [SerializeField] private float q_1_angle = -90f;
    [Range(-180f, 180f)]
    [SerializeField] private float q_2_angle = 0f;

    [Header("Joint References")]
    [SerializeField] private GameObject q_1_obj;
    [SerializeField] private GameObject q_2_obj;

    private Transform q_1_transform;
    private Transform q_2_transform;

    private void Awake()
    {
        if (q_1_obj != null)
            q_1_transform = q_1_obj.transform;
        else
            Debug.LogWarning("Joint 1 Object is not assigned.", this);

        if (q_2_obj != null)
            q_2_transform = q_2_obj.transform;
        else
            Debug.LogWarning("Joint 2 Object is not assigned.", this);
    }

    private void Update()
    {
        if (q_1_transform != null)
            q_1_transform.localRotation = Quaternion.Euler(0f, 0f, q_1_angle);

        if (q_2_transform != null)
            q_2_transform.localRotation = Quaternion.Euler(0f, 0f, q_2_angle);
    }
}
