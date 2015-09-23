namespace HyBy.FrameWork.DAService
{
    using HyBy.FrameWork.Common;
    using System;
    using System.Collections;
    using System.Reflection;

    public class DBMappingCollection : CollectionBase
    {
        public int Add(DBMapping value)
        {
            return base.List.Add(value);
        }

        public bool Contains(DBMapping value)
        {
            return base.List.Contains(value);
        }

        public string FindFirstBySecond(string second)
        {
            foreach (DBMapping mapping in base.List)
            {
                if (mapping.Second == second)
                {
                    return mapping.First;
                }
            }
            throw new CommonException("无法通过" + second + "找不到指定的Mapping信息！", CommonDeclare.EnumExceptionLevel.ERROR);
        }

        public string FindSecondByFirst(string first)
        {
            foreach (DBMapping mapping in base.List)
            {
                if (mapping.First == first)
                {
                    return mapping.Second;
                }
            }
            throw new CommonException("无法通过" + first + "找到指定的Mapping信息！", CommonDeclare.EnumExceptionLevel.ERROR);
        }

        public DBMapping GetByFirst(string first)
        {
            foreach (DBMapping mapping in base.List)
            {
                if (mapping.First == first)
                {
                    return mapping;
                }
            }
            throw new CommonException("无法通过" + first + "找到指定的Mapping信息！", CommonDeclare.EnumExceptionLevel.ERROR);
        }

        public DBMapping GetBySecond(string second)
        {
            foreach (DBMapping mapping in base.List)
            {
                if (mapping.Second == second)
                {
                    return mapping;
                }
            }
            throw new CommonException("无法通过" + second + "找到指定的Mapping信息！", CommonDeclare.EnumExceptionLevel.ERROR);
        }

        public int IndexOf(DBMapping value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, DBMapping value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(DBMapping value)
        {
            base.List.Remove(value);
        }

        public DBMapping this[int index]
        {
            get
            {
                return (DBMapping) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

