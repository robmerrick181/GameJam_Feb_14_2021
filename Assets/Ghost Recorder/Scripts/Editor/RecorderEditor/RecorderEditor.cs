using MyRecorder.Settings;
using System;
using UnityEditor;
using UnityEngine;

namespace MyRecorder.Editor
{
    [CustomEditor(typeof(Recorder))]
    public class RecorderEditor : UnityEditor.Editor
    {
        private Recorder Target;
        private void OnEnable()
        {
            Target = (Recorder)target;
        }
        #region Inspector
        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                //base.OnInspectorGUI();
                if (check.changed)
                {

                }
                draw();
            }
        }
        private void draw()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Recorder state");
            Target.recorderState = (RecorderState)EditorGUILayout.Popup((int)Target.recorderState, Enum.GetNames(typeof(RecorderState)));
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            Target.recorderSettings = (RecorderSettings)EditorGUILayout.ObjectField(Target.recorderSettings, typeof(RecorderSettings), true);
            EditorGUILayout.EndHorizontal();
            if (Target.recorderSettings != null && Target.recorderSettings.saveBestBenefit)
            {
                drawSaveLoadButtons();
            }
            EditorGUILayout.EndVertical();
        }
        private void drawSaveLoadButtons()
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.LabelField("Best record option(s)");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
            {
                Target.Save();
            }
            if (GUILayout.Button("Load"))
            {
                Target.Load();
            }
            if (GUILayout.Button("Play"))
            {
                Target.Play();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.HelpBox("The system load and store automatically.", MessageType.Info);
            EditorGUILayout.EndVertical();
        }

        #endregion
    }
}