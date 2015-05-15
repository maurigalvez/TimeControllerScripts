using UnityEngine;
using System.Collections;
/// <summary>
/// Time State Data - Class used to determine current state of time
/// Author: Mauricio Galvez
/// Created: May 13/15
/// Last Edited by: Mauricio Galvez
/// Last Edited Date: May 13/15
/// </summary>
public class TimeStateData : Singleton<TimeStateData>
{
   protected TimeStateData() { }  // Guarantee there's only one instance of these created.
   /// ==============
   /// PROPERTIES
   /// ==============
   public enum eTimeState
   {     
      NORMAL = 0,
      FROZEN = 1,
   }
   public eTimeState data;       // Instance of current state of the game
   /// ==============
   /// NORMAL TIME
   /// <summary>
   /// Sets Time state to NORMAL
   /// </summary>
   ///  ==============
   public void NormalTime()
   {
      data = eTimeState.NORMAL;
   }
   /// ==============
   /// IS NORMAL TIME
   /// <summary>
   /// Checks whether Time state is on NORMAL
   /// </summary>
   /// <returns>True if time is NORMAL, false otherwise.</returns>
   /// ==============
   public bool IsNormalTime()
   {
      return data == eTimeState.NORMAL;
   }
   /// ==============
   /// FREEZE TIME
   /// <summary>
   /// Sets Time state to FROZEN
   /// </summary>
   /// ==============
   public void FreezeTime()
   {
      data = eTimeState.FROZEN;
   }
   /// ===============
   /// IS FROZEN TIME
   /// <summary>
   /// Checks whether Time state is on FROZEN
   /// </summary>
   /// <returns>True if time is FROZEN, false otherwise.</returns>
   /// ===============
   public bool IsFrozenTime()
   {
      return data == eTimeState.FROZEN;
   }
}

   

