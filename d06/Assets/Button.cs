using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    private bool _door;
    public GameObject laser;
    public GameObject door;
    public Transform card;
    public GameObject fan;
    public GameObject icon;
    public Material text;
    
    float doorLenght = 0.0f;
    
    void Start () {
        _door = false;
    }
    
    void Update () {
        
    }
    
    void OnTriggerStay(Collider button) {
        if (button.gameObject.tag == "Player") {
            if (Input.GetKeyDown ("e") && laser) {
                Vector3 tmp = laser.transform.position;
                tmp.x = 0;
                laser.transform.position = tmp;
            } else if (Input.GetKeyDown ("e") && door && card.childCount == 2) {
                icon.GetComponent<Renderer>().material = text;
                StartCoroutine(openDoor());
            } else if (Input.GetKeyDown ("e") && fan) {
                fan.GetComponentInChildren<ParticleSystem>().enableEmission = true;
            }
        }

    }


    void OnGUI () {
        
        GUI.color = Color.yellow;
        
        if (_door ) {
            GUILayout.BeginArea( new Rect((Screen.width - 200) / 2, (Screen.height - 200) / 2, 200, 200));
            GUILayout.Label("You must press E to open the door");
            GUILayout.EndArea();
        }
        
    }
    
    void OnTriggerEnter (Collider col) {

        _door = true;
        
    }    
    
    void OnTriggerExit (Collider col) {
        _door = false;
        
    }


    IEnumerator openDoor()
    {
        while (doorLenght < 50.0f) {
            door.transform.Translate (new Vector3 (0.0f, -Time.deltaTime * 0.2f, 0.0f));
            yield return new WaitForSeconds (0.01f);
            doorLenght += 0.1f;
        }
    }
}