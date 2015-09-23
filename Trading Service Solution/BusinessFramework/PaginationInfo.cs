/*
 * 模块名称：分页信息。
 * 说明：用于传递和保存分页信息。
*/
using System;
using System.Diagnostics;

namespace HyBy.Trading.BusinessFramework
{

    /// <summary>
    /// 分页信息
    /// </summary>
    public sealed class PaginationInfo
    {
        private int m_PageSize;
        private int m_PageCount;
        private int m_CurrentPageIndex;
        private int m_ItemCount;
        private int m_CurrentPageBeginItemIndex;
        private int m_CurrentPageItemCount;

        /// <summary>
        /// 构造一个分页信息。
        /// </summary>
        /// <param name="pageSize">每页条数</param>
        public PaginationInfo(int pageSize)
        {
            Debug.Assert(pageSize > 0);
            m_PageSize = pageSize;
        }

        /// <summary>
        /// 界面层使用此方法设置希望获取的页码。(从1开始)
        /// </summary>
        /// <param name="pageIndex">页码</param>
        public void SetWantCurrentPageIndex(int pageIndex)
        {
            Debug.Assert(pageIndex > 0);
            m_CurrentPageIndex = pageIndex;
        }

        /// <summary>
        /// 逻辑层使用此方法设置实际条数，并计算实际的当前页码。
        /// </summary>
        /// <param name="itemCount">记录条数</param>
        public void SetItemCount(int itemCount)
        {
            Debug.Assert(itemCount >= 0);
            if (itemCount == 0)
            {
                m_ItemCount = 0;
                m_PageCount = 0;
                m_CurrentPageIndex = 0;
                m_CurrentPageBeginItemIndex = 0;
                m_CurrentPageItemCount = 0;
            }

            m_ItemCount = itemCount;
            m_PageCount = m_ItemCount / m_PageSize;
            if (m_ItemCount % m_PageSize > 0)
            {
                m_PageCount++;	//向上取整
            }
            //超出实际页数，则取最后一页。
            m_CurrentPageIndex = Math.Min(m_PageCount, m_CurrentPageIndex);
            m_CurrentPageBeginItemIndex = (m_CurrentPageIndex - 1) * m_PageSize + 1;
            //最后一页不一定达到每页条数
            m_CurrentPageItemCount = Math.Min(m_PageSize, m_ItemCount - m_CurrentPageBeginItemIndex + 1);
        }

        /// <summary>
        /// 每页条数。
        /// </summary>
        public int PageSize
        {
            get { return m_PageSize; }
        }

        /// <summary>
        /// 总页数。
        /// </summary>
        public int PageCount
        {
            get { return m_PageCount; }
        }

        /// <summary>
        /// 当前页。(从1开始)
        /// </summary>
        public int CurrentPageIndex
        {
            get { return m_CurrentPageIndex; }
        }

        /// <summary>
        /// 总条数。
        /// </summary>
        public int ItemCount
        {
            get { return m_ItemCount; }
        }

        /// <summary>
        /// 当前页起始记录号。(从1开始)
        /// </summary>
        public int CurrentPageBeginItemIndex
        {
            get { return m_CurrentPageBeginItemIndex; }
        }

        /// <summary>
        /// 当前页条数。
        /// </summary>
        public int CurrentPageItemCount
        {
            get { return m_CurrentPageItemCount; }
        }
    }
}
