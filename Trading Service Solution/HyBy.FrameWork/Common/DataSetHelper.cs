namespace HyBy.FrameWork.Common
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Text;

    public class DataSetHelper
    {
        private DataSet ds;

        public DataSetHelper()
        {
            this.ds = null;
        }

        public DataSetHelper(ref DataSet ds)
        {
            this.ds = null;
            this.ds = ds;
        }

        private bool ColumnEqual(object A, object B)
        {
            if ((A == DBNull.Value) && (B == DBNull.Value))
            {
                return true;
            }
            if ((A == DBNull.Value) || (B == DBNull.Value))
            {
                return false;
            }
            return A.Equals(B);
        }

        public DataView DataSort(DataTable sourceTable, GroupSort[] sort, string decimalCompareColumnName)
        {
            int num;
            Array.Sort<GroupSort>(sort);
            ArrayList list = new ArrayList();
            foreach (GroupSort sort2 in sort)
            {
                DataTable table = this.SelectDistinct(sourceTable, sort2.ColumnName);
                this.ds.Tables.Add(table);
                string name = "r" + Guid.NewGuid().ToString("N");
                DataRelation relation = this.ds.Relations.Add(name, table.Columns[sort2.ColumnName], sourceTable.Columns[sort2.ColumnName], false);
                list.Add("S" + Guid.NewGuid().ToString("N"));
                sourceTable.Columns.Add(list[list.Count - 1].ToString(), typeof(int));
                table.Columns.Add("T", typeof(decimal), "SUM(Child." + decimalCompareColumnName + ")");
                table.DefaultView.Sort = "T DESC";
                num = 0;
                while (num < table.DefaultView.Count)
                {
                    DataRow[] rowArray = sourceTable.Select(string.Concat(new object[] { sort2.ColumnName, "='", table.DefaultView[num][sort2.ColumnName], "'" }));
                    foreach (DataRow row in rowArray)
                    {
                        row[list[list.Count - 1].ToString()] = (list.Count > 1) ? (int.Parse(row[list[list.Count - 2].ToString()].ToString()) + num) : (num);
                    }
                    num++;
                }
                this.ds.Relations.Remove(relation);
                this.ds.Tables.Remove(table);
            }
            StringBuilder builder = new StringBuilder();
            for (num = 0; num < list.Count; num++)
            {
                builder.Append(list[num]);
                if ((num + 1) != list.Count)
                {
                    builder.Append(",");
                }
            }
            sourceTable.DefaultView.Sort = builder.ToString();
            return sourceTable.DefaultView;
        }

        public DataTable SelectDistinct(DataTable SourceTable, string FieldName)
        {
            DataTable table = new DataTable();
            table.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);
            object a = null;
            foreach (DataRow row in SourceTable.Select("", FieldName))
            {
                if (!((a != null) && this.ColumnEqual(a, row[FieldName])))
                {
                    a = row[FieldName];
                    table.Rows.Add(new object[] { a });
                }
            }
            return table;
        }
    }
}

