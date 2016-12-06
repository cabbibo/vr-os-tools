using UnityEngine;
using System.Collections;

public class MirrorHuman : MonoBehaviour {

  public Transform t_Head;
  public Transform t_HandL;
  public Transform t_HandR;

  public GameObject Head;
  public GameObject HandL;
  public GameObject HandR;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    Head.transform.position = transform.TransformPoint( t_Head.position );
    Head.transform.eulerAngles = transform.TransformDirection( t_Head.eulerAngles );

    HandL.transform.position = transform.TransformPoint( t_HandL.position );
    HandL.transform.eulerAngles = transform.TransformDirection( t_HandL.eulerAngles );
   // HandL.transform.localScale = new Vector3( v1.y * .06f ,  v1.y * .06f , v1.y * .06f );

    HandR.transform.position = transform.TransformPoint( t_HandR.position );
    HandR.transform.eulerAngles = transform.TransformDirection( t_HandR.eulerAngles );

	}
}
