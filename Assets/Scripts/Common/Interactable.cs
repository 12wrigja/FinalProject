using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour {

    public string interactText;

    public abstract void Interact();

}
