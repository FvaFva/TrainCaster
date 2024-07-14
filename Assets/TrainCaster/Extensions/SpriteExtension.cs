using UnityEngine;

public static class SpriteExtension
{
    public static Vector4 GetUV (this Sprite sprite)
    {
        Texture2D texture = sprite.texture;
        Rect textureRect = sprite.textureRect;
        Vector2 textureSize = new Vector2(texture.width, texture.height);
        return new Vector4(
            textureRect.x / textureSize.x,
            textureRect.y / textureSize.y,
            textureRect.width / textureSize.x,
            textureRect.height / textureSize.y);
    }
}