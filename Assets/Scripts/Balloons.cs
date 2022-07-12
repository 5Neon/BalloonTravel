using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloons : MonoBehaviour
{
    Vector3 destination;
    Vector3 speed = Vector3.zero;

    public GameObject BallonRope;

    private float rand1;
    private float rand2;

    public void Start()
    {
        destination = new Vector3(Random.Range(10f, -10f), 40f, Random.Range(10f, -10f));

        //MeshRenderer _mr = GetComponent<MeshRenderer>();
        //_mr.material.shader = Shader.Find(ToonShader);

        //Material _material = new Material(Shader.Find(ToonShader));

        //GetComponent<Renderer>().material = _material;
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        rand1 = Random.Range(700f, 1300f);
        rand2 = Random.Range(0.5f, 2.0f)
;    }

    public void Update()
    {
        if (GameManager.isGround == true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref speed, rand1 * Time.deltaTime);
            BallonRope.transform.rotation = Quaternion.Slerp(BallonRope.transform.rotation, Quaternion.Euler(-90, 0, 0), rand2 * Time.deltaTime);
        }
    }
}
