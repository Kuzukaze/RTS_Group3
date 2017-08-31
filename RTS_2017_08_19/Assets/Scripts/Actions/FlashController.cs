using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashController : MonoBehaviour
{
    public float flashSpeed;
    private Collider coll;

    void Start()
    {
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * flashSpeed * Time.deltaTime);
    }

    /* private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(coll, other.gameObject.GetComponent<Collider>(), true);
        }
        else
        {
            Destroy(gameObject, 0.1f);
        }

    }
    */
}
