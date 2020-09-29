﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;

namespace System.Collections.Generic
{
    public static class AbpListExtensions
    {
        public static int FindIndex<T>(this IList<T> source, Predicate<T> selector)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (selector(source[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static void AddFirst<T>(this IList<T> source, T item)
        {
            source.Insert(0,item);
        }

        public static void AddLast<T>(this IList<T> source, T item)
        {
            source.Insert(source.Count,item);
        }

        public static void InsertAfter<T>(this IList<T> source, T existingItem, T item)
        {
            var index = source.IndexOf(existingItem);
            if (index<0)
            {
                source.AddFirst(item);
                return;
            }

            source.Insert(index+1,item);
        }

        public static void InsertAfter<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            var index = source.FindIndex(selector);
            if (index<0)
            {
                source.AddFirst(item);
                return;
            }
            source.Insert(index+1,item);
        }

        public static void InsertBefore<T>(this IList<T> source, T existingItem, T item)
        {
            var index = source.IndexOf(existingItem);
            if (index<0)
            {
                source.AddLast(item);
                return;
            }
            source.Insert(index,item);
        }

        public static void InsertBefore<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            var index = source.FindIndex(selector);
            if (index<0)
            {
                source.AddLast(item);
                return;
            }
            source.Insert(index,item);
        }

        public static void ReplaceWhile<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (selector(source[i]))
                {
                    source[i] = item;
                }
            }
        }

        public static void ReplaceWhile<T>(this IList<T> source, Predicate<T> selector, Func<T, T> itemFactory)
        {
            for (int i = 0; i < source.Count; i++)
            {
                var item = source[i];
                if (selector(item))
                {
                    source[i] = itemFactory(item);
                }
            }
        }

        public static void ReplaceOne<T>(this IList<T> source, Predicate<T> selector, T item)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (selector(source[i]))
                {
                    source[i] = item;
                    return;
                }
            }
        }

        public static void ReplaceOne<T>(this IList<T> source, Predicate<T> selector, Func<T, T> itemFactory)
        {
            for (int i = 0; i < source.Count; i++)
            {
                var item = source[i];
                if (selector(item))
                {
                    source[i] = itemFactory(item);
                    return;
                }
            }
        }

        public static void ReplaceOne<T>(this IList<T> source, T item, T replaceWith)
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (Comparer<T>.Default.Compare(source[i],item)==0)
                {
                    source[i] = replaceWith;
                    return;
                }
            }
        }

        public static void MoveItem<T>(this List<T> source, Predicate<T> selector, int targetIndex)
        {
            if (!targetIndex.IsBetween(0,source.Count-1))
            {
                throw new IndexOutOfRangeException($"targetIndex should be between 0 and {source.Count-1}");
            }

            var currentIndex = source.FindIndex(0, selector);
            if (currentIndex==targetIndex)
            {
                return;
            }


            var item = source[currentIndex];
            source.RemoveAt(currentIndex);
            source.Insert(targetIndex,item);
        }

        [NotNull]
        public static T GetOrAdd<T>([NotNull] this IList<T> source, Func<T, bool> selector, Func<T> factory)
        {
            Check.NotNull(source, nameof(source));

            var item = source.FirstOrDefault(selector);

            if (item==null)
            {
                item = factory();
                source.Add(item);
            }

            return item;
        }

        public static List<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies)
        {
            var sorted=new List<T>();
            var visited=new Dictionary<T,bool>();

            foreach (var item in source)
            {
                SortByDependenciesVisit(item,getDependencies,sorted,visited);
            }

            return sorted;
        }

        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted,
            Dictionary<T, bool> visited)
        {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);
            if (alreadyVisited)
            {
                if (inProcess)
                {
                    throw new ArgumentException($"Cyclic dependency found! Item:{item}");
                }
            }
            else
            {
                visited[item] = true;
                var dependencies = getDependencies(item);
                if (dependencies!=null)
                {
                    foreach (var dependency in dependencies)
                    {
                        SortByDependenciesVisit<T>(dependency,getDependencies,sorted,visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }








    }
}
