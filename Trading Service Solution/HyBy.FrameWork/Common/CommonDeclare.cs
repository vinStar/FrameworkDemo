using System;
using System.Collections;
using System.Web;
using System.ComponentModel;

namespace HyBy.FrameWork.Common
{
    /// <summary>
    /// ��Ȩ����: ��Ȩ����(C) 2013����Ծ����
    /// ����ժҪ: ���õ�ö�١�����������
    /// ������ڣ�2014-5-16
    /// ��    ����V1.0
    /// ��    �ߣ���˧��
    /// </summary>
    [Serializable]
    public class CommonDeclare
    {
        #region ��־��������
        /// <summary>
        /// ��־��������
        /// </summary>
        public enum EnumLogOperateCatalog
        {
            /// <summary>
            /// ���
            /// </summary>
            [EnumDescription(DefaultDescription = "���", DefaultValueText = "1")]
            Add,
            /// <summary>
            /// ɾ��
            /// </summary>
            [EnumDescription(DefaultDescription = "ɾ��", DefaultValueText = "2")]
            Delete,
            /// <summary>
            /// �޸�
            /// </summary>
            [EnumDescription(DefaultDescription = "�޸�", DefaultValueText = "3")]
            Modify,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "4")]
            Find
        }
        #endregion

        #region �·�
        public enum Month
        {
            /// <summary>
            /// һ��
            /// </summary>
            [EnumDescription(DefaultDescription = "һ��", DefaultValueText = "1")]
            Jan,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "2")]
            Feb,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "3")]
            Mar,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "4")]
            Apr,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "5")]
            May,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "6")]
            Jun,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "7")]
            Jul,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "8")]
            Aug,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "9")]
            Sep,
            /// <summary>
            /// ʮ��
            /// </summary>
            [EnumDescription(DefaultDescription = "ʮ��", DefaultValueText = "10")]
            Oct,
            /// <summary>
            /// ʮһ��
            /// </summary>
            [EnumDescription(DefaultDescription = "ʮһ��", DefaultValueText = "11")]
            Nov,
            /// <summary>
            /// ʮ����
            /// </summary>
            [EnumDescription(DefaultDescription = "ʮ����", DefaultValueText = "12")]
            Dec
        }
        #endregion

        #region �쳣����
        /// <summary>
        /// �쳣����
        /// </summary>
        [Serializable]
        public enum EnumExceptionLevel
        {
            /// <summary>
            /// δ֪
            /// </summary>
            UNKNOWN = 0,
            /// <summary>
            /// �����Դ�����NCache/���ݿ�ҵ���Ӱ�쵽�����û�
            /// </summary>
            FAULT = 1,
            /// <summary>
            /// ���ش����д�����������ֻӰ�쵽�������û������������û���Ӱ��
            /// </summary>
            ERROR = 2,
            /// <summary>
            /// һ���Դ����д����������ǿɻָ����û����Բ�������
            /// </summary>
            WARNING = 3,
            /// <summary>
            /// һ������Ϣ���д����������ǲ�Ӱ���û�ʹ��
            /// </summary>
            INFO = 4,
            /// <summary>
            /// ������Ϣ��û�д�������ֻ�Ǽ��������б����ִ��·��
            /// </summary>
            DEBUG = 99
        }

        #endregion

        #region �Ա�
        /// <summary>
        /// �Ա�
        /// </summary>
        public enum EnumGender
        {
            /// <summary>
            /// ��
            /// </summary>
            [EnumDescription(DefaultDescription = "��", DefaultValueText = "1")]
            Male,
            /// <summary>
            /// Ů
            /// </summary>
            [EnumDescription(DefaultDescription = "Ů", DefaultValueText = "2")]
            Female,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "3")]
            Unknown
        }
        #endregion

        #region �����ļ�����
        /// <summary>
        /// �����ļ�����
        /// </summary>
        public const string ConfigurationFileName = "CommonSystem.config";
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// <remarks>
        ///     �������ͷ�Ϊ���ࣺ
        ///     �༭״̬�ͷǱ༭״̬
        /// </remarks>
        /// </summary>
        public enum CommonEditType
        {
            Unknown,         ///δ֪����
            Editing,        ///�༭״̬
            NonEditing,      ///�Ǳ༭״̬
            NonAdding,          ///����Ҫ���
            NonDeleting     ///����Ҫɾ��
        }
        #endregion

        #region ����ʾ��ʽö��
        public enum CommonColumnShowStyle
        {
            /// <summary>
            /// δ֪
            /// </summary>
            Unknown,
            /// <summary>
            /// ��ʾ����ͨ״̬
            /// </summary>    
            ShowInNormal,
            /// <summary>
            /// ��ʾ��Webҳ��
            /// </summary>
            ShowInWeb,
            /// <summary>
            /// ��ʾ���û��ͻ���
            /// </summary>
            ShowInUserClient,
            /// <summary>
            /// ��ʾ�ڱ�Я�豸
            /// </summary>
            ShowInMobile
        }
        #endregion

        #region ֤������
        public enum EnumCredentType
        {

            /// <summary>
            /// ���֤
            /// </summary>
            [EnumDescription(DefaultDescription = "���֤", DefaultValueText = "1")]
            IDCard,
            /// <summary>
            /// ǩ֤
            /// </summary>
            [EnumDescription(DefaultDescription = "�۰�ͨ��֤", DefaultValueText = "2")]
            Visa,
            /// <summary>
            /// ����
            /// </summary>
            [EnumDescription(DefaultDescription = "����", DefaultValueText = "3")]
            Passport,
            /// <summary>
            /// δ֪
            /// </summary>
            [EnumDescription(DefaultDescription = "δ֪", DefaultValueText = "4")]
            Unknown
        }
        #endregion

        #region �������
        public enum EnumDurationType
        {
            /// <summary>
            /// δ֪
            /// </summary>
            [EnumDescription(DefaultDescription = "δ֪", DefaultValueText = "0")]
            Unknown,
            /// <summary>
            /// Сʱ
            /// </summary>
            [EnumDescription(DefaultDescription = "Сʱ", DefaultValueText = "1")]
            Hour,
            /// <summary>
            /// ��
            /// </summary>
            [EnumDescription(DefaultDescription = "��", DefaultValueText = "2")]
            Day,
            /// <summary>
            /// ��
            /// </summary>
            [EnumDescription(DefaultDescription = "��", DefaultValueText = "3")]
            Week,
            /// <summary>
            /// ��
            /// </summary>
            [EnumDescription(DefaultDescription = "��", DefaultValueText = "4")]
            Month,
            /// <summary>
            /// ��
            /// </summary>
            [EnumDescription(DefaultDescription = "��", DefaultValueText = "5")]
            Year

        }
        #endregion
    }
}
