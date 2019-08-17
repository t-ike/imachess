using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GameObjectExtensions
{

    public static bool HasChild(this GameObject gameObject)
    {
        return 0 < gameObject.transform.childCount;
    }

    public static bool HasComponent<T>(this GameObject self) where T : Component
    {
        return self.GetComponent<T>() != null;
    }

    public static Transform FindInParents<T>(this GameObject g) where T : Component
    {
        /* 指定トランスフォーム(=引数)から親トランスフォームを再帰的に上り
         * 指定のコンポーネント(=T)をもっているか調べて
         * 持っている場合は、そのコンポーネントを
         * 持っていない場合は、Nullを戻す
         */

        // 指定トランスフォームの指定コンポーネントを取得
        object comp;
        Transform t = g.transform;
        // 親があり且つ、親が指定コンポーネントを持っていない場合
        while (t != null)
        {
            comp = t.GetComponent<T>();
            if (comp == null)
            {
                t = t.transform.parent;
            }
            else
            {
                break;
            }
            
        }
        return t;
    }
}

public static partial class TransformExtensions
{
    public static bool HasChild(this Transform transform)
    {
        return 0 < transform.childCount;
    }

    public static bool HasComponent<T>(this Transform self) where T : Component
    {
        return self.GetComponent<T>() != null;
    }

    public static Transform FindInParents<T>(this Transform t) where T : Component
    {
        /* 指定トランスフォーム(=引数)から親トランスフォームを再帰的に上り
         * 指定のコンポーネント(=T)をもっているか調べて
         * 持っている場合は、そのコンポーネントを
         * 持っていない場合は、Nullを戻す
         */

        // 指定トランスフォームの指定コンポーネントを取得
        object comp;

        // 親があり且つ、親が指定コンポーネントを持っていない場合
        while (t != null)
        {
            comp = t.GetComponent<T>();
            if (comp == null)
            {
                t = t.transform.parent;
            }
            else
            {
                break;
            }
        }

        return t;
    }
}

