using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Interception;
using unity = Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Test.Interception
{
    [TestClass]
    public class BaseInterceptionBehaviorTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void BaseInterceptionBehaviorConstructorTest()
        {
            var target = new BaseInterceptionBehavior();
            
            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void BaseInterceptionBehaviorInvokeTest()
        {
            //Arrange
            var container = new UnityContainer();
            container.AddNewExtension<unity.Interception>();
            container.RegisterType<IInterceptionBehavior, BaseInterceptionBehavior>("BaseInterceptionBehavior",
                new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<BaseInterceptionBehavior>());
            
            //Act
            var target = container.Resolve<IInterceptionBehavior>("BaseInterceptionBehavior");
            
            //Assert
            Assert.IsTrue(target.WillExecute);
        }
        
        [TestMethod]
        public void BaseInterceptionBehaviorRequiredInterfacesTest()
        {
            var target = new BaseInterceptionBehavior();
            var interfaces = target.GetRequiredInterfaces();

            Assert.IsNotNull(interfaces);
        }

        [TestMethod]
        public void BaseInterceptionBehaviorWillExecuteTest()
        {
            var target = new BaseInterceptionBehavior();

            Assert.IsTrue(target.WillExecute);
        }
    }
}