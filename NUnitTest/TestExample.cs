using System;
using Moq;
using NUnit.Framework;

namespace NUnitTest
{
    [TestFixture]
    public class TestExample
    {
        [Test]
        public void InterfaceMEthodIsCalled()
        {
            var moq = new Mock<ITestInterface>();

            var obj = new TestClass(moq.Object);

            obj.CallsInt(5);

            moq.Verify(ti => ti.CallMe(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void InterfaceMethodIsCalledWintCorrectInput()
        {
            var moq = new Mock<ITestInterface>();

            var obj = new TestClass(moq.Object);

            obj.CallsInt(5);

            moq.Verify(ti => ti.CallMe(It.Is<int>(i => i == 5)), Times.Once());
        }

        [Test]
        [ExpectedException]
        public void ThrowsExceptionWhenReturnsFive()
        {
            var moq = new Mock<ITestInterface>();
            moq.Setup(ti => ti.ReturnSomething())
               .Returns(5);

            var obj = new TestClass(moq.Object);

            obj.ThrowIfFive(5);
        }
    }

    public interface ITestInterface
    {
        void CallMe(int number);
        void CallMeStr(string str);

        int ReturnSomething();
    }


    internal class TestClass
    {
        private readonly ITestInterface _test;

        public TestClass(ITestInterface test)
        {
            _test = test;
        }

        public void CallsInt(int num)
        {
            _test.CallMe(num);
        }

        public void ThrowIfFive(int num)
        {
            if (_test.ReturnSomething() == 5)
            {
                throw new Exception();
            }
        }
    }
}
