using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFunctions : MonoBehaviour
{
   
  public void IncreasePlayerCharacterController()
    {
        print("TESTING PLAYER FUNCTION SCRIPT");
        print(CharacterSelector.CurrentSelectedCharacter);
        CharacterController controller = CharacterSelector.CurrentSelectedCharacter.GetComponent<CharacterController>();
        controller.radius = 0.7f;
    }

    public void DecreasePlayerCharacterController()
    {
        print("TESTING PLAYER FUNCTION SCRIPT");
        print(CharacterSelector.CurrentSelectedCharacter);
        CharacterController controller = CharacterSelector.CurrentSelectedCharacter.GetComponent<CharacterController>();
        controller.radius = 0.2f;
    }




}
