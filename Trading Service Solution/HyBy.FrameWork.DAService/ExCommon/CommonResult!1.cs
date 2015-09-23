namespace HyBy.FrameWork.DAService.ExCommon
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class CommonResult<T> : IEnumerable<T>, IEnumerable
    {
        private int _recordCount;

        public CommonResult()
        {
            this._recordCount = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Datas.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Datas.GetEnumerator();
        }

        public List<T> Datas { get; set; }

        public T this[int i]
        {
            get
            {
                return this.Datas[i];
            }
        }

        public int PageCount { get; set; }

        public int RecordCount
        {
            get
            {
                if (this.PageCount == 0)
                {
                    return this.Datas.Count;
                }
                return this._recordCount;
            }
            set
            {
                this._recordCount = value;
            }
        }

        public object Tag { get; set; }
    }
}

