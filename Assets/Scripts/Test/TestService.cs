using System.Collections;
using KamenFramework;

namespace Test
{
    public interface ITestService
    {
        
    }
    public class TestService : ServiceBase , ITestService
    {
        protected override IEnumerator OnInit()
        {
            KLogger.LogError("OnInit");
            return base.OnInit();
        }

        public override void BeforeInit()
        {
            base.BeforeInit();
            KLogger.LogError("BeforeInit");
        }

        public override void AfterInit()
        {
            base.AfterInit();
            KLogger.LogError("AfterInit");
        }
    }
}