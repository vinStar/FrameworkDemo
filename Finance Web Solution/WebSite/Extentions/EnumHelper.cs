using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using HyBy.FrameWork.Common;


namespace WebSite.Extentions
{
    public class EnumHelper
    {
        #region 泛型获取枚举
        /// <summary>
        /// 泛型获取枚举 User:Hyf
        /// </summary>
        /// <typeparam name="T">枚举</typeparam>
        /// <param name="selValue">选择的值</param>
        /// <returns></returns>
        public static SelectList GetEnumAsT<T>(object selValue)
        {
            List<SelectListItem> select = new List<SelectListItem>();
            foreach (int e in Enum.GetValues(typeof(T)))
            {
                Type enumT = typeof(T);
                string v = HyBy.FrameWork.Common.EnumDescriptionAttribute.GetValueText(enumT, Convert.ToInt32(e));
                string t = HyBy.FrameWork.Common.EnumDescriptionAttribute.GetDescription(enumT, Convert.ToInt32(e));
                select.Add(new SelectListItem
                {
                    Text = t,
                    Value = v
                });
            }
            return new SelectList(select, "Value", "Text", selValue);
        }
        #endregion

        #region 薪资记录状态
        /// <summary>
        /// 薪资记录状态
        /// </summary>
        public enum EnumSalaryRecord
        {
            /// <summary>
            /// 投资会计审核
            /// </summary>
            [EnumDescription(DefaultDescription = "需投资会计审核", DefaultValueText = "3")]
            InvestAccountant = 3,

            /// <summary>
            /// 集团人事审核
            /// </summary>
            [EnumDescription(DefaultDescription = "集团人事审核", DefaultValueText = "4")]
            GroupHR = 4,


            /// <summary>
            /// 集团财务审核
            /// </summary>
            [EnumDescription(DefaultDescription = "集团财务审核", DefaultValueText = "5")]
            GroupFinance = 5,

            /// <summary>
            /// 投资出纳发放
            /// </summary>
            [EnumDescription(DefaultDescription = "投资出纳发放", DefaultValueText = "6")]
            Cashier = 6,

            /// <summary>
            /// 投资会计打回
            /// </summary>
            [EnumDescription(DefaultDescription = "投资会计打回", DefaultValueText = "7")]
            InvestAccountantBack = 7,

            /// <summary>
            /// 集团人事打回
            /// </summary>
            [EnumDescription(DefaultDescription = "集团人事打回", DefaultValueText = "8")]
            GroupHRBack = 8,

            /// <summary>
            /// 集团财务打回
            /// </summary>
            [EnumDescription(DefaultDescription = "集团财务打回", DefaultValueText = "9")]
            GroupFinanceBack = 9,

        }
        #endregion

        #region 薪资状态
        /// <summary>
        /// 薪资状态1，人事主管3，投资会计，4，集团人事5，集团财务，6，投资出纳7，出纳发放完毕 
        /// </summary>
        public enum EnumSalary
        {
            /// <summary>
            /// 人事主管
            /// </summary>
            [EnumDescription(DefaultDescription = "人事主管", DefaultValueText = "1")]
            HumanAffairs = 1,

            /// <summary>
            /// 投资会计
            /// </summary>
            [EnumDescription(DefaultDescription = "投资会计", DefaultValueText = "3")]
            InvestAccountant = 3,

            /// <summary>
            /// 集团人事
            /// </summary>
            [EnumDescription(DefaultDescription = "集团人事", DefaultValueText = "4")]
            JTHumanAffairs = 4,

            /// <summary>
            /// 集团财务审核
            /// </summary>
            [EnumDescription(DefaultDescription = "集团财务", DefaultValueText = "5")]
            JTFinance = 5,

            /// <summary>
            /// 投资出纳
            /// </summary>
            [EnumDescription(DefaultDescription = "投资出纳", DefaultValueText = "6")]
            InvestCashier = 6,

            /// <summary>
            /// 出纳发放完毕
            /// </summary>
            [EnumDescription(DefaultDescription = "出纳发放完毕", DefaultValueText = "7")]
            ForInvestCashier = 7
        }
        #endregion

        #region 上缴利润状态
        /// <summary>
        /// 上缴利润状态1，待投资会计审核，2，投资会计已审核3，投资会计审核未通过4，集团财务审核5，集团财务审核未通过
        /// </summary>
        public enum EnumProfitDelivering
        {
            /// <summary>
            /// （商务、主案、工程）分红
            /// </summary>
            [EnumDescription(DefaultDescription = "（商务、主案、工程）分红", DefaultValueText = "1")]
            ShareBonus=1,

            /// <summary>
            /// 投资会计审核
            /// </summary>
            [EnumDescription(DefaultDescription = "投资会计审核", DefaultValueText = "2")]
            InvestAccountantShenHe = 2,

            /// <summary>
            /// 集团审核（商务、主案、工程）
            /// </summary>
            [EnumDescription(DefaultDescription = "集团审核（商务、主案、工程）", DefaultValueText = "3")]
            QTJTShenHe = 3,

            /// <summary>
            /// 出纳发放
            /// </summary>
            [EnumDescription(DefaultDescription = "出纳发放", DefaultValueText = "4")]
            CashierGrant = 4,

            /// <summary>
            /// 出纳发放完毕
            /// </summary>
            [EnumDescription(DefaultDescription = "出纳发放完毕", DefaultValueText = "5")]
            ForCashierGrant = 5
            ///// <summary>
            ///// 待投资会计审核
            ///// </summary>
            //[EnumDescription(DefaultDescription = "待投资会计审核", DefaultValueText = "1")]
            //InvestAccountant = 1,

            ///// <summary>
            ///// 投资会计已审核
            ///// </summary>
            //[EnumDescription(DefaultDescription = "投资会计已审核", DefaultValueText = "2")]
            //InvestPass = 2,

            ///// <summary>
            ///// 投资会计审核未通过
            ///// </summary>
            //[EnumDescription(DefaultDescription = "投资会计审核未通过", DefaultValueText = "3")]
            //InvestNoPass = 3,

            ///// <summary>
            ///// 集团财务审核
            ///// </summary>
            //[EnumDescription(DefaultDescription = "集团财务审核", DefaultValueText = "4")]
            //JTFinancenPass = 4,

            ///// <summary>
            ///// 集团财务审核未通过
            ///// </summary>
            //[EnumDescription(DefaultDescription = "集团财务审核未通过", DefaultValueText = "5")]
            //JTFinancenNoPass = 5
        }
        #endregion

        #region true / false
        /// <summary>
        /// 是否删除，true / false
        /// </summary>
        public enum EnumDefault
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 删除
            /// </summary>
            Deleted = 0

        }
        #endregion

        #region 资金账户类型
        /// <summary>
        /// 资金账户类型
        /// </summary>
        public enum EnumAccountType
        {
            /// <summary>
            /// 设计公司
            /// </summary>
            [EnumDescription(DefaultDescription = "地产公司", DefaultValueText = "1")]
            DesignAccount = 1,

            /// <summary>
            /// 地产公司
            /// </summary>
            [EnumDescription(DefaultDescription = "设计公司", DefaultValueText = "2")]
            LandedAccount = 2,

            /// <summary>
            /// 工程公司
            /// </summary>
            [EnumDescription(DefaultDescription = "工程公司", DefaultValueText = "3")]
            ProjectAccount = 3,

            /// <summary>
            /// 投资公司
            /// </summary>
            [EnumDescription(DefaultDescription = "投资公司", DefaultValueText = "4")]
            InvestAccount = 4,

            /// <summary>
            /// 项目管理
            /// </summary>
            [EnumDescription(DefaultDescription = "项目管理", DefaultValueText = "5")]
            ItemAccount = 5,

            /// <summary>
            /// 薪酬管理
            /// </summary>
            [EnumDescription(DefaultDescription = "薪酬管理", DefaultValueText = "6")]
            PaymentAccount = 6,

            /// <summary>
            /// 管理费
            /// </summary>
            [EnumDescription(DefaultDescription = "管理费", DefaultValueText = "7")]
            ManagementCostAccount = 7,

            /// <summary>
            /// 税金账户
            /// </summary>
            [EnumDescription(DefaultDescription = "税金账户", DefaultValueText = "8")]
            ExpensesTaxationAccount =8

        }
        #endregion

        #region 项目几期款
        public enum EnumReceivableDetailsPeriod
        {
            /// <summary>
            /// 一期款
            /// </summary>
            [EnumDescription(DefaultDescription = "一期款", DefaultValueText = "1")]
            PeriodOne = 1,

            /// <summary>
            /// 二期款
            /// </summary>
            [EnumDescription(DefaultDescription = "二期款", DefaultValueText = "2")]
            PeriodTwo = 2,

            /// <summary>
            /// 三期款
            /// </summary>
            [EnumDescription(DefaultDescription = "三期款", DefaultValueText = "3")]
            PeriodThree = 3,

            /// <summary>
            /// 四期款
            /// </summary>
            [EnumDescription(DefaultDescription = "四期款", DefaultValueText = "4")]
            PeriodFour = 4,

            /// <summary>
            /// 五期款
            /// </summary>
            [EnumDescription(DefaultDescription = "五期款", DefaultValueText = "5")]
            PeriodFive = 5,

            /// <summary>
            /// 六期款
            /// </summary>
            [EnumDescription(DefaultDescription = "六期款", DefaultValueText = "6")]
            PeriodSix = 6,

            /// <summary>
            /// 七期款
            /// </summary>
            [EnumDescription(DefaultDescription = "七期款", DefaultValueText = "7")]
            PeriodSeven = 7,

            /// <summary>
            /// 八期款
            /// </summary>
            [EnumDescription(DefaultDescription = "八期款", DefaultValueText = "8")]
            PeriodEight = 8,

            /// <summary>
            /// 九期款
            /// </summary>
            [EnumDescription(DefaultDescription = "九期款", DefaultValueText = "9")]
            PeriodNine =9,

            /// <summary>
            /// 十期款
            /// </summary>
            [EnumDescription(DefaultDescription = "十期款", DefaultValueText = "10")]
            PeriodTen = 10,
        }
        #endregion

        #region 旧账类型
        public enum EnumOldprojectType
        {
            /// <summary>
            /// 房租
            /// </summary>
            [EnumDescription(DefaultDescription = "房租", DefaultValueText = "1")]
            Rent = 1,

            /// <summary>
            /// 工资
            /// </summary>
            [EnumDescription(DefaultDescription = "工资", DefaultValueText = "2")]
            Salary = 2,

            /// <summary>
            /// 法务
            /// </summary>
            [EnumDescription(DefaultDescription = "法务", DefaultValueText = "3")]
            LawWorks = 3,

            /// <summary>
            /// 运营
            /// </summary>
            [EnumDescription(DefaultDescription = "运营", DefaultValueText = "4")]
            Manage = 4,

            /// <summary>
            /// 办公
            /// </summary>
            [EnumDescription(DefaultDescription = "办公", DefaultValueText = "5")]
            Work = 5,

            /// <summary>
            /// 其他
            /// </summary>
            [EnumDescription(DefaultDescription = "其他", DefaultValueText = "6")]
            Other = 6
        }
        #endregion

        #region 提成状态
        /// <summary>
        /// 提成审批状态（1，集团财务2，投资出纳3，出纳发放完毕，4，集团财务打回）
        /// </summary>
        public enum EnumCommission
        {

            /// <summary>
            /// 集团财务
            /// </summary>
            [EnumDescription(DefaultDescription = "集团财务", DefaultValueText = "1")]
            GroupFinance = 1,

            /// <summary>
            /// 投资出纳
            /// </summary>
            [EnumDescription(DefaultDescription = "投资出纳", DefaultValueText = "2")]
            InvestCashier = 2,

            /// <summary>
            /// 投资出纳发放
            /// </summary>
            [EnumDescription(DefaultDescription = "投资出纳发放", DefaultValueText = "3")]
            ForInvestCashier = 3,

            /// <summary>
            /// 集团财务打回
            /// </summary>
            [EnumDescription(DefaultDescription = "集团财务打回", DefaultValueText = "4")]
            GroupFinanceBack = 4,
        }
        #endregion

    }

}
