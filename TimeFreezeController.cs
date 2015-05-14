using UnityEngine;
using System.Collections.Generic;
using System.Collections;
/// <summary>
/// TimeFreezeController - Class in charge of controlling Time Freeze feature for game
/// Author: Mauricio Galvez
/// Created: May 14/15
/// Last Edited by: Mauricio Galvez
/// Last Edited Date: May 14/15
/// </summary>
public class TimeFreezeController : MonoBehaviour 
{
   /// ================
   /// EDITOR PROPERTIES
   /// ================
   public bool FreezeEnabled = true;                // True if time can be frozen, false otherwise.
   public float CoolDown = 3.0f;                    // Amount of seconds time freeze will be disabled after activated
   public bool debugging = true;                    // True if debugging should be active, false otherwise.
   [HideInInspector]
   public GUIStyle debugStyle = new GUIStyle();     // GUIStyle to be used for debugging
   /// ================
   /// PRIVATE PROPERTIES
   /// ================
   private float cooldownTimer;                     // Current value of timer
   private TimeStateData TimeState;                 // State of time
   private TimeController time;                     // Instance of Time controller
   /// ================
   /// START
   /// ================
	void Start () 
   {
      Initialize();
	}
   /// ================
   /// UPDATE
   /// ================

	void Update () 
   {
	   // check if time is normal and Freeze is disabled
      if(TimeState.IsNormalTime() && !FreezeEnabled)      
         updateCoolDown();      
	}
   /// ================
   /// ON GUI
   /// ================
   void OnGUI()
   {
      // check if debugging is active
      if(debugging)      
         UpdateDebug();      
   }
   /// =================
   /// FREEZE TIME
   /// <summary>
   /// Calls stop time method in time controller only if FreezeEnabled is TRUE
   /// </summary>
   /// =================
   public void FreezeTime()
   {
      // Check if Normal Time and Freeze is Enabled
      if (time && TimeState && TimeState.IsNormalTime() && FreezeEnabled)
      {
         // freeze time
         time.StopTime();
         // change time state
         TimeState.FreezeTime();      
      }
   }
   /// =================
   /// UNFREEZE TIME
   /// <summary>
   /// Calls setToNormalTime method in time controller
   /// </summary>
   /// ==================
   public void UnfreezeTime()
   {
      // check if is in Frozen Time and Freeze is Enabled
      if (time && TimeState && TimeState.IsFrozenTime() && FreezeEnabled)
      {
         // unfreezxe time
         time.SetToNormalTime();
         // disable freeze
         FreezeEnabled = false;
         // change time state
         TimeState.NormalTime();
      }
   }
   /// =================
   /// UPDATE COOLDOWN 
   /// <summary>
   /// Updates cool down timer and sets FreezeEnabled to TRUE
   /// </summary>
   /// =================
   public void updateCoolDown()
   {     
      // check if timer is zero
      if (cooldownTimer <= 0.0f)
      {
         // reset timer
         cooldownTimer = CoolDown;
         // enable FREEZE
         FreezeEnabled = true;
      }
      else
         // drecrease timer
         cooldownTimer -= Time.deltaTime;
   }
   /// ================
   /// INITIALIZE
   /// <summary>
   /// Initalizes variables of controller
   /// </summary>
   /// ================
   private void Initialize()
   {
      // obtain TimeController
      time = (TimeController)GameObject.FindObjectOfType<TimeController>();
      if (!time)
         Debug.LogError("No instance of TimeController was found in Game Scene. Make sure there's one available!");
      // obtain TimeStateData
      TimeState = (TimeStateData)GameObject.FindObjectOfType<TimeStateData>();
      if (!TimeState)
         Debug.LogError("No instance of TimeStateData was found in Game Scene. Make sure there's one available!");
      // initialize timer
      cooldownTimer = CoolDown;  
      // initialize style
      debugStyle.normal.textColor = Color.red;
      debugStyle.alignment = TextAnchor.UpperCenter;
   }
   /// =================
   /// UPDATE DEBUG
   /// <summary>
   /// Updates debug info 
   /// </summary>
   /// =================
   private void UpdateDebug()
   {  
      // Display current time state
      if (TimeState)
         GUI.TextField(new Rect(Screen.width * 0.1f, Screen.height * 0.05f, 50,50), "Time State: " + TimeState.data.ToString(), debugStyle);
      // Display current time scale
      GUI.TextField(new Rect(Screen.width * 0.5f, Screen.height * 0.05f, 50, 50), "Time Scale: " + Time.timeScale, debugStyle);
      // Display current Delta
      if(time)
         GUI.TextField(new Rect(Screen.width * 0.85f, Screen.height * 0.05f, 50, 50), "Delta: " + time.Delta.ToString(), debugStyle);    
      // Display Cooldown
      if (TimeState.IsNormalTime() && !FreezeEnabled)
         GUI.TextField(new Rect(Screen.width * 0.5f, Screen.height * 0.85f, 50, 50), "CoolDown " + cooldownTimer.ToString(),debugStyle);
   }
}
