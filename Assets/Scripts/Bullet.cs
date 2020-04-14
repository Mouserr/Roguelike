using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public delegate void HitDelegate(Vector3 point, Bullet bullet);

    public HitDelegate OnHit = (p, b) => {};

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Portal")) return;
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        OnHit(pos, this);
    }
}
