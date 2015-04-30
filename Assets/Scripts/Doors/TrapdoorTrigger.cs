using UnityEngine;
using System.Collections;

public class TrapdoorTrigger : Interactable {

    public trapdoorcontroller trapdoor;

    public override void Interact()
    {
        trapdoor.HaveFoundKey();
    }

}
