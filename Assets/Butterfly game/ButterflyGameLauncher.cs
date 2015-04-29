using UnityEngine;
using System.Collections;

public class ButterflyGameLauncher : Interactable {

    public override void Interact()
    {
        ButterflyMachine.launchGame();
    }
}
