// converted to unity with some modifications - mgear - http://unitycoder.com/blog/
// original source: http://www.xbdev.net/physics/Verlet/index.php

using UnityEngine;
using System.Collections;

	// Some helper containers
	struct Constraint2
	{
		  public int         index0;
		  public int         index1;
		  public float restLength;
	};
	
	struct Point
	{
		public Vector3 curPos;
		public Vector3 oldPos;
		public bool	unmovable;
	};

public class Verlet3 : MonoBehaviour {

	//Code Snippet for cloth simulation

	// Some defines to determine how our cloth will look
//	private int g_width          = 30;
//	private int  g_height         = 30;
//	private float g_gap                  = 0.5f;
//	private float g_disOffFloor  = 15.0f;
//	private bool g_floorCollisions = false;
	private float gravity = -9.8f*0.5f;

	private float softness = 0.01135f*0.80f;
	
	private      int               m_numPoints;
    private Point[]           m_points;
    private  int               m_numConstraints;
	private Vector3  m_offset;
	private float	m_scale;	

	//Constraint*  m_constraints;
     private Constraint2[]  m_constraints;
	

	private Mesh mesh;

	
	// Use this for initialization
	void Start () {
		
		mesh = GetComponent<MeshFilter>().mesh;
		
		//Cloth();
		Create();
	
	}
	
	// Update is called once per frame
	void Update () {
        VerletIntegrate(Time.deltaTime);
        SatisfyConstraints();
		Draw();
		
	}
	
	void Draw()
	{
			UpdateVerts();
		/*
            for (int i=0; i<m_numPoints; i++)

            {

                  // Just for debug, draw a sphere

7
			//	Debug.DrawLine ( m_points[i].curPos,  m_points[i].curPos+new Vector3 (0, 0.1f, 0), Color.green,0.05f);

            }

 */

            // Draw a wire mesh for our constraints
/*
            for (int i=0; i<m_numConstraints; i++)
            {
			Debug.DrawLine ( m_points[m_constraints[i].index0].curPos,  m_points[m_constraints[i].index1].curPos+new Vector3 (0, 0.1f, 0), Color.white,0.05f);
            }
			*/
	}
	



	  	void VerletIntegrate(float dt)
		{
			for (int i=0; i<m_numPoints; i++)
			{
				if (!m_points[i].unmovable)
				{
					Vector3 oldPos = m_points[i].oldPos;
					Vector3 curPos = m_points[i].curPos;
					Vector3 a = new Vector3(0,gravity,0);

//					curPos = 2*curPos - oldPos + a*dt*dt;
					curPos = 1.995f*curPos - 0.995f*oldPos + a*dt*dt;

					m_points[i].oldPos = m_points[i].curPos;
					m_points[i].curPos = curPos;
				}
			}
		}
	  
 
	void SatisfyConstraints()
	{
		const int numIterations = 1;

		for (int i=0; i<numIterations; i++)
		{
			for (int k=0; k< m_numConstraints; k++)
			{
				// Constraint 1 (Floor)
				for (int v=0; v<m_numPoints; v++)
				{
					if (m_points[v].curPos.y < 0.0f) m_points[v].curPos.y = 0.0f;
				}

				// Constraint 2 (Links)
				Constraint2 c = m_constraints[k];
				Vector3 p0 = m_points[c.index0].curPos;
				Vector3 p1 = m_points[c.index1].curPos;
				Vector3 delta = p1-p0;
				float len = delta.magnitude;
//				float diff = (len - c->restLength) / len;
				 float diff = (len - c.restLength) / len;
				p0+= delta*softness*diff;
				p1-= delta*softness*diff;


				if (p0.y>-110)
				{
					m_points[c.index0].curPos=p0;
					m_points[c.index1].curPos=p1;	  
				}

				if (m_points[c.index0].unmovable)
				{
					m_points[c.index0].curPos = m_points[c.index0].oldPos;
				}
				if (m_points[c.index1].unmovable)
				{
					m_points[c.index1].curPos = m_points[c.index1].oldPos;
				}
				

			}
		}
	}
	
	void UpdateVerts()
	{
//		IDirect3DVertexBuffer* vb = m_drawModel.GetVertexBuffer();
//		DrawModel::CUSTOMVERTEX *pVertex	= NULL;
//		vb->Lock(0,0, (void**)&pVertex, 0);
		Vector3[] vertices = mesh.vertices;
		for (int i=0; i<(int)m_numPoints; i++)
		{
//			pVertex[i].x = m_points[i].curPos.x;
//			pVertex[i].y = m_points[i].curPos.y;
//			pVertex[i].z = m_points[i].curPos.z;
			vertices[i] = m_points[i].curPos;
		}
		
		mesh.vertices = vertices;
		mesh.RecalculateNormals();
        mesh.RecalculateBounds();
		
//		vb->Unlock();
	}

	  
	void Create()
	{
//		m_modelData = md;
//		m_drawModel.Create(md, pDevice);

		m_offset = new Vector3(0,12,0);
		m_scale	 = 0.3f;
		
		
		// get verts count
		
        Vector3[] vertices = mesh.vertices;
		m_numPoints = vertices.Length;
		
		m_points = new Point[m_numPoints];

		m_numConstraints = (m_numPoints * m_numPoints) - m_numPoints;
		m_constraints = new Constraint2[m_numConstraints];

		for (int i=0; i<m_numPoints; i++)
		{
			//float * data = &md->v[i*3];
			//D3DXVECTOR3* pos = (D3DXVECTOR3*)data;
			Vector3 pos = vertices[i];
			m_points[i].curPos = (pos)*m_scale + m_offset;
			m_points[i].oldPos = (pos)*m_scale + m_offset;
			m_points[i].unmovable = false;
		}
		
		// create constraints
		int c = 0;
		
		for (int i=0; i<m_numPoints; i++)
		{
			for (int k=0; k<m_numPoints; k++)
			{
				if (i!=k)
				{

					float len = (m_points[i].curPos - m_points[k].curPos).magnitude;

					m_constraints[c].restLength = len;
					m_constraints[c].index0 = i;
					m_constraints[c].index1 = k;
					c++;
				}
			}
		}


	  }
	  /*
	void Create2()
	{
		m_offset = new Vector3(0,12,0);
		m_scale	 = 0.3f;
		
		
		// get verts count
		Vector3[] vertices = mesh.vertices;
		m_numPoints = vertices.Length;
		
		m_points = new Point[m_numPoints];

//		m_numConstraints = (m_numPoints * m_numPoints) - m_numPoints;
		m_numConstraints = m_numPoints*3;
		m_constraints = new Constraint2[m_numConstraints];

		for (int i=0; i<m_numPoints; i++)
		{
			Vector3 pos = vertices[i];
			m_points[i].curPos = (pos)*m_scale + m_offset;
			m_points[i].oldPos = (pos)*m_scale + m_offset;
			m_points[i].unmovable = false;
		}
		
		// create constraints
		int c = 0;
		
		for (int i=0; i<m_numPoints; i++)
		{
//			for (int k=0; k<m_numPoints; k++)
//			{

//					float len = (m_points[i].curPos - m_points[i+1].curPos).magnitude;
//					int r = (int)Random.value*m_numPoints;
//					int r = (int)Mathf.Repeat(i+1,m_numPoints-1);
					int r = (int)Random.value*m_numPoints;	
					if (i==r) r=(int)Mathf.Repeat(r+1,m_numPoints-1);
			
					float len = (m_points[i].curPos - m_points[r].curPos).magnitude;
//					float len = (m_points[i].curPos - m_points[m_numPoints-1].curPos).magnitude;
					m_constraints[c].restLength = len;
					m_constraints[c].index0 = i;
					m_constraints[c].index1 = r;
					c++;

//					r = (int)Mathf.Repeat(i-1,m_numPoints-1);
					r = (int)Random.value*m_numPoints;
					if (i==r) r=(int)Mathf.Repeat(r+1,m_numPoints-1);
			
					len = (m_points[i].curPos - m_points[r].curPos).magnitude;
					m_constraints[c].restLength = len;
					m_constraints[c].index0 = i;
					m_constraints[c].index1 = r;
			
					r = (int)Random.value*m_numPoints;
					if (i==r) r=(int)Mathf.Repeat(r+1,m_numPoints-1);
					
					len = (m_points[i].curPos - m_points[r].curPos).magnitude;
					m_constraints[c].restLength = len;
					m_constraints[c].index0 = i;
					m_constraints[c].index1 = r;

			
//					len = (m_points[i].curPos - m_points[0].curPos).magnitude;
//					m_constraints[c].restLength = len;
//					m_constraints[c].index0 = i;
//					m_constraints[c].index1 = i;
//					c++;
		
					len = (m_points[i].curPos - m_points[m_numPoints/2].curPos).magnitude;
					m_constraints[c].restLength = len;
					m_constraints[c].index0 = i;
					m_constraints[c].index1 = i;
					c++;

					len = (m_points[i].curPos - m_points[m_numPoints/3].curPos).magnitude;
					m_constraints[c].restLength = len;
					m_constraints[c].index0 = i;
					m_constraints[c].index1 = i;
					c++;

//			}

		}


	  }
*/	
}

