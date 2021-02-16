using MyRecorder.Settings;
using UnityEditor;

namespace MyRecorder.Editor
{
    [CustomEditor(typeof(RecorderSettings))]
    public class RecorderSettingsEditor : UnityEditor.Editor
    {
        private RecorderSettings Target;
        private void OnEnable()
        {
            Target = (RecorderSettings)target;
        }
        #region Inspector
        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();
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
            EditorGUILayout.LabelField("Generation rate time");
            Target.shadowGenerationRate = EditorGUILayout.FloatField(Target.shadowGenerationRate);
            EditorGUILayout.EndHorizontal();
            if (Target.recorderSettingsType == RecorderSettingsType.AfterComplete)
            {
                draw_AfterComplete();
            }
            else
            {
                draw_RealTime();
            }
            EditorGUILayout.EndVertical();
        }
        private void draw_AfterComplete()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Ghost(s) Count");
            Target.shadowsCount = EditorGUILayout.IntField(Target.shadowsCount);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            Target.saveBestBenefit = EditorGUILayout.Toggle("Use best time", Target.saveBestBenefit);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            Target.autoLoadAndStoreBestBenefit = EditorGUILayout.Toggle("Auto Save and Load best time", Target.autoLoadAndStoreBestBenefit);
            EditorGUILayout.EndHorizontal();
        }
        private void draw_RealTime()
        {
            EditorGUILayout.BeginHorizontal();
            Target.deleteTrackedPath = EditorGUILayout.Toggle("Delete tracked path", Target.deleteTrackedPath);
            EditorGUILayout.EndHorizontal();
        }
        #endregion
    }
}