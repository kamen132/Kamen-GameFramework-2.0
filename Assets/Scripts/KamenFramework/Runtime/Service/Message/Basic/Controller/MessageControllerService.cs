﻿using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace KamenFramework
{
    public interface IControllerService : IService
    {
        
    }
    public class MessageControllerService : ServiceBase ,IControllerService
    {
        protected override IEnumerator OnInit()
        {
            string controllerName = "Game.Message";
            foreach (Type item in (from type in AppDomain.CurrentDomain.GetAssemblies().First((Assembly a) => a.GetName().Name == controllerName).GetTypes()
                where type.GetInterfaces().Any((Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IMessageController<>))
                select type).ToList())
            {
                Type controllerType = item;
                var controllerInstance = Activator.CreateInstance(controllerType);
                Type messageType = (from t in controllerType.GetInterfaces()
                    where t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IMessageController<>)
                    select t.GetGenericArguments().FirstOrDefault()).FirstOrDefault();
                MethodInfo handleMethod = controllerType.GetMethod("Handle");
                if (!(handleMethod == null) && !(messageType == null))
                {
                    ServiceManager.Instance.GetService<IMessageService>().Register(messageType, (msg) =>
                    {
                        handleMethod.Invoke(controllerInstance, new object[1] {msg});
                    });
                }
            }
            return base.OnInit();
        }
    }
}