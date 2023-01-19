using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachBox : MonoBehaviour
{
    
    private DistanceJoint2D distanceJoint;
    private LineRenderer lineRend;
    private GameObject selected;
    private bool attached;
    
    // Start is called before the first frame update
    void Start()
    {
        distanceJoint = GetComponent<DistanceJoint2D>();
        lineRend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            GameObject[] box = GameObject.FindGameObjectsWithTag("Box");
            if(box != null)
            {
                float minDistance = Vector3.Distance(box[0].transform.position, this.transform.position);
                int selectedIndex = 0;
                if(box.Length > 1)
                {
                    for(int i = 1; i < box.Length; i++)
                    {
                        float currentDist = Vector3.Distance(box[i].transform.position, this.transform.position);
                        if(minDistance > currentDist)
                        {
                            minDistance = currentDist;
                            selectedIndex = i;
                        }
                    }
                }
                selected = box[selectedIndex];
                if(distanceJoint.connectedBody == null)
                {
                    attached = true;
                    distanceJoint.enabled = true;
                    lineRend.enabled = true;
                }
            }  
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            distanceJoint.connectedBody = null;
            //lineRend.SetPosition(0, null);
            //lineRend.SetPosition(1, null);
            distanceJoint.enabled = false;
            lineRend.enabled = false;
            attached = false;
        }

        if(Input.GetKeyDown(KeyCode.F) && attached == true)
        {
            selected.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f), Random.Range(-500.0f, 500.0f)));
        }

        if(selected == null){
            distanceJoint.enabled = false;
            lineRend.enabled = false;
            attached = false;
        } else if(attached == true){
            distanceJoint.distance = 5.0f;
            lineRend.SetPosition(0, this.transform.position);
            lineRend.SetPosition(1, selected.transform.position);
        }
    }
}
