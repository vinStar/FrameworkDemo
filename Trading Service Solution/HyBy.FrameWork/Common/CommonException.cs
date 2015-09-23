using System;
using System.Runtime.Serialization;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// �쳣���ࡣ
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

        private const string ExceptionMessage = "����δ֪�쳣���������Ա��ϵ��";

        private CommonDeclare.EnumExceptionLevel errorLevel = CommonDeclare.EnumExceptionLevel.FAULT;
        /// <summary>
        /// �쳣�Ĵ��󼶱�
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
        /// Ĭ�ϴ���һ��<see cref="CommonException"/>ʵ����
        /// </summary>
        public CommonException()
            : base(ExceptionMessage)
        {
        }

        /// <summary>
        /// ʹ�ô�����Ϣ����һ��<see cref="CommonException"/>ʵ����
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="level">�쳣����</param>
        public CommonException(string message, CommonDeclare.EnumExceptionLevel level)
            : base(message)
        {
            errorLevel = level;
        }

        /// <summary>
        /// ʹ�ô�����Ϣ����һ��<see cref="CommonException"/>ʵ����
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="level">�쳣����</param>
        /// <param name="errorCode">�������</param>
        public CommonException(string message, CommonDeclare.EnumExceptionLevel level, string errorCode)
            : base(message)
        {
            errorLevel = level;
            this.errorCode = errorCode;
        }

        /// <summary>
        /// ʹ��Դ�쳣����һ��<see cref="CommonException"/>ʵ����
        /// </summary>
        /// <param name="innerException">Դ�쳣����</param>
        public CommonException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }
        
        /// <summary>
        /// ʹ��Դ�쳣����һ��<see cref="CommonException"/>ʵ����
        /// </summary>
        /// <param name="innerException">Դ�쳣����</param>
        /// <param name="level">�쳣����</param>
        public CommonException(Exception innerException, CommonDeclare.EnumExceptionLevel level)
            : base(innerException.Message, innerException)
        {
            errorLevel = level;
        }

        /// <summary>
        /// ���캯��(��Դ�쳣�������Ϣ)
        /// </summary>
        /// <param name="innerException">Դ�쳣����</param>
        /// <param name="message">������Ϣ</param>
        public CommonException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// ���캯��(��Դ�쳣�������Ϣ)
        /// </summary>
        /// <param name="innerException">Դ�쳣����</param>
        /// <param name="message">������Ϣ</param>
        /// <param name="level">�쳣����</param>
        public CommonException(string message, Exception innerException, CommonDeclare.EnumExceptionLevel level)
            : base(message, innerException)
        {
            errorLevel = level;
        }

        /// <summary>
        /// ʹ�ô�����Ϣ�ʹ����뽨��һ��<see cref="CommonException"/>ʵ��
        /// ������Դ�쳣
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="innerException">Դ�쳣</param>
        /// <param name="level">���󼶱�</param>
        /// <param name="errorCode">�쳣����</param>
        public CommonException(string message, Exception innerException, CommonDeclare.EnumExceptionLevel level, string errorCode)
            : base(message, innerException)
        {
            errorLevel = level;
            this.errorCode = errorCode;
        }

        /// <summary>
        /// ʹ�ô�����Ϣ�ʹ����뽨��һ��<see cref="CommonException"/>ʵ��
        /// ������Դ�쳣        /// </summary>
        /// <param name="innerException">Դ�쳣</param>
        /// <param name="level">���󼶱�</param>
        /// <param name="errorCode">�쳣����</param>
        public CommonException(Exception innerException, CommonDeclare.EnumExceptionLevel level, string errorCode)
            : base(string.Empty, innerException)
        {
            errorLevel = level;
            this.errorCode = errorCode;
        }
        
        /// <summary>
        /// ʹ�ô�����Ϣ�ʹ����뽨��һ��<see cref="CommonException"/>ʵ��
        /// </summary>
        /// <param name="message">������Ϣ</param>
        /// <param name="errorCode">�쳣����</param>
        public CommonException(string message, string errorCode)
            : base(message)
        {
            this.errorCode = errorCode;
        }

        /// <summary>
        /// ʹ�ô�����Ϣ����һ��<see cref="CommonException"/>ʵ����
        /// </summary>
        /// <param name="message">������Ϣ</param>
        public CommonException(string message)
            : base(message)
        {
        }

        public CommonException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    } 
}


