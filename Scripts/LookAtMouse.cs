using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtMouse : MonoBehaviour
{
    float raycast_depth = 100.0f;
    LayerMask floor_layer_mask;
    string floor_layer_name = "Ground";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floor_layer_mask = LayerMask.GetMask(floor_layer_name);
    }

    // Update is called once per frame
    void Update()
    {
        P_LookAtMouse();
    }

    void P_LookAtMouse()
    {
        Vector3 mouse_position = Mouse.current.position.ReadValue();
        mouse_position.z = 0.01f;

        Ray mouse_ray = Camera.main.ScreenPointToRay(mouse_position);
        RaycastHit[] mouse_hits = Physics.RaycastAll(mouse_ray, raycast_depth, floor_layer_mask);

        if (mouse_hits.Length == 1)
        {
            Vector3 hit_postion = mouse_hits[0].point;

            Vector3 local_hit_position = transform.InverseTransformPoint(hit_postion);
            local_hit_position.y = 0;
            Vector3 look_position = transform.TransformPoint(local_hit_position);

            // Using LookAt 
            transform.LookAt(look_position, transform.up);

            // Using Quaternoin
            //Vector3 look_direction = look_position - transform.position;
            //transform.rotation = Quaternion.LookRotation(look_direction, transform.up);
        }
    }
}
