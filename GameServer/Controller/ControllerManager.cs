using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonData;
using System.Reflection;
using GameServer.Servers;

namespace GameServer.Controller
{
    class ControllerManager
    {
        private Dictionary<RequestType, BaseController> ControllerDict = new Dictionary<RequestType, BaseController>();
        private Server server;

        ControllerManager(Server server)
        {
            Init(server);
        }

        void Init(Server server) 
        {
            this.server = server;

            DefaultController defaultController = new DefaultController();
            ControllerDict.Add(defaultController.requestType, defaultController);

        }
        public void HandleRequest(RequestType rt ,RequestAction ra ,string data,Client client)
        {
            BaseController controller;
            bool isGet = ControllerDict.TryGetValue(rt, out controller);

            if (isGet==false)
            {
                Console.WriteLine("无法得到 requestCode 对应的 controller");
                return;
            }//无法得到 requestCode 对应的 controller

            string methodName = Enum.GetName(typeof(RequestAction), ra);
            MethodInfo mi = controller.GetType().GetMethod(methodName);

            if (mi == null)
            {
                Console.WriteLine("controller 里没有" + methodName +"方法");
                return;
            }//controller 里没有" + methodName +"方法

            object[] parameters = new object[] { data, client };
            object obj =mi.Invoke(controller, parameters);
            if (obj ==null || string.IsNullOrEmpty(obj as string))
            {
                return;
            }
            server.SendRespones(rt,client);
        }
        
    }
}
