using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            col.transform.rotation = transform.rotation;
        }
    }
    public void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == 8 || col.gameObject.layer == 9)
        {
            col.transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
