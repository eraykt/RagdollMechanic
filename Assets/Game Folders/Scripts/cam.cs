using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    [SerializeField] Player player;

    private void Update()
    {
        Vector3 pos = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = pos;
    }
}
