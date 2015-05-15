using UnityEngine;
using System.Collections;
/// <summary>
/// Time Controller - Class in charge of Calculating and managing time scale
/// Author: Mauricio Galvez
/// Created: May 13/15
/// Last Edited by: Mauricio Galvez
/// Last Edited Date: May 15/15
/// </summary>
public class TimeController : Singleton<TimeController>
{
   protected TimeController() { }               // Guarantee that this will be a singleton!
   /// ==================
   /// EDITOR PROPERTIES
   /// ==================
   //public bool LerpScale = false;              // If true, scale will be lerped from 0 to 1 and viceversa.
   //public float Rate = 0.1f;                   // Rate At which Time will be lerped
   //public float SecBeforeFreeze;
   [HideInInspector]
   public float NormalTime = 1.0f;              // Value of Normal Time 
   [HideInInspector]
   public bool Running= false;                  // True if time is running, false otherwise.
   /// ==================
   /// TIME PROPERTIES
   /// ================== 
   public float Delta;                          // Delta Time Calculated
   /// ==================
   /// PRIVATE PROPERTIES
   /// ==================
   private float CurrentTime;                   // Current Time From Start
   private float PreviousFrameTime;             // Time from previous Frames 
   private GUIStyle textStyle;
   /// ==================
	/// AWAKE
   /// ==================
	void Awake () 
   {
      Running = true;
	   // initialize controller
      Init();      
	}
   /// ==================
   /// UPDATE
   /// ==================
   void Update()
   {
      UpdateDelta();    
   } 
   /// ==================
   /// INITAILIZE
   /// <summary>
   /// Initalize variables of Controller
   /// </summary>
   /// ==================
   private void Init()
   {
      // Set time values to ZERO
      Delta = 0;
      CurrentTime = 0;
      PreviousFrameTime = 0;
      Time.timeScale = NormalTime;
      textStyle = new GUIStyle();
      textStyle.fontSize = 35;
   }
   /// ==================
   /// UPDATE DELTA
   /// <summary>
   /// Updates value of Delta
   /// </summary>
   /// ==================
   private void UpdateDelta()
   {
      // obtain current time
      CurrentTime = Time.realtimeSinceStartup;
      // calculate Delta
      Delta = CurrentTime - PreviousFrameTime;
      // assign previous frame time
      PreviousFrameTime = CurrentTime;
   }
   /// ==================
   /// STOP TIME
   /// <summary>
   /// Sets time scale to ZERO
   /// </summary>.
   /// ==================
   public void StopTime()
   {
      Time.timeScale = 0.0f;
   }
   /// ===================
   /// SET TO NORMAL TIME
   /// <summary>
   /// Sets time scale to normal time
   /// </summary>
   /// ===================
   public void SetToNormalTime()
   {
      Time.timeScale = NormalTime;
   }
}
