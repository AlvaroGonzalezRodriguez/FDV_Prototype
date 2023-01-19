using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveFloor : MonoBehaviour
{
    public Transform floorARight;
	public Transform floorALeft;
	public Transform floorBRight;
	public Transform floorBLeft;
	public Transform floorCRight;
	public Transform floorCLeft;

	public Transform reference;

    public GameObject cameraMain;
	private bool firstRow = false;
    private bool secondRow = true;
	private bool thirdRow = false;
	private TilemapRenderer sprRend;

	private int actualNumberEnemies = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        sprRend = reference.GetComponent<TilemapRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float size = cameraMain.GetComponent<Camera>().orthographicSize;
		float spacing = reference.GetComponent<Renderer>().bounds.size.y;

		//SI ESTA EN LA SEGUNDA FILA Y BAJA EL FONDO DE ARRIBA DEL TODO AHORA ESTA ABAJO DEL TODO
		if(cameraMain.transform.position.y < floorALeft.position.y && secondRow == true){
			floorBLeft.position = floorALeft.position - Vector3.up * spacing;
			floorBRight.position = floorARight.position - Vector3.up * spacing;
			secondRow = false;
			thirdRow = true;
		//SI ESTA EN LA SEGUNDA FILA Y SUBE EL FONDO DE ABAJO DEL TODO AHORA ESTA ARRIBA DEL TODO
		} else if(cameraMain.transform.position.y > floorBLeft.position.y && secondRow == true){
			floorALeft.position = floorBLeft.position + Vector3.up * spacing;
			floorARight.position = floorBRight.position + Vector3.up * spacing;
			secondRow = false;
			firstRow = true;
		} else if(cameraMain.transform.position.y < floorBLeft.position.y && thirdRow == true){
			floorCLeft.position = floorBLeft.position - Vector3.up * spacing;
			floorCRight.position = floorBRight.position - Vector3.up * spacing;
			thirdRow = false;
			firstRow = true;
		} else if(cameraMain.transform.position.y > floorCLeft.position.y && thirdRow == true){
			floorBLeft.position = floorCLeft.position + Vector3.up * spacing;
			floorBRight.position = floorCRight.position + Vector3.up * spacing;
			thirdRow = false;
			secondRow = true;
		} else if(cameraMain.transform.position.y < floorCLeft.position.y && firstRow == true){
			floorALeft.position = floorCLeft.position - Vector3.up * spacing;
			floorARight.position = floorCRight.position - Vector3.up * spacing;
			firstRow = false;
			secondRow = true;
		} else if(cameraMain.transform.position.y > floorALeft.position.y && firstRow == true){
			floorCLeft.position = floorALeft.position + Vector3.up * spacing;
			floorCRight.position = floorARight.position + Vector3.up * spacing;
			firstRow = false;
			thirdRow = true;
		}
    }

	public void SpawnEnemies(int numberEnemies, int max)
	{
		if(actualNumberEnemies < max)
		{
			actualNumberEnemies += numberEnemies * 4;
			if(firstRow == true)
			{
				floorALeft.GetComponent<Spawner>().Generate(numberEnemies);
				floorARight.GetComponent<Spawner>().Generate(numberEnemies);
				floorCLeft.GetComponent<Spawner>().Generate(numberEnemies);
				floorCRight.GetComponent<Spawner>().Generate(numberEnemies);
			} else if(secondRow == true){
				floorALeft.GetComponent<Spawner>().Generate(numberEnemies);
				floorARight.GetComponent<Spawner>().Generate(numberEnemies);
				floorBLeft.GetComponent<Spawner>().Generate(numberEnemies);
				floorBRight.GetComponent<Spawner>().Generate(numberEnemies);
			} else {
				floorBLeft.GetComponent<Spawner>().Generate(numberEnemies);
				floorBRight.GetComponent<Spawner>().Generate(numberEnemies);
				floorCLeft.GetComponent<Spawner>().Generate(numberEnemies);
				floorCRight.GetComponent<Spawner>().Generate(numberEnemies);
			}
		}
	}

	public void LessEnemy()
	{
		actualNumberEnemies--;
	}
}
