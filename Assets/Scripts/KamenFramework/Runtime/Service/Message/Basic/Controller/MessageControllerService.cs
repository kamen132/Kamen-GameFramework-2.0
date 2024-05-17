using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using KamenFramework.Runtime.Service.Base;

namespace KamenFramework.Runtime.Service.Message.Basic.Controller
{
    public interface IControllerService : IService
    {
        
    }
    public class MessageControllerService : ServiceBase ,IControllerService
    {
        protected override IEnumerator OnInit()
        {
            string controllerName = "Game.MessageController";
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
                    KamenGame.Instance.MessageService.Register(messageType, (msg) =>
                    {
                        handleMethod.Invoke(controllerInstance, new object[1] {msg});
                    });
                }
            }
            return base.OnInit();
        }
    }
}