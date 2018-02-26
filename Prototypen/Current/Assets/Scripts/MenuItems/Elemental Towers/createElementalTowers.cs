using UnityEngine;
using UnityEditor;
using System.Collections;

public class createElementalTowers : MonoBehaviour
{
    [MenuItem("Elemental Domination/Create/Elemental Towers")]
    public static void createTowers()
    {
        elementalGem[] gems = Resources.FindObjectsOfTypeAll<elementalGem>();

        if (gems == null)
        {
            return;
        }

        foreach (elementalGem gem in gems)
        {
            Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/PrefabObjects/Elemental Towers/" + gem.gemName + "Tower.prefab");//"Assets/Temporary/" + t.gameObject.name + ".prefab");

            GameObject newTower = new GameObject(gem.gemName + "Tower");
            newTower.tag = gem.gemName + "Tower";

            SpriteRenderer spriteRenderer = newTower.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "tower";

            newTower.AddComponent<tower>();

            BoxCollider2D towerCollider = newTower.AddComponent<BoxCollider2D>();
            towerCollider.isTrigger = true;

            GameObject selectable = new GameObject("selectable");
            selectable.tag = "selectable";
            BoxCollider2D selectableCollider = selectable.AddComponent<BoxCollider2D>();
            selectableCollider.isTrigger = true;

            GameObject range = new GameObject("range");
            range.tag = "range";
            SpriteRenderer spriterenderer = range.AddComponent<SpriteRenderer>();
            spriterenderer.sortingLayerName = "gui";

            range.transform.SetParent(newTower.transform);
            selectable.transform.SetParent(newTower.transform);

            PrefabUtility.ReplacePrefab(newTower, prefab, ReplacePrefabOptions.ConnectToPrefab);

            DestroyImmediate(newTower);
            DestroyImmediate(selectable);
            DestroyImmediate(range);
        }
    }
}
