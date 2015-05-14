using UnityEngine;
using UnityEditor;
using System.Collections;
/// <summary>
/// Time Manager Window - Class used to configure Time Properties in Game
/// Author: Mauricio Galvez
/// Created: May 13/15
/// Last Edited by: Mauricio Galvez
/// Last Edited Date: May 13/15
/// </summary>
public class TimeManagerWindow : EditorWindow 
{
   // =============
   // PROPERTIES
   // =============
   private static TimeController controller;                             // Instance of TimeController in Game Scene
   private static TimeFreezeController timeFreeze;                       // Instance of TimeFreezeController in GameScene
   private Vector2 CoolDownRange = new Vector2(5,10);                    // MaxCooldown value for TimeFreeze
   private Vector2 FontRange = new Vector2(12,24);                       // Max font size for TimeFreeze Debugging
   /// ===================
   /// INIT
   /// <summary>
   /// Init this instance.
   /// </summary>
   /// ===================
   [MenuItem("Time Manager/Configure... %#t")]
   static void Init()
   {      
      //-----------
      // Initialize window
      //-----------
      EditorWindow window = (TimeManagerWindow)EditorWindow.GetWindow(typeof(TimeManagerWindow));
      window.maxSize = new Vector2(500, 800);
      window.minSize = window.maxSize;
      window.Show();
      // initalize controller
      InitController();
   }   
   /// ==================
   /// INITCONTROLLER
   /// <summary>
   /// Obtains and Initializes instance of TimeController
   /// </summary>
   /// ==================
   private static void InitController()
   {     
      // Obtain Instance of Time Controller   
      controller = (TimeController)GameObject.FindObjectOfType<TimeController>();
      // check if it was not found
      if (!controller)
      {
         // Create a GameObject that contains controller
         GameObject control = new GameObject("Time Manager");
         // assign a controller
         controller = control.AddComponent<TimeController>();
         // assign a TimeStateData
         control.AddComponent<TimeStateData>();
         // display message
         EditorUtility.DisplayDialog("Time Manager Created", "Time Manager was not found, but one has been created and added to the game scene!", "Ok");
      }
      // Obtain Instance of TimeFreezeController
      timeFreeze = (TimeFreezeController)GameObject.FindObjectOfType<TimeFreezeController>();
      // check if a TFC was not found
      if(!timeFreeze)
      {
         // look for Time Manager
         GameObject tm = GameObject.Find("Time Manager");         
         // assign a TimeFreezeController
         timeFreeze = tm.AddComponent<TimeFreezeController>();
         // display message
         EditorUtility.DisplayDialog("Time Freeze Controller Created","Time Freeze Added to Time Manager!","Ok");
      }
   }
   /// =================
   /// ONGUI 
   /// <summary>
   /// Draw GUI of Window
   /// </summary>
   /// ==================
	void OnGUI()
   {
      GUILayout.Label("Time Manager Window", EditorStyles.boldLabel);
      // check if no controller is assigned
      if (!controller || !timeFreeze)
         InitController();
      //---------
      // TIME CONTROLLER PROPERTIES
      //---------
      GUILayout.Label("Time Controller Settings", EditorStyles.label);
      EditorGUI.indentLevel++;
      //controller.LerpScale = GUILayout.Toggle(controller.LerpScale,"Lerp Time Scale");
      //controller.Rate = EditorGUILayout.FloatField("Lerp Rate", controller.Rate);
      controller.NormalTime = EditorGUILayout.FloatField("Normal Time", controller.NormalTime);
      EditorGUI.indentLevel--;
      //---------
      // TIME FREEZE PROPERTIES
      //---------
      GUILayout.Label("Time Freeze Settings", EditorStyles.label);
      EditorGUI.indentLevel++;
      // obtain cooldown
      timeFreeze.CoolDown = (float)EditorGUILayout.Slider("Cool Down",timeFreeze.CoolDown,CoolDownRange.x,CoolDownRange.y);
      // obtain debugging
      timeFreeze.debugging = EditorGUILayout.Toggle("Debug Enable",timeFreeze.debugging);
      // check if debugging is enabled
      if (timeFreeze.debugging)
      {
         EditorGUI.indentLevel++;
         timeFreeze.debugStyle.fontSize = EditorGUILayout.IntSlider("Font Size", timeFreeze.debugStyle.fontSize, (int)FontRange.x, (int)FontRange.y);
         EditorGUI.indentLevel--;
      }
      EditorGUI.indentLevel--;
      //----------
      // FREEZE AND UNFREEZE BUTTONS
      //----------
      GUILayout.BeginHorizontal();
      // check if there's a timefreeze instance
      if (controller.Running)
      {
         //------
         // FREEZE
         //------
         if (GUILayout.Button("Freeze Time"))         
            timeFreeze.FreezeTime();
         //------
         // UNFREEZE
         //------
         if (GUILayout.Button("Unfreeze Time"))         
            timeFreeze.UnfreezeTime();         
      }
      GUILayout.EndHorizontal();
   }
}
