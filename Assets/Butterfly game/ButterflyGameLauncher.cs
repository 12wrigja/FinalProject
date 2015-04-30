using UnityEngine;
using System.Collections;

public class ButterflyGameLauncher : Interactable {
	public ButterflyMachine butterflyMachine;

    public override void Interact()
    {
        butterflyMachine.beginGame();
    }
}
