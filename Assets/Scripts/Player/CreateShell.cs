using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShell : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 3f;
    private BaseClass archer = new ArcherClass();

    //public void Start()
    //{
    //    //this.GetComponent<CapsuleCollider>().height = archer.Distance;
    //    //transform.position += new Vector3(0, 0, (archer.Distance - 1f) / 4f);
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("bulletttt");
    //    if (other.gameObject.tag == "Lose")
    //    {
    //        CreateBullet();
    //    }
    //}

    //private void CreateBullet()
    //{
    //    Debug.Log("bullet2");
    //    GameObject newBullet = Instantiate(bullet,
    //                        transform.position + new Vector3(0, 0, 1),
    //                        Quaternion.Euler(90, 0, 0)) as GameObject;
    //    Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
    //    bulletRB.velocity = this.transform.forward * bulletSpeed;
    //}
}
