using UnityEngine;
using System.Collections;

public class HumanControlScript : MonoBehaviour {

    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        anim.SetFloat("Forward_Axis", v);
        anim.SetFloat("Rotate_Axis", h);
	}
}
