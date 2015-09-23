namespace HyBy.FrameWork.Common
{
    using System;
    using System.Collections;

    public class GroupSort : IComparer, IComparable
    {
        private string m_columnName = string.Empty;
        private int m_depth = -1;

        public GroupSort(string columnName, int depth)
        {
            this.m_columnName = columnName;
            this.m_depth = depth;
        }

        public int Compare(object x, object y)
        {
            GroupSort sort = (GroupSort) x;
            GroupSort sort2 = (GroupSort) y;
            return ((sort.Depth == sort2.Depth) ? 0 : ((sort.Depth > sort2.Depth) ? 1 : -1));
        }

        public int CompareTo(object obj)
        {
            GroupSort sort = (GroupSort) obj;
            return ((this.Depth == sort.Depth) ? 0 : ((this.Depth > sort.Depth) ? 1 : -1));
        }

        public string ColumnName
        {
            get
            {
                return this.m_columnName;
            }
            set
            {
                this.m_columnName = value;
            }
        }

        public int Depth
        {
            get
            {
                return this.m_depth;
            }
            set
            {
                this.m_depth = value;
            }
        }
    }
}

