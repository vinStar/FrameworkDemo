using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HyBy.Trading.BusinessEntity
{
    public class BusinessExceptionModel
    {
        /// <summary>
        /// 字段：CallCount 英文名称
        /// </summary>
        [DataMember()]
        public const string CALLCOUNT_FIELD = "CallCount";

        /// <summary>
        /// 字段：CallCount 中文名称
        /// </summary>
        [DataMember()]
        public const string CALLCOUNT_LABEL = "执行次数";

        /// <summary>
        /// 执行次数
        /// 最大字符长度：20
        /// 值范围：-9223372036854775808 到 9223372036854775807
        /// </summary>
        [DataMember()]
        [DisplayName("执行次数")]
        [Range(-9223372036854775808, 9223372036854775807, ErrorMessage = "执行次数应该在 “-9223372036854775808”和“9223372036854775807”之间")]
        public virtual System.Int64? CallCount
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：CallStatus 英文名称
        /// </summary>
        [DataMember()]
        public const string CALLSTATUS_FIELD = "CallStatus";

        /// <summary>
        /// 字段：CallStatus 中文名称
        /// </summary>
        [DataMember()]
        public const string CALLSTATUS_LABEL = "状态";

        /// <summary>
        /// 状态
        /// 最大字符长度：20
        /// 值范围：-9223372036854775808 到 9223372036854775807
        /// </summary>
        [DataMember()]
        [DisplayName("状态")]
        [Range(-9223372036854775808, 9223372036854775807, ErrorMessage = "状态应该在 “-9223372036854775808”和“9223372036854775807”之间")]
        public virtual System.Int64? CallStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：CreateTime 英文名称
        /// </summary>
        [DataMember()]
        public const string CREATETIME_FIELD = "CreateTime";

        /// <summary>
        /// 字段：CreateTime 中文名称
        /// </summary>
        [DataMember()]
        public const string CREATETIME_LABEL = "创建时间";

        /// <summary>
        /// 创建时间
        /// 最大字符长度：21
        /// </summary>
        [DataMember()]
        [DisplayName("创建时间")]
        [StringLength(21)]
        public virtual System.DateTime? CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：FunctionName 英文名称
        /// </summary>
        [DataMember()]
        public const string FUNCTIONNAME_FIELD = "FunctionName";

        /// <summary>
        /// 字段：FunctionName 中文名称
        /// </summary>
        [DataMember()]
        public const string FUNCTIONNAME_LABEL = "方法名";

        /// <summary>
        /// 方法名
        /// 最大字符长度：512
        /// </summary>
        [DataMember()]
        [DisplayName("方法名")]
        [StringLength(512)]
        public virtual System.String FunctionName
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：InfoCode 英文名称
        /// </summary>
        [DataMember()]
        public const string INFOCODE_FIELD = "InfoCode";

        /// <summary>
        /// 字段：InfoCode 中文名称
        /// </summary>
        [DataMember()]
        public const string INFOCODE_LABEL = "信息编码";

        /// <summary>
        /// 信息编码
        /// 最大字符长度：256
        /// </summary>
        [DataMember()]
        [DisplayName("信息编码")]
        [StringLength(256)]
        public virtual System.String InfoCode
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：InfoID 英文名称
        /// </summary>
        [DataMember()]
        public const string INFOID_FIELD = "InfoID";

        /// <summary>
        /// 字段：InfoID 中文名称
        /// </summary>
        [DataMember()]
        public const string INFOID_LABEL = "信息编号";

        /// <summary>
        /// 信息编号
        /// 最大字符长度：20
        /// 值范围：-9223372036854775808 到 9223372036854775807
        /// </summary>
        [DataMember()]
        [DisplayName("信息编号")]
        [Range(-9223372036854775808, 9223372036854775807, ErrorMessage = "信息编号应该在 “-9223372036854775808”和“9223372036854775807”之间")]
        public virtual System.Int64? InfoID
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：Memo 英文名称
        /// </summary>
        [DataMember()]
        public const string MEMO_FIELD = "ErrorMessageList";

        /// <summary>
        /// 字段：Memo 中文名称
        /// </summary>
        [DataMember()]
        public const string MEMO_LABEL = "异常消息列表";

        /// <summary>
        /// 异常消息列表
        /// </summary>
        [DataMember()]
        [DisplayName("异常消息列表")]
        [StringLength(8000)]
        public virtual List<System.String> ErrorMessageList
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：MessageCode 英文名称
        /// </summary>
        [DataMember()]
        public const string MESSAGECODE_FIELD = "MessageCode";

        /// <summary>
        /// 字段：MessageCode 中文名称
        /// </summary>
        [DataMember()]
        public const string MESSAGECODE_LABEL = "消息编码";

        /// <summary>
        /// 消息编码
        /// 最大字符长度：256
        /// </summary>
        [DataMember()]
        [DisplayName("消息编码")]
        [StringLength(256)]
        public virtual System.String MessageCode
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：NextCallTime 英文名称
        /// </summary>
        [DataMember()]
        public const string NEXTCALLTIME_FIELD = "NextCallTime";

        /// <summary>
        /// 字段：NextCallTime 中文名称
        /// </summary>
        [DataMember()]
        public const string NEXTCALLTIME_LABEL = "下次执行时间";

        /// <summary>
        /// 下次执行时间
        /// 最大字符长度：21
        /// </summary>
        [DataMember()]
        [DisplayName("下次执行时间")]
        [StringLength(21)]
        public virtual System.DateTime? NextCallTime
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：Parameters 英文名称
        /// </summary>
        [DataMember()]
        public const string PARAMETERS_FIELD = "Parameters";

        /// <summary>
        /// 字段：Parameters 中文名称
        /// </summary>
        [DataMember()]
        public const string PARAMETERS_LABEL = "参数";

        /// <summary>
        /// 参数
        /// 最大字符长度：8000
        /// </summary>
        [DataMember()]
        [DisplayName("参数")]
        [StringLength(8000)]
        public virtual object[] Parameters
        {
            get;
            set;
        }

        /// <summary>
        /// 字段：SystemID 英文名称
        /// </summary>
        [DataMember()]
        public const string SYSTEMID_FIELD = "SystemID";

        /// <summary>
        /// 字段：SystemID 中文名称
        /// </summary>
        [DataMember()]
        public const string SYSTEMID_LABEL = "所属系统";

        /// <summary>
        /// 所属系统
        /// 最大字符长度：20
        /// 值范围：-9223372036854775808 到 9223372036854775807
        /// </summary>
        [DataMember()]
        [DisplayName("所属系统")]
        [Range(-9223372036854775808, 9223372036854775807, ErrorMessage = "所属系统应该在 “-9223372036854775808”和“9223372036854775807”之间")]
        public virtual System.Int64? SystemID
        {
            get;
            set;
        }
    }
}
