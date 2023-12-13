using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField]
    private GameObject _shortHead, _tallHead;

   public void EnableShortHead()
   {
        _shortHead.SetActive(true);
        _tallHead.SetActive(false);
   }

    public void EnableTallHead()
    {
        _shortHead.SetActive(false);
        _tallHead.SetActive(true);
    }
}
