using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        { 
            transform.LookAt(target);
        //shoebill.transform.LookAt(new Vector3(player.position.x, transform.position.y, transform.position.z));
        }
    }
}
