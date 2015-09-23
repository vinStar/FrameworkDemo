/**********************************************************************************************************************
 * 
 *  版权所有：(c)2014， 华跃博弈有限公司
 * 
 * ********************************************************************************************************************/

using System;
using System.Collections;
using System.Web;
using System.ComponentModel;
using HyBy.FrameWork.Common;

namespace HyBy.Trading.BusinessFramework
{
    /// <summary>
    /// 公用声明类
    /// <remarks>
    ///     公用的枚举、错误代码类及一些公用的常量放在此处
    /// </remarks>
    /// </summary>
    [Serializable]
    public class CommonUnit
    {
        public const int UpLoadImageSize = 1024 * 1024 * 500;

        public const int PageSize = 10;

        #region 文件上传路径配置

        /// <summary>
        /// 图片服务器地址
        /// </summary>
        public static string ImageServicePath = ConfigurationHelper.GetAppSetting("ImageServer");

        /// <summary>
        /// 资料
        /// </summary>
        public const string CMDataInfoPath = "/Upload/CMData/";

        /// <summary>
        /// 水印图片
        /// </summary>
        public const string WatermarkImagePath = "/Upload/WatermarkImage/";

        /// <summary>
        /// 专题
        /// </summary>
        public const string TopicPath = "/Topic/";

        #endregion

        #region 配置文件名称
        /// <summary>
        /// 配置文件名称
        /// </summary>
        public const string ConfigurationFileName = "CommonSystem.config";
        #endregion

        #region 任务单状态

        public enum EnumOrderStatus
        {
            [EnumDescription(DefaultDescription = "提单", DefaultValueText = "1")]
            SubmitOrder = 1,
            [EnumDescription(DefaultDescription = "准单", DefaultValueText = "2")]
            ConfirmOrder = 2,
            [EnumDescription(DefaultDescription = "发包", DefaultValueText = "3")]
            Pack = 3,
            [EnumDescription(DefaultDescription = "完工", DefaultValueText = "4")]
            Finish = 4,
            [EnumDescription(DefaultDescription = "决算", DefaultValueText = "5")]
            Settlement = 5,
        }

        #endregion

        #region 系统分类

        public enum EnumSystemCatalog
        {
            [EnumDescription(DefaultDescription = "人事系统", DefaultValueText = "RS")]
            RsSystem = 1,
            [EnumDescription(DefaultDescription = "主案系统", DefaultValueText = "ZA")]
            ZaSystem = 2,
            [EnumDescription(DefaultDescription = "商务系统", DefaultValueText = "SW")]
            SwSystem = 3,
            [EnumDescription(DefaultDescription = "工程系统", DefaultValueText = "GC")]
            GcSystem = 4,
            [EnumDescription(DefaultDescription = "财务系统", DefaultValueText = "CW")]
            CwSystem = 5,
            [EnumDescription(DefaultDescription = "集团系统", DefaultValueText = "JT")]
            JtSystem = 6
        }

        #endregion

        #region 处理状态
        public enum EnumDealStatus
        {
            /// <summary>
            /// 未处理
            /// </summary>
            [EnumDescription(DefaultDescription = "未处理", DefaultValueText = "1")]
            UnDeal = 1,
            /// <summary>
            /// 无法处理
            /// </summary>
            [EnumDescription(DefaultDescription = "无法处理", DefaultValueText = "2")]
            UnableDeal = 2,
            /// <summary>
            /// 用户取消
            /// </summary>
            [EnumDescription(DefaultDescription = "用户取消", DefaultValueText = "3")]
            CustomerCancel = 3,
            /// <summary>
            /// 处理中
            /// </summary>
            [EnumDescription(DefaultDescription = "处理中", DefaultValueText = "4")]
            Processing = 4,
            /// <summary>
            /// 处理成功
            /// </summary>
            [EnumDescription(DefaultDescription = "处理成功", DefaultValueText = "5")]
            Finished = 5,
            /// <summary>
            /// 处理失败
            /// </summary>
            [EnumDescription(DefaultDescription = "处理失败", DefaultValueText = "6")]
            DealFailed = 6
        }
        #endregion

        #region 阀值取值运算符

        public enum EnumThresholdOperator
        {
            [EnumDescription(DefaultDescription = ">", DefaultValueText = "1")]
            Greater = 1,
            [EnumDescription(DefaultDescription = ">=", DefaultValueText = "2")]
            GreaterEqual = 2,
            [EnumDescription(DefaultDescription = "<", DefaultValueText = "3")]
            Less = 3,
            [EnumDescription(DefaultDescription = "<=", DefaultValueText = "4")]
            LessEqual = 4
        }

        #endregion

        #region 城市类型

        public enum EnumCityType
        {
            [EnumDescription(DefaultDescription = "全国", DefaultValueText = "0")]
            WholeNation = 0,
            [EnumDescription(DefaultDescription = "一线城市", DefaultValueText = "1")]
            FirstLevel = 1,
            [EnumDescription(DefaultDescription = "二线城市", DefaultValueText = "2")]
            SecondLevel = 2,
            [EnumDescription(DefaultDescription = "三线城市", DefaultValueText = "3")]
            ThirdLevel = 3
        }

        #endregion

        #region 地区集合
        public enum EnumDiquList
        {
            [EnumDescription(DefaultDescription = "集团", DefaultValueText = "10")]
            GLXT,
            ///<summary>
            ///R6
            ///</summary>
            [EnumDescription(DefaultDescription = "R6", DefaultValueText = "11")]
            R6,
            ///<summary>
            ///北京
            ///</summary>
            [EnumDescription(DefaultDescription = "北京", DefaultValueText = "12")]
            BJ,
            ///<summary>
            ///南京
            ///</summary>
            [EnumDescription(DefaultDescription = "南京", DefaultValueText = "15")]
            NJ,
            ///<summary>
            ///合肥
            ///</summary>
            [EnumDescription(DefaultDescription = "合肥", DefaultValueText = "16")]
            HF,
            ///<summary>
            ///武汉
            ///</summary>
            [EnumDescription(DefaultDescription = "武汉", DefaultValueText = "18")]
            WH,
            ///<summary>
            ///天津
            ///</summary>
            [EnumDescription(DefaultDescription = "天津", DefaultValueText = "19")]
            TJ,
            ///<summary>
            ///郑州
            ///</summary>
            [EnumDescription(DefaultDescription = "郑州", DefaultValueText = "20")]
            ZZ,
            ///<summary>
            ///重庆
            ///</summary>
            [EnumDescription(DefaultDescription = "重庆", DefaultValueText = "21")]
            CQ,
            ///<summary>
            ///成都
            ///</summary>
            [EnumDescription(DefaultDescription = "成都", DefaultValueText = "22")]
            CD,
            ///<summary>
            ///西安
            ///</summary>
            [EnumDescription(DefaultDescription = "西安", DefaultValueText = "25")]
            XA,
            ///<summary>
            ///杭州
            ///</summary>
            [EnumDescription(DefaultDescription = "杭州", DefaultValueText = "26")]
            HZ,
            ///<summary>
            ///长沙
            ///</summary>
            [EnumDescription(DefaultDescription = "长沙", DefaultValueText = "28")]
            CS,
            ///<summary>
            ///青岛
            ///</summary>
            [EnumDescription(DefaultDescription = "青岛", DefaultValueText = "29")]
            QD,
            ///<summary>
            ///济南
            ///</summary>
            [EnumDescription(DefaultDescription = "济南", DefaultValueText = "30")]
            JN,
            ///<summary>
            ///石家庄
            ///</summary>
            [EnumDescription(DefaultDescription = "石家庄", DefaultValueText = "31")]
            SJZ,
            ///<summary>
            ///太原
            ///</summary>
            [EnumDescription(DefaultDescription = "太原", DefaultValueText = "32")]
            TY,
            ///<summary>
            ///苏州
            ///</summary>
            [EnumDescription(DefaultDescription = "苏州", DefaultValueText = "35")]
            SZ,
            ///<summary>
            ///宁波
            ///</summary>
            [EnumDescription(DefaultDescription = "宁波", DefaultValueText = "36")]
            NB,
            ///<summary>
            ///上海
            ///</summary>
            [EnumDescription(DefaultDescription = "上海", DefaultValueText = "38")]
            SH,
            ///<summary>
            ///大院
            ///</summary>
            [EnumDescription(DefaultDescription = "大院", DefaultValueText = "39")]
            JTDY,
            ///<summary>
            ///WNS
            ///</summary>
            [EnumDescription(DefaultDescription = "WNS", DefaultValueText = "40")]
            WNS,
            ///<summary>
            ///资金部
            ///</summary>
            [EnumDescription(DefaultDescription = "资金部", DefaultValueText = "41")]
            ZJB,
            ///<summary>
            ///设计院
            ///</summary>
            [EnumDescription(DefaultDescription = "设计院", DefaultValueText = "42")]
            SJY,
            ///<summary>
            ///材料部
            ///</summary>
            [EnumDescription(DefaultDescription = "材料部", DefaultValueText = "43")]
            CLB
        }

        #endregion

        #region 绩效支付方式

        public enum EnumKpiPayType
        {
            [EnumDescription(DefaultDescription = "即时支付", DefaultValueText = "1")]
            PayDown = 1,
            [EnumDescription(DefaultDescription = "月支付", DefaultValueText = "2")]
            MonthPay = 2
        }

        #endregion

        #region 未回访类型

        public enum EnumNotVisitFinesCatalog
        {
            [EnumDescription(DefaultDescription = "未回访", DefaultValueText = "1")]
            NotVisit = 1,
            [EnumDescription(DefaultDescription = "请假", DefaultValueText = "2")]
            Leave = 2,
            [EnumDescription(DefaultDescription = "外出谈单", DefaultValueText = "3")]
            OutForOrder = 3,
            [EnumDescription(DefaultDescription = "系统原因", DefaultValueText = "4")]
            SystemReason = 4,
            [EnumDescription(DefaultDescription = "其他原因", DefaultValueText = "5")]
            OtherReason = 5
        }

        #endregion

        #region 主案接口枚举
        /// <summary>
        /// 主案回访类型
        /// </summary>
        public enum ZA_VisitType
        {
            我公司面谈 = 1,
            甲方公司面谈 = 2,
            其他地点面谈 = 3,
            电话 = 4,
            QQ = 5,
            EMAIL = 6,
            微信 = 7,
            其他即时通讯 = 8,
            预测 = 9
        }
        /// <summary>
        /// 主案为回访类型
        /// </summary>
        public enum ZA_NotVisitType
        {
            未回访 = 1,
            请假 = 2,
            外出谈单 = 3,
            系统原因 = 4,
            其他原因 = 5
        }
        /// <summary>
        /// 主案回访回复类型
        /// </summary>
        public enum ZA_ReplyType
        {
            方案审核 = 1,
            阶段跳跃 = 2,
            普通回访 = 3,
            预算审核 = 4
        }
        /// <summary>
        /// 主案提单状态
        /// </summary>
        public enum ZA_OrderStage
        {
            量房 = 1,
            方案 = 2,
            设计合同 = 3,
            预算 = 4,
            施工合同 = 5
        }
        #endregion

        #region 工程接口枚举
        /// <summary>
        /// 工程发包类型
        /// </summary>
        public enum GC_PackType
        {
            装饰 = 1,
            消防 = 2,
            空调 = 3,
            弱电 = 4,
            其他 = 5,
            钢结构 = 6,
        }
        #endregion

        #region 资料类型

        public enum EnumCMDataCatalog
        {
            [EnumDescription(DefaultDescription = "基本资料", DefaultValueText = "1")]
            BaseInfo = 1,
            [EnumDescription(DefaultDescription = "指标资料", DefaultValueText = "2")]
            KpiInfo = 2,
            [EnumDescription(DefaultDescription = "岗位资料", DefaultValueText = "3")]
            PositionInfo = 3,
            [EnumDescription(DefaultDescription = "离职资料", DefaultValueText = "4")]
            LeaveInfo = 4,
            [EnumDescription(DefaultDescription = "公司章程资料", DefaultValueText = "5")]
            CompanyArticleInfo = 5
        }

        #endregion

        #region 水印图片位置
        public enum EnumWaterMarkPosition
        {
            [EnumDescription(DefaultDescription = "左上", DefaultValueText = "1")]
            LeftTop,
            [EnumDescription(DefaultDescription = "中上", DefaultValueText = "2")]
            CenterTop,
            [EnumDescription(DefaultDescription = "右上", DefaultValueText = "3")]
            RightTop,
            [EnumDescription(DefaultDescription = "左中", DefaultValueText = "4")]
            LeftMiddle,
            [EnumDescription(DefaultDescription = "中中", DefaultValueText = "5")]
            CenterMiddle,
            [EnumDescription(DefaultDescription = "右中", DefaultValueText = "6")]
            RightMiddle,
            [EnumDescription(DefaultDescription = "左下", DefaultValueText = "7")]
            LeftBottom,
            [EnumDescription(DefaultDescription = "中下", DefaultValueText = "8")]
            CenterBottom,
            [EnumDescription(DefaultDescription = "右下", DefaultValueText = "9")]
            RightBottom
        }
        #endregion

        #region 主案签单额类别

        public enum Za_PAmountCatalog
        {
            [EnumDescription(DefaultDescription = "合计", DefaultValueText = "1")]
            TotalAmount = 1,
            [EnumDescription(DefaultDescription = "设计", DefaultValueText = "2")]
            DesignAmount = 2,
            [EnumDescription(DefaultDescription = "施工", DefaultValueText = "3")]
            ConstructionAmount = 3
        }

        #endregion

        #region 日志类型

        public enum EnumLogType
        {
            [EnumDescription(DefaultDescription = "标准设置", DefaultValueText = "1")]
            KpiSet = 1,
            [EnumDescription(DefaultDescription = "图标管理", DefaultValueText = "2")]
            IconManage = 2,
            [EnumDescription(DefaultDescription = "权限设置", DefaultValueText = "3")]
            PrivilegeSet = 3,
            [EnumDescription(DefaultDescription = "地区设置", DefaultValueText = "4")]
            DiquSet = 4,
            [EnumDescription(DefaultDescription = "资料管理", DefaultValueText = "5")]
            DataManage = 5
        }

        #endregion

        #region 日志操作类型

        public enum EnumLogOperateType
        {
            [EnumDescription(DefaultDescription = "增加", DefaultValueText = "1")]
            Add = 1,
            [EnumDescription(DefaultDescription = "修改", DefaultValueText = "2")]
            Edit = 2,
            [EnumDescription(DefaultDescription = "删除", DefaultValueText = "3")]
            Delete = 3,
            [EnumDescription(DefaultDescription = "登录", DefaultValueText = "4")]
            Login = 4
        }

        #endregion

        #region 通用状态枚举

        public enum EnumCommonStatus
        {
            [EnumDescription(DefaultDescription = "启用", DefaultValueText = "1")]
            Enabled = 1,
            [EnumDescription(DefaultDescription = "禁用", DefaultValueText = "2")]
            Disabled = 2,
            [EnumDescription(DefaultDescription = "删除", DefaultValueText = "3")]
            Deleted = 3
        }

        #endregion

        #region 工资区间状态
        /// <summary>
        /// 工资区间状态
        /// </summary>
        public enum EnumWageStatus
        {
            /// <summary>
            /// 不符合范围
            /// </summary>
            [EnumDescription(DefaultDescription = "不符合范围", DefaultValueText = "0")]
            NotInRange = 0,
            /// <summary>
            /// 符合范围
            /// </summary>
            [EnumDescription(DefaultDescription = "符合范围", DefaultValueText = "1")]
            InRange = 1,
            /// <summary>
            /// 卡号不存在
            /// </summary>
            [EnumDescription(DefaultDescription = "卡号不存在", DefaultValueText = "2")]
            NoKaoHao = 2,
            /// <summary>
            /// 未设定薪资范围
            /// </summary>
            [EnumDescription(DefaultDescription = "未设定薪资范围", DefaultValueText = "3")]
            NotRange = 3,
            /// <summary>
            /// 程序异常
            /// </summary>
            [EnumDescription(DefaultDescription = "程序异常", DefaultValueText = "4")]
            Abnormal = 4
        }
        #endregion

        #region 主案提出发放方式枚举

        public enum EnumGiveType
        {
            [EnumDescription(DefaultDescription = "回款比例发放", DefaultValueText = "1")]
            PaymentRatio = 1,
            [EnumDescription(DefaultDescription = "达到某比例全部发放", DefaultValueText = "2")]
            ReachRatio = 2
        }

        #endregion

        #region 任务类型
        public enum EnumTaskType
        {
            [EnumDescription(DefaultDescription = "个人", DefaultValueText = "1")]
            Personal = 1,
            [EnumDescription(DefaultDescription = "小组", DefaultValueText = "2")]
            Group = 2,
            [EnumDescription(DefaultDescription = "部门", DefaultValueText = "3")]
            Department = 3
        }
        #endregion

        #region 任务单提成类型
        public enum EnumOrderCommissionType
        {
            [EnumDescription(DefaultDescription = "个人", DefaultValueText = "1")]
            Personal = 1,
            [EnumDescription(DefaultDescription = "小组", DefaultValueText = "2")]
            Group = 2,
            [EnumDescription(DefaultDescription = "部门", DefaultValueText = "3")]
            Department = 3
        }
        #endregion

        #region 任务单类型--IDS

        /// <summary>
        /// 任务单类型
        /// </summary>
        public enum EnumOrderType
        {
            [EnumDescription(DefaultDescription = "家具单", DefaultValueText = "1")]
            Furniture = 1,
            [EnumDescription(DefaultDescription = "弱电单", DefaultValueText = "2")]
            Weak = 2,
            [EnumDescription(DefaultDescription = "样板间", DefaultValueText = "3")]
            Model = 3
        }

        #endregion


        #region 任务单类型--业务系统

        /// <summary>
        /// 任务单类型
        /// </summary>
        public enum EnumTaskOrderType
        {
            [EnumDescription(DefaultDescription = "设计单", DefaultValueText = "1")]
            Design = 1,
            [EnumDescription(DefaultDescription = "施工单", DefaultValueText = "2")]
            Construction = 2,
            [EnumDescription(DefaultDescription = "增减项", DefaultValueText = "3")]
            IncDec = 3,
            [EnumDescription(DefaultDescription = "工程罚款", DefaultValueText = "4")]
            Fines = 4,
            [EnumDescription(DefaultDescription = "家具单", DefaultValueText = "5")]
            Furniture = 5,
            [EnumDescription(DefaultDescription = "弱电单", DefaultValueText = "6")]
            Weak = 6,
            [EnumDescription(DefaultDescription = "样板间", DefaultValueText = "7")]
            Model = 7
        }

        #endregion


        #region 考核结果
        public enum EnumResultType
        {
            [EnumDescription(DefaultDescription = "成功", DefaultValueText = "1")]
            Success = 1,
            [EnumDescription(DefaultDescription = "不成功", DefaultValueText = "2")]
            Failure = 2
        }
        #endregion

        #region 主案提点类型
        public enum EnumRatioType
        {
            [EnumDescription(DefaultDescription = "签单额", DefaultValueText = "1")]
            GrossVolume = 1,
            [EnumDescription(DefaultDescription = "毛利", DefaultValueText = "2")]
            GrossProfit = 2
        }
        #endregion

        #region 主案设计单业绩分成比例---分成分类
        public enum EnumDividedType
        {
            [EnumDescription(DefaultDescription = "集团", DefaultValueText = "1")]
            Group = 1,
            [EnumDescription(DefaultDescription = "地方", DefaultValueText = "2")]
            Place = 2
        }
        #endregion

        #region 施工单分配明细类型
        public enum EnumDistributionCatalog
        {
            [EnumDescription(DefaultDescription = "常规发包", DefaultValueText = "1")]
            Pack = 1,
            [EnumDescription(DefaultDescription = "增项发包", DefaultValueText = "2")]
            IncPack = 2,
            [EnumDescription(DefaultDescription = "税金", DefaultValueText = "3")]
            Taxes = 3,
            [EnumDescription(DefaultDescription = "佣金", DefaultValueText = "4")]
            OtherMoney = 4,
            [EnumDescription(DefaultDescription = "保险", DefaultValueText = "5")]
            Insurance = 5,
            [EnumDescription(DefaultDescription = "增项收益", DefaultValueText = "6")]
            IncProfit = 6,
            [EnumDescription(DefaultDescription = "工程罚款收益", DefaultValueText = "7")]
            ConstructionFineProfit = 7,
            [EnumDescription(DefaultDescription = "签单商务个人提成", DefaultValueText = "8")]
            BussinessCommission = 8,
            [EnumDescription(DefaultDescription = "签单主案个人提成", DefaultValueText = "9")]
            DesignCommission = 9,
            [EnumDescription(DefaultDescription = "签单工程监理提成", DefaultValueText = "10")]
            ConstructionSupervisorCommission = 10,
            [EnumDescription(DefaultDescription = "签单工程经理提成", DefaultValueText = "11")]
            ConstructionManagerCommission = 11,
            [EnumDescription(DefaultDescription = "增项工程监理提成", DefaultValueText = "12")]
            IncSupervisorCommission = 12,
            [EnumDescription(DefaultDescription = "工程罚款监理提成", DefaultValueText = "13")]
            ConstructionFineSupervisorCommission = 13,
            [EnumDescription(DefaultDescription = "投资利润收益", DefaultValueText = "14")]
            InvestmentProfit = 14,
            [EnumDescription(DefaultDescription = "抹零", DefaultValueText = "15")]
            Maling = 15,
            [EnumDescription(DefaultDescription = "签单收益", DefaultValueText = "16")]
            SignProfit = 16,
            [EnumDescription(DefaultDescription = "代收", DefaultValueText = "17")]
            Collection = 17,
            [EnumDescription(DefaultDescription = "增项工程经理提成", DefaultValueText = "18")]
            IncManagerCommission = 18,
            [EnumDescription(DefaultDescription = "工程罚款经理提成", DefaultValueText = "19")]
            ConstructionFineCommission = 19,
            [EnumDescription(DefaultDescription = "质保金", DefaultValueText = "20")]
            GuaranteeMoney = 20,
            [EnumDescription(DefaultDescription = "工程溢价收益", DefaultValueText = "21")]
            EstimatePackAmount = 21,
            [EnumDescription(DefaultDescription = "分项发包", DefaultValueText = "22")]
            SubitemPack = 22,
            [EnumDescription(DefaultDescription = "减项", DefaultValueText = "23")]
            DecItem = 23,
        }
        #endregion

        #region 设计单分配明细类型
        public enum EnumDesignDistributionCatalog
        {
         
            [EnumDescription(DefaultDescription = "税金", DefaultValueText = "3")]
            Taxes = 3,
            [EnumDescription(DefaultDescription = "佣金", DefaultValueText = "4")]
            OtherMoney = 4,
            [EnumDescription(DefaultDescription = "签单商务个人提成", DefaultValueText = "8")]
            BussinessCommission = 8,
            [EnumDescription(DefaultDescription = "签单主案个人提成", DefaultValueText = "9")]
            DesignCommission = 9,
            [EnumDescription(DefaultDescription = "投资利润收益", DefaultValueText = "14")]
            InvestmentProfit = 14,
            [EnumDescription(DefaultDescription = "签单收益", DefaultValueText = "16")]
            SignProfit = 16,
        }
        #endregion

        #region 投资账户类型
        public enum EnumAccountInfo
        {
            今日 = 1,
            主案 = 2,
            工程 = 3,
            项目成本 = 4,
            投资公司 = 5,
            薪酬 = 6,
            管理费 = 7
        }
        #endregion

        #region 财务批工程款罚款类型
        public enum EnumCWGiveFineType
        {
            人工 = 1,
            材料 = 2,
            其他 = 3
        }
        #endregion
    }
}
