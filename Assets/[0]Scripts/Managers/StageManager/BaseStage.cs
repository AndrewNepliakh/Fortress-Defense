using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageID
{
   Menu = 0,
   Level
}

public abstract class BaseStage
{
   public abstract StageID StageId { get; }

   public abstract void Load();
   public abstract void Close();

}
