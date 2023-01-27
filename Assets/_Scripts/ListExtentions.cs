using System.Collections.Generic;

    public static class ListExtentions
    {
        public static void AddAll<T>(this List<T> list, params T[] group)
        {
            foreach (var item in group)
            {
                list.Add(item);
            }
        }
    }
