using UnityEditor;
using UnityEngine;

[CustomPreview(typeof(SpriteSize))]
public class SpriteSizePreview : ObjectPreview
{
    private SpriteSize _spriteSize;

    public override void Initialize(Object[] targets)
    {
        base.Initialize(targets);
        _spriteSize = (SpriteSize)target;
    }

    public override bool HasPreviewGUI()
    {
        return true;
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        GUI.Label(r, _spriteSize.ToString(), background);
    }
}
