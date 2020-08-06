using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour
{
   public void Release()
   {
      InjectBox.Get<PoolManager>().Release(gameObject.name, gameObject);
   }
}
