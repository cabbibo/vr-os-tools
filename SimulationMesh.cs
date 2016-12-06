using UnityEngine;
using System.Collections;

public class SimulationMesh : MonoBehaviour {


  public GameObject VertBuffer;
  public GameObject AnchorBuffer;
  public GameObject HumanBuffer;

  public ComputeShader computeShader;
  public Material material;

  private int _kernel;

  private int vertCount;
  private int size;

  private int activeMesh = 0;

  public ComputeBuffer _transBuffer;
  public float[] transValues;

	// Use this for initialization
	void Start () {

    VertBuffer.GetComponent<MeshVertBuffer>().PopulateBuffer();
    vertCount = VertBuffer.GetComponent<MeshVertBuffer>().vertexCount;

    size = VertBuffer.GetComponent<MeshVertBuffer>().SIZE;

    AnchorBuffer.GetComponent<MeshAnchorBuffer>().PopulateBuffer();
    
    transValues = new float[16];
    _transBuffer = new ComputeBuffer( 1 , 16 * sizeof(float) );   

    _kernel = computeShader.FindKernel("CSMain");

    Camera.onPostRender += Render;
    Set();

	}

  void Render(Camera c){

    material.SetPass(0);
    material.SetBuffer("buf_Points", VertBuffer.GetComponent<MeshVertBuffer>()._vertBuffer);

    Graphics.DrawProcedural(MeshTopology.Triangles, vertCount - ( vertCount %3 ));

  }

  void OnDisable(){
    Camera.onPostRender -= Render;
  }

  void Set(){
    DoPhysics( 1 );
  }


  void updateTransBuffer(){

    Matrix4x4 m = transform.localToWorldMatrix;

    for( int i = 0; i < 16; i++ ){
      int x = i % 4;
      int y = (int) Mathf.Floor(i / 4);
      transValues[i] = m[x,y];
    }

    _transBuffer.SetData(transValues);

  
  }

  void DoPhysics( int set ){

    updateTransBuffer();

    computeShader.SetInt( "_Set" , set );


    computeShader.SetBuffer( _kernel , "vertBuffer"     , VertBuffer.GetComponent<MeshVertBuffer>()._vertBuffer );
    computeShader.SetBuffer( _kernel , "anchorBuffer"   , AnchorBuffer.GetComponent<MeshAnchorBuffer>()._anchorBuffer );

    if( HumanBuffer.GetComponent<HumanBuffer>()._buffer != null ){
      computeShader.SetInt( "_NumberHumans" ,HumanBuffer.GetComponent<HumanBuffer>().numberHumans  );
      computeShader.SetBuffer( _kernel , "humanBuffer"   , HumanBuffer.GetComponent<HumanBuffer>()._buffer );
    }else{
      computeShader.SetInt( "_NumberHumans" , 0);
    }
    
    computeShader.SetBuffer( _kernel , "transBuffer"  , _transBuffer    );

    computeShader.Dispatch( _kernel, size , size , size );
    // Update is called once per frame
  }


  void FixedUpdate () {
    
    DoPhysics( 0 );

  }



}
