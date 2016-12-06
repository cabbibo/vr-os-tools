
using UnityEngine;
using System.Collections.Generic;


public class HumanBuffer : MonoBehaviour {

    
    public GameObject[] Humans;
    public int numberHumans { get { return Humans.Length; } }
    public ComputeBuffer _buffer;
    private float[] inValues;

    // Use this for initialization
    void Start() {
        RebuildHumans();
    }



    void RebuildHumans() {
        // Reset size of inValues
        if (numberHumans > 0){
            inValues = new float[numberHumans * Structs.HumanStructSize];
        }else{
            inValues = new float[1 * Structs.HumanStructSize];
        }

        // Rebuild buffers
        createBuffers();
    }

    private void createBuffers() {
        if (_buffer != null)
            _buffer.Release();

        if (numberHumans > 0){
            _buffer = new ComputeBuffer(numberHumans, Structs.HumanStructSize * sizeof(float));
        }else{
            _buffer = new ComputeBuffer(1, Structs.HumanStructSize * sizeof(float));
        }
    }


  void FixedUpdate(){

	  int index = 0;
	  if (numberHumans > 0){
            //Debug.Log("ygaga");
			for(int i =0; i<numberHumans; i++){
                
			  Structs.AssignHumanStruct(inValues, index, out index, Humans[i].GetComponent<HumanInfo>().human);
			}
	  } else {
	      Structs.AssignNullStruct(inValues, index, out index, Structs.HumanStructSize);
	  }

	  _buffer.SetData(inValues);
    
 }


    // Update is called once per frame
    void Update()
    {

    }
}