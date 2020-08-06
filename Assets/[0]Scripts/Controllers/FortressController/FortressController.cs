using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortressController
{
   private GameObject _fortressPrefab;
   private PoolManager _poolManager;
   private Vector3 _fortressSpawnPosition = new Vector3(4.5f, -1.5f, 0.0f);

   public FortressController()
   {
      _fortressPrefab = Resources.Load<GameObject>("Prefabs/Level/Fortress/Fortress");
      _poolManager = InjectBox.Get<PoolManager>();
   }

   public Fortress SpawnFortress()
   {
      var fortress = _poolManager.GetOrCreate<Fortress>(_fortressPrefab, null, _fortressSpawnPosition);
      fortress.InitFortress();
      return fortress;
   }
}
