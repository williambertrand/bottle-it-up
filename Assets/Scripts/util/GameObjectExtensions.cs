using UnityEngine;

namespace extensions
{
    public static class GameObjectExtensions
    {
        public static T FindComponentByTag<T>(string tag) => GameObject.FindGameObjectWithTag(tag).GetComponent<T>();
    }
}