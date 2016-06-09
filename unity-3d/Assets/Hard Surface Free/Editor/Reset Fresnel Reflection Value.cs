using UnityEngine;
using UnityEditor;
using System.Collections;

// Set Fresnel Reflection Value to 1.13 range

public class MaterialValuesCopier : ScriptableObject
{
	private static Material mat;
	private static float frezvalue;
	

    [MenuItem ("Hard Surface / Set Fresnel Reflection Value to 1.13 range")]
    static void DoRecord()
    {
    	
    	foreach (Material m in Selection.GetFiltered(typeof(Material), SelectionMode.DeepAssets))
        {
        	Undo.RegisterUndo (m, "Material Copy Change");
        	        	
        	if(m.HasProperty("_FrezPow"))
			{
        		frezvalue = m.GetFloat("_FrezPow");
				frezvalue *= 0.0009765625f;
				m.SetFloat("_FrezPow", frezvalue );
			}
		
		}
    }
 
   }
