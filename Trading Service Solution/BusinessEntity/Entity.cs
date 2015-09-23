/**********************************************************************************************************************
 * 
 *  版权所有：(c)2014， 华跃博弈有限公司
 * 
 * ********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.Reflection;

namespace HyBy.FrameWork.DAService
{
    
    public class Entity
    {
        /// Return the entityID
        public virtual string EntityID
        {
            get;
            set;
        }

        public virtual object[] GetKeys()
        {
            return null;
        }

        public virtual int Count()
        {
            return 0;
        }

        public virtual string EntityName
        {
            get;
            set;
        }

        /// <summary>
        /// 复制对象
        /// </summary>
        /// <returns></returns>
        public virtual void CopyTo(Entity entity)
        {
            entity = Activator.CreateInstance(this.GetType()) as Entity;
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
     
                property.SetValue(entity, this.GetType().GetProperty(property.Name).GetValue(this));
            }
        }

        /// <summary>
        /// 复制对象，返回新的对象
        /// </summary>
        /// <returns></returns>
        public virtual Entity Copy()
        {
            Entity entity = Activator.CreateInstance(this.GetType()) as Entity;
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "EntityID")
                    continue;
                property.SetValue(entity, this.GetType().GetProperty(property.Name).GetValue(this));
            }
            return entity;
        }

    }
}
