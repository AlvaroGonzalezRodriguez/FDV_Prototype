using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 0.005f;
    public Renderer[] sky;

    public MainMovement movement;
	
	public Transform quad;
    
    // Start is called before the first frame update
    void Start()
    {
        movement.moveParallax += moveLayers;
        foreach (var layer in sky)
        {
            Material m = layer.material;
            m.mainTexture.wrapMode = TextureWrapMode.Repeat;
        }
    }
	
	void Update()
	{
		quad.position = new Vector3(quad.position.x, this.transform.position.y, quad.position.z);
	}

    private void moveLayers(float x, float y)
    {
        int i = 0;
        foreach (var layer in sky)
        {
            Material m = layer.material;
            m.SetTextureOffset("_MainTex", m.GetTextureOffset("_MainTex") + (new Vector2(x, y) * speed / ((i + 1) * 5.0f)));
            i++;
        }
    }
}
