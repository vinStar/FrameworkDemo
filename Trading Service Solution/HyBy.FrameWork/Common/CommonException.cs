using System;
using System.Runtime.Serialization;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// 异常基类。
    /// </summary>
    [Serializable]
    public class CommonException : Exception//, IXmlSerializable
    {

        #region SoapException Detail Section Element Name Constants
        //private const string CommonExceptionElement = "CommonException";
        //private const string ExceptionTypeElement = "ExceptionType";
        //private const string ErrorLevelElement = "EnumExceptionLevel";
        //private const string SerializationDataElement = "SerializationData";
        //private const string ErrorCodeElement = "ErrorCode";
        //private const string MessageElement = "Message";
        #endregion

        private const string ExceptionMessage = "发生未知异常，请与管理员联系。";

        private CommonDeclare.EnumExceptionLevel errorLevel = CommonDeclare.EnumExceptionLevel.FAULT;
        /// <summary>
        /// 异常的错误级别。
        /// </summary>
        public CommonDeclare.EnumExceptionLevel ExceptionLevel
        {
            get { return errorLevel; }
        }

        private string errorCode = "Unknown";
        public string ErrorCode
        {
            get { return errorCode; }
        }
        /// <summary>
        /// 默认创建一个<see cref="CommonException"/>实例。
        /// </summary>
        public CommonException()
            : base(ExceptionMessage)
        {
        }

        /// <summary>
        /// 使用错误信息创建一个<see cref="CommonException"/>实例。
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="level">异常级别。</param>
        public CommonException(string message, CommonDeclare.EnumExceptionLevel level)
            : base(message)
        {
            errorLevel = level;
        }

        /// <summary>
        /// 使用错误信息创建一个<see cref="CommonException"/>实例。
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="level">异常级别。</param>
        /// <param name="errorCode">错误代码</param>
        public CommonException(string message, CommonDeclare.EnumExceptionLevel level, string errorCode)
            : base(message)
        {
            errorLevel = level;
            this.errorCode = errorCode;
        }

        /// <summary>
        /// 使用源异常创建一个<see cref="CommonException"/>实例。
        /// </summary>
        /// <param name="innerException">源异常对象</param>
        public CommonException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }
        
        /// <summary>
        /// 使用源异常创建一个<see cref="CommonException"/>实例。
        /// </summary>
        /// <param name="innerException">源异常对象</param>
        /// <param name="level">异常级别。</param>
        public CommonException(Exception innerException, CommonDeclare.EnumExceptionLevel level)
            : base(innerException.Message, innerException)
        {
            errorLevel = level;
        }

        /// <summary>
        /// 构造函数(带源异常与出错信息)
        /// </summary>
        /// <param name="innerException">源异常对象</param>
        /// <param name="message">错误信息</param>
        public CommonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 构造函数(带源异常与出错信息)
        /// </summary>
        /// <param name="innerException">源异常对象</param>
        /// <param name="message">错误信息</param>
        /// <param name="level">异常级别。</param>
        public CommonException(string message, Exception innerException, CommonDeclare.EnumExceptionLevel level)
            : base(message, innerException)
        {
            errorLevel = level;
        }

        /// <summary>
        /// 使用错误信息和错误码建立一个<see cref="CommonException"/>实例
        /// 并包含源异常
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">源异常</param>
        /// <param name="level">错误级别</param>
        /// <param name="errorCode">异常编码</param>
        public CommonException(string message, Exception innerException, CommonDeclare.EnumExceptionLevel level, string errorCode)
            : base(message, innerException)
        {
            errorLevel = level;
            this.errorCode = errorCode;
        }

        /// <summary>
        /// 使用错误信息和错误码建立一个<see cref="CommonException"/>实例
        /// 并包含源异常        /// </summary>
        /// <param name="innerException">源异常</param>
        /// <param name="level">错误级别</param>
        /// <param name="errorCode">异常编码</param>
        public CommonException(Exception innerException, CommonDeclare.EnumExceptionLevel level, string errorCode)
            : base(string.Empty, innerException)
        {
            errorLevel = level;
            this.errorCode = errorCode;
        }
        
        /// <summary>
        /// 使用错误信息和错误码建立一个<see cref="CommonException"/>实例
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="errorCode">异常编码</param>
        public CommonException(string message, string errorCode)
            : base(message)
        {
            this.errorCode = errorCode;
        }

        /// <summary>
        /// 使用错误信息创建一个<see cref="CommonException"/>实例。
        /// </summary>
        /// <param name="message">错误信息</param>
        public CommonException(string message)
            : base(message)
        {
        }

        public CommonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    } 
}


