namespace HyBy.FrameWork.DAService
{
    using HyBy.FrameWork.Common;
    using System;
    using System.Collections.ObjectModel;
    using System.Reflection;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;

    public class BusinessObject : MarshalByRefObject, IErrorHandler, IServiceBehavior
    {
        protected DataAccess dac;

        protected BusinessObject()
        {
            this.dac = new DataAccess("");
        }

        protected BusinessObject(DataAccess da)
        {
            this.dac = new DataAccess("");
            this.dac = da;
        }

        public void AddBindingParameters(System.ServiceModel.Description.ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(System.ServiceModel.Description.ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            serviceDescription.Namespace = "http://www.dskmall.com";
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                dispatcher.ErrorHandlers.Add(this);
            }
        }

        public virtual void Dispose()
        {
            this.dac.Close();
        }

        public bool HandleError(Exception error)
        {
            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is CommonException)
            {
                CommonException exception = error as CommonException;
                string message = string.Empty;
                ExceptionToMessageHelper.ToString(error, ref message);
                message = "\r\n\tError Code: " + exception.ErrorCode + "\r\nError Level: " + exception.ExceptionLevel.ToString() + "\r\nMachineName:" + Environment.MachineName + "\r\nHostName: " + Assembly.GetExecutingAssembly().GetName().Name + "\r\n" + message;
                FaultException<string> exception2 = new FaultException<string>(exception.ErrorCode, message);
                MessageFault fault2 = exception2.CreateMessageFault();
                fault = Message.CreateMessage(version, fault2.Code, exception.ErrorCode, message, exception2.Action);
                ExceptionToMessageHelper.WriteLog(message);
            }
            else
            {
                FaultException<Exception> exception3 = new FaultException<Exception>(error);
                MessageFault fault3 = exception3.CreateMessageFault();
                fault = Message.CreateMessage(version, fault3, exception3.Action);
                ExceptionToMessageHelper.WriteLog(error);
            }
        }

        public void Validate(System.ServiceModel.Description.ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}

