using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Collections;

namespace Volo.Abp.Core.Volo.Abp.Collections
{
    public class TypeList
    {
        
    }

    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        public int Count => _typeList.Count;

        public bool IsReadOnly => false;

        public Type this[int index]
        {
            get { return _typeList[index]; }
            set
            {
                CheckType(value);
                _typeList[index] = value;
            }
        }

        private readonly List<Type> _typeList;

        public TypeList()
        {
            _typeList=new List<Type>();
        }

        public void Add<T>() where T : TBaseType
        {
            _typeList.Add(typeof(T));
        }

        public bool TryAdd<T>() where T : TBaseType
        {
            if (Contains<T>())
            {
                return false;

            }
            Add<T>();
            return true;
        }

        public void Add(Type item)
        {
            CheckType(item);
            _typeList.Add(item);
        }

        public void Insert(int index, Type item)
        {
            CheckType(item);
            _typeList.Insert(index, item);
        }

        public int IndexOf(Type item)
        {
            return _typeList.IndexOf(item);
        }

        public bool Contains<T>() where T : TBaseType
        {
            return Contains(typeof(T));
        }

        /// <inheritdoc/>
        public bool Contains(Type item)
        {
            return _typeList.Contains(item);
        }

        public void Remove<T>() where T : TBaseType
        {
            _typeList.Remove(typeof(T));
        }

        /// <inheritdoc/>
        public bool Remove(Type item)
        {
            return _typeList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _typeList.RemoveAt(index);
        }

        public void Clear()
        {
            _typeList.Clear();
        }

        public void CopyTo(Type[] array, int arrayIndex)
        {
            _typeList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Type> GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _typeList.GetEnumerator();
        }

        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).GetTypeInfo().IsAssignableFrom(item))
            {
                throw new ArgumentException($"Given type ({item.AssemblyQualifiedName}) should be instance of {typeof(TBaseType).AssemblyQualifiedName} ", nameof(item));
            }
        }
    }
}
