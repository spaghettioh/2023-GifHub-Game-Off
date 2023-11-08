using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class EditorUtil
{
    public static void SelectInHierarchy<T>(this T thing, bool frame = false)
        where T : Component
    {
        Selection.objects = new Object[]
        {
            thing.gameObject,
        };

        if (frame)
        {
            FrameSelected();
        }
    }

    public static void SelectInHierarchy<T>(
        this List<T> things, bool frame = false
    ) where T : Component
    {
        Selection.objects = things.ToArray();

        if (frame)
        {
            FrameSelected();
        }
    }

    private static void FrameSelected() =>
        SceneView.lastActiveSceneView.FrameSelected(true, true);
}
