using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private float timeCounter = 0;
    public float speed = 3f;
    public float size = 10f;

    Rigidbody rb;
    public Transform target;
    public GameObject Look;

    //public GameObject targetobj;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = target.position;

    }

    // Update is called once per frame
    void Update()
    {

        //speed = rb.AddForce(speed * Time.deltaTime);
        //rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    timeCounter -= speed * Time.deltaTime;
        //    transform.rotation = Quaternion.Euler(transform.rotation.x, Look.transform.rotation.eulerAngles.y - 90f, transform.rotation.z);

        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    timeCounter += speed * Time.deltaTime;
        //    transform.rotation = Quaternion.Euler(transform.rotation.x, Look.transform.rotation.eulerAngles.y + 90f, transform.rotation.z);

        //}

        float posx = Mathf.Cos(timeCounter) * size + target.position.x;
        float posy = 0/* + target.position.y*/;
        float posz = Mathf.Sin(timeCounter) * size + target.position.z;
        //Debug.Log(Mathf.Cos(timeCounter));
        //Debug.Log(Mathf.Sin(timeCounter));

        //this.transform.position = (targetobj.position, targetobj.transform.position.y, targetobj.transform.position.z);
        transform.position = new Vector3(posx, posy, posz);
        transform.LookAt(new Vector3(target.position.x, 0f, target.position.z));
        //cam.transform.LookAt(target);

        //transform.Rotate(Mathf.Atan2(posx, posz))
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            timeCounter -= speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(transform.rotation.x, Look.transform.rotation.eulerAngles.y - 90f, transform.rotation.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            timeCounter += speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(transform.rotation.x, Look.transform.rotation.eulerAngles.y + 90f, transform.rotation.z);
        }

        float posx = Mathf.Cos(timeCounter) * size + target.position.x;
        float posy = 0/* + target.position.y*/;
        float posz = Mathf.Sin(timeCounter) * size + target.position.z;
        //Debug.Log(Mathf.Cos(timeCounter));
        //Debug.Log(Mathf.Sin(timeCounter));

        //this.transform.position = (targetobj.position, targetobj.transform.position.y, targetobj.transform.position.z);
        transform.position = new Vector3(posx, posy, posz);
        transform.LookAt(new Vector3(target.position.x, 0f, target.position.z));
        //cam.transform.LookAt(target);

        //transform.Rotate(Mathf.Atan2(posx, posz))
    }
}
