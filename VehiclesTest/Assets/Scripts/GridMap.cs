using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]


public class GridMap : MonoBehaviour {

	List <GameObject> gridBlocksArray = new List<GameObject> ();
	List <GameObject> pointsInBlockArray = new List<GameObject> ();

	public GameObject ground = null;
	public GameObject spawnObject = null;
	public GameObject emptyGameObject = null;
	
	private GameObject blocks = null;
	private GameObject nestedBlock = null;

	private int mapSize = 10; 
	private int nestedBlockNumber = 16;

	
	private Mesh mesh;
	private Vector3[] vertices;
	
	private MeshFilter filter;
	//private Rigidbody meshRigidbody;
	private MeshRenderer meshRenderer;

	Color fromColor = Color.white;
	Color toColor = new Color();

	private void Awake () {
		StartCoroutine(GenerateBuildings());
	}
	void Start ()
	{
		//GenerateBuildings ();

		//toColor = new Color ((48.0f / 255.0f), (48.0f / 255.0f), (80.0f / 255.0f));
		toColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f));

	}
	void Update () 
	{
		//Debug.Log (madePoints.Count );
	}
	private IEnumerator GenerateBuildings () 
	//private void GenerateBuildings ()
	{

		WaitForSeconds wait = new WaitForSeconds(0.05f);
//
//		for (int i = 0; i < mapSize; i++) { 
//
//
//			for (int j = 0; j < mapSize; j++) {
//
//				GameObject blocks = Instantiate ( spawnType, new Vector3 (i - Mathf.Floor (mapSize / 2),0, j + Mathf.Floor (mapSize / 2)), Quaternion.Euler (new Vector3 ())) as GameObject;
//			
//				blocks.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f),Random.Range(0.0f,1.0f));
//				
//				float offset = 20.0f;
//				float xPos = ground.transform.position.x + ( i * offset) + ( spawnType.transform.localScale.x / 2 );
//				float yPos = ground.transform.position.z + ( j * offset ) + ( spawnType.transform.localScale.z / 2 );
//
//				blocks.transform.position = new Vector3 ( xPos , 0 , yPos );
//				blocks.transform.parent = ground.transform;
//
//				yield return wait;
//			}
//		}

		for (int i = 0; i < 100; i++) { 
	
			blocks = Instantiate ( spawnObject ) as GameObject;
			blocks.name = "BuiltingBlock"+i;

			float offset1 = 100.0f;
			float xPos = 50 + ( i % mapSize ) * ( offset1 );
			float zPos = 50 + ( i / mapSize ) * ( offset1 );

			blocks.transform.position = new Vector3 ( xPos-ground.GetComponent<Renderer>().bounds.size.x/2 , 0 , zPos -ground.GetComponent<Renderer>().bounds.size.z/2);

			blocks.transform.parent = ground.transform;
			gridBlocksArray.Add (blocks);


			for (int n = 0; n < nestedBlockNumber; n++) { 

				float offset2 = 16.0f;
				float xPos2 = (gridBlocksArray[i].transform.position.x + ( n % 4 ) * ( offset2 - 4));
				float zPos2 = gridBlocksArray[i].transform.position.z + ( n / 4 ) * ( offset2 - 4);
			

				nestedBlock = Instantiate ( emptyGameObject ) as GameObject;
				nestedBlock.transform.parent = gridBlocksArray[i].transform;
				nestedBlock.name = "instantiationPoint"+n;
				nestedBlock.transform.position = new Vector3 ( xPos2 - 20, 1 , zPos2-20);

				pointsInBlockArray.Add (nestedBlock);


				for (int b = 0; b < Random.Range(1,3); b++) { 
					
					GameObject buildingMesh = CreateCube();
					buildingMesh.transform.parent = blocks.transform;
					buildingMesh.name = "Building" + b;
			
					buildingMesh.transform.position = new Vector3 (nestedBlock.transform.position.x-10, 0, nestedBlock.transform.position.z-5);

				}

				
			}
			yield return wait;
		}
		
	}
	
	GameObject CreateCube ()
	{
		GameObject cube = new GameObject ();
		
		filter = cube.AddComponent< MeshFilter >();
		mesh = filter.mesh;
		mesh.Clear();
		
		float lengthX = 1f; 
		float lengthY = 1f; 
		float lengthZ = 1f;  
		
		//cube
		float additionalLength = 1f;
		
		//region Vertices
		Vector3 p0 = new Vector3( 0, 0, lengthZ*additionalLength );
		Vector3 p1 = new Vector3( lengthX, 0, lengthZ*additionalLength );
		Vector3 p2 = new Vector3( lengthX, 0, 0 );
		Vector3 p3 = new Vector3( 0, 0, 0 );	
		
		Vector3 p4 = new Vector3( 0,	lengthY,  lengthZ*additionalLength );
		Vector3 p5 = new Vector3( lengthX, 	lengthY,  lengthZ *additionalLength);
		Vector3 p6 = new Vector3( lengthX, 	lengthY,  0 );
		Vector3 p7 = new Vector3( 0,	lengthY,  0 );
		
		vertices = new Vector3[]
		{
			// Bottom
			p0, p1, p2, p3,
			
			// Left
			p7, p4, p0, p3,
			
			// Front
			p4, p5, p1, p0,
			
			// Back
			p6, p7, p3, p2,
			
			// Right
			p5, p6, p2, p1,
			
			// Top
			p7, p6, p5, p4
		};
		//endregion
		
		//		//region Normales
		Vector3 up 	= Vector3.up;
		Vector3 down 	= Vector3.down;
		Vector3 front 	= Vector3.forward;
		Vector3 back 	= Vector3.back;
		Vector3 left 	= Vector3.left;
		Vector3 right 	= Vector3.right;
		
		Vector3[] normales = new Vector3[]
		{
			// Bottom
			down, down, down, down,
			
			// Left
			left, left, left, left,
			
			// Front
			front, front, front, front,
			
			// Back
			back, back, back, back,
			
			// Right
			right, right, right, right,
			
			// Top
			up, up, up, up
		};
		//endregion	
		
		//region UVs
		//		Vector2 _00 = new Vector2( 0f, 0f );
		//		Vector2 _10 = new Vector2( 1f, 0f );
		//		Vector2 _01 = new Vector2( 0f, 1f );
		//		Vector2 _11 = new Vector2( 1f, 1f );
		
		//top and bottom
		Vector2 tbp1 = new Vector2( 0.68f, 0f);
		Vector2 tbp2 = new Vector2( 1f, 0f );
		Vector2 tbp3 = new Vector2( 1f, 1f );
		Vector2 tbp4 = new Vector2( 0.68f, 1f );
		
		//rest
		Vector2 rp1 = new Vector2( 0f, 0f );
		Vector2 rp2 = new Vector2( 0.68f, 0f );
		Vector2 rp3 = new Vector2( 0.68f, 1f );
		Vector2 rp4 = new Vector2( 0f, 1f );
		
		//		Vector2[] uvs = new Vector2[vertices.Length];
		//		
		//		for (int i=0; i < uvs.Length; i++) {
		//			uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
		//
		//		}
		
		Vector2[] uvs = new Vector2[]
		{
			// Bottom
			//_11, _01, _00, _10,
			tbp1,tbp2,tbp3,tbp4,
			
			// Left
			//_11, _01, _00, _10,
			rp1,rp2,rp3,rp4,
			
			// Front
			//_11, _01, _00, _10,
			rp1,rp2,rp3,rp4,
			
			// Back
			//_11, _01, _00, _10,
			rp1,rp2,rp3,rp4,
			
			// Right
			//_11, _01, _00, _10,
			rp1,rp2,rp3,rp4,
			
			// Top
			//_11, _01, _00, _10,
			tbp1,tbp2,tbp3,tbp4
		};
		//		//endregion
		
		//region Triangles
		int[] triangles = new int[]
		{
			// Bottom
			3, 1, 0,
			3, 2, 1,			
			
			// Left
			3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
			3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
			
			// Front
			3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
			3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
			
			// Back
			3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
			3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
			
			// Right
			3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
			3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
			
			// Top
			3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
			3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,
			
		};
		//endregion
		
		
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		mesh.normals = normales;
		meshRenderer = cube.AddComponent<MeshRenderer> ();

		Material material = new Material (Shader.Find ("Standard"));
		material.color = Color.Lerp(fromColor,toColor , Random.Range(0.0f, 1.0f));
		
		Texture texture = Resources.Load ("TextureTransparent") as Texture;

		material.mainTexture = texture;
		
		meshRenderer.material = material;
		
		MeshCollider meshCollider = cube.AddComponent<MeshCollider> ();
		meshCollider.isTrigger = false;
		
		cube.layer = 0;
		cube.tag = "Player";
		
		mesh.RecalculateBounds();
		mesh.Optimize();
		
		float rotationY = Random.Range (0, Mathf.PI * 2);
		float scaleWidth = (((Random.Range (0.0f, 1.0f) * Random.Range (0.0f, 1.0f) * Random.Range (0.0f, 1.0f) * Random.Range (0.0f, 1.0f)) * 30.0f) + 10);
		float scaleHeight = (((Random.Range (0.0f, 1.0f) * Random.Range (0.0f, 1.0f) * Random.Range (0.0f, 1.0f) * scaleWidth) * 10) + 10);

		cube.transform.Rotate (0, rotationY, 0);
		cube.transform.localScale = new Vector3 (scaleWidth, scaleHeight, scaleWidth);

		return cube as GameObject;
	}
	

}
