using System;
using System.Net.NetworkInformation;
using NUnit.Framework;

namespace ProjectEulerWebApp.Services
{
    public class EulerProblemServiceTest
    {
        
        [Test]
        public void PingTest()
        {
            var ping = new Ping();
            var result = ping.Send("projecteuler.net", 1000);
            Assert.NotNull(result);
            Assert.AreEqual(System.Net.NetworkInformation.IPStatus.Success, result.Status);
        }
    }
}