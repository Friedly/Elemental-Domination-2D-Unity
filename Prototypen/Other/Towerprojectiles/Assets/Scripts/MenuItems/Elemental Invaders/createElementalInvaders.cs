using UnityEngine;
using UnityEditor;

public class createElementalInvader : MonoBehaviour
{
    [MenuItem("Elemental Domination/Create/Elemental Invader")]
    public static void createInvader()
    {
        elementalGem[] gems = Resources.FindObjectsOfTypeAll<elementalGem>();

        if (gems == null)
        {
            return;
        }

        foreach (elementalGem gem in gems)
        {
            Sprite placeholder = Resources.Load<Sprite>("Sprites/placeholder");

            Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/PrefabObjects/Elemental Invader/" + gem.gemName + "Invader.prefab");

            GameObject newGameObject = new GameObject(gem.gemName + "Invader");
            newGameObject.tag = "invader";

            SpriteRenderer spriteRenderer = newGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = placeholder;
            spriteRenderer.sortingLayerName = "invader";

            Rigidbody2D rigidbody = newGameObject.AddComponent<Rigidbody2D>();
            rigidbody.gravityScale = 0.0f;
            rigidbody.isKinematic = true;

            newGameObject.AddComponent<BoxCollider2D>();

            invader invader = newGameObject.AddComponent<invader>();
            invader.element = gem.gemName;
            invader.icon = placeholder;
            invader.elementIcon = placeholder;

            GameObject selectable = new GameObject("selectable");
            selectable.tag = "selectable";
            //selectable.AddComponent<selectable>();

            BoxCollider2D selectableCollider = selectable.AddComponent<BoxCollider2D>();
            selectableCollider.isTrigger = true;

            selectable.transform.SetParent(newGameObject.transform);

            PrefabUtility.ReplacePrefab(newGameObject, prefab, ReplacePrefabOptions.ConnectToPrefab);

            DestroyImmediate(newGameObject);
        }
    }
}
