// #if UNITY_EDITOR
// using Code.OctavianCodeSubmodule.CollisionHandling;
// using UnityEditor;
// using UnityEngine;
//
//
// namespace Code.OctavianCodeSubmodule.Editor.EditorExtensions.CustomColliders
// {
//     [CustomEditor(typeof(CustomSphereColliderBounds))]
//     public class CustomSphereColliderBoundsEditor : UnityEditor.Editor
//     {
//         private float _scale = 1f;
//         private bool _isEditingBounds;
//         
//         private void OnSceneGUI()
//         {
//             var sphere = target as CustomSphereColliderBounds;
//             var transform = sphere.transform;
//             
//             //ButtonOnScene(sphere, transform);
//             DrawCollider(transform, sphere, Color.cyan);
//             EditBounds(transform, sphere);
//         }
//         
//         public override void OnInspectorGUI()
//         {
//             var sphere = target as CustomSphereColliderBounds;
//             
//             GUILayout.BeginHorizontal("Box");
//             if (GUILayout.Button("Edit Bounds", new[]{GUILayout.Width(120), GUILayout.Height(60)}))
//             {
//                 _isEditingBounds = !_isEditingBounds;
//             }
//             _scale = sphere.Radius;
//             GUILayout.EndHorizontal();
//             base.OnInspectorGUI();
//         }
//
//         // private void ButtonOnScene(CustomSphereColliderBounds sphere, Transform transform)
//         // {
//         //     sphere.offset = transform.InverseTransformPoint(
//         //         Handles.PositionHandle(
//         //             transform.TransformPoint(sphere.offset),
//         //             transform.rotation));
//         //     Handles.BeginGUI();
//         //     var rectMin = Camera.current.WorldToScreenPoint(
//         //         sphere.transform.position +
//         //         sphere.offset);
//         //     var rect = new Rect();
//         //     rect.xMin = rectMin.x;
//         //     rect.yMin = SceneView.currentDrawingSceneView.position.height -
//         //                 rectMin.y;
//         //     rect.width = 64;
//         //     rect.height = 18;
//         //     GUILayout.BeginArea(rect);
//         //     using (new EditorGUI.DisabledGroupScope(Application.isPlaying))
//         //     {
//         //         if (GUILayout.Button("Fire"))
//         //         {
//         //             Debug.Log("fire");
//         //         }
//         //     }
//         //
//         //     GUILayout.EndArea();
//         //     Handles.EndGUI();
//         // }
//
//         private void DrawCollider(Transform transform, CustomSphereColliderBounds sphere, Color color)
//         {
//             Handles.color = color;
//             Handles.DrawWireDisc(transform.TransformPoint(sphere.Center), transform.up, sphere.Radius);
//             Handles.DrawWireDisc(transform.TransformPoint(sphere.Center), transform.right, sphere.Radius);
//             Handles.DrawWireDisc(transform.TransformPoint(sphere.Center), transform.forward, sphere.Radius);
//         }
//
//         private void EditBounds(Transform transform, CustomSphereColliderBounds sphere)
//         {
//             if (_isEditingBounds)
//             {
//                 Handles.color = Color.red;
//
//                 EditorGUI.BeginChangeCheck();
//                 _scale = Handles.ScaleValueHandle(_scale,
//                     transform.TransformPoint(sphere.Center + transform.up * _scale), Quaternion.identity, 0.5f,
//                     Handles.CubeHandleCap, 0.1f);
//                 _scale = Handles.ScaleValueHandle(_scale,
//                     transform.TransformPoint(sphere.Center + transform.right * _scale), Quaternion.identity, 0.5f,
//                     Handles.CubeHandleCap, 0.1f);
//                 _scale = Handles.ScaleValueHandle(_scale,
//                     transform.TransformPoint(sphere.Center + transform.forward * _scale), Quaternion.identity, 0.5f,
//                     Handles.CubeHandleCap, 0.1f);
//
//                 if (EditorGUI.EndChangeCheck())
//                 {
//                     Undo.RecordObject(sphere, "Scaled");
//                     sphere.Radius = _scale;
//                 }
//             }
//         }
//
//         
//         //Draw custom gizmos function
//         [DrawGizmo(GizmoType.Pickable | GizmoType.Selected)]
//         private static void DrawGizmosSelected(CustomSphereColliderBounds launcher, GizmoType gizmoType)
//         {
//             /*var offsetPosition = launcher.transform.position + launcher.offset;
//             Handles.DrawDottedLine(launcher.transform.position, offsetPosition, 3);
//             Handles.Label(offsetPosition, "Offset");
//
//             var endPosition = offsetPosition + Vector3.forward * 5;
//
//             using (new Handles.DrawingScope(Color.yellow))
//             {
//                 Handles.DrawDottedLine(offsetPosition, endPosition, 3);
//                 Gizmos.DrawWireSphere(endPosition, 0.125f);
//                 Handles.Label(endPosition, "Estimated Position");
//             }*/
//         }
//     }
// }
// #endif
//
//
//
//
//
