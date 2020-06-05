using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    [SerializeField]
    private Animator blick;

   public void Blick()
   {
        blick.Play("EatSnake"); 
   }

    public void Died()
    {
        blick.Play("SnakeDied");
    }
}
