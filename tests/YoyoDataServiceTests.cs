using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using web.Data;
using web.Models;
using web.Services;
using Xunit;

namespace tests
{
    public class YoyoDataServiceTests
    {
        private List<Shuttle> TestShuttles => new List<Shuttle>
            {
                new Shuttle
                {
                    SpeedLevel = 10,
                    ShuttleNo = 2,
                    StartTime = new TimeSpan(0,0,21),
                    CommulativeTime = new TimeSpan(0,0,20),
                },
                new Shuttle
                {
                    SpeedLevel  = 9,
                    ShuttleNo = 1,
                    StartTime = new TimeSpan(0,0,10),
                    CommulativeTime = new TimeSpan(0,0,10),
                }
            };

        [Fact]
        public void CheckForMatchingShuttle_Returns_Null_When_No_Match_Found()
        {
            Mock<IRepository<Shuttle>> mockShuttleRepo = new Mock<IRepository<Shuttle>>();
            mockShuttleRepo.SetupGet(repo => repo.Data).Returns(() => TestShuttles);

            var sut = new YoyoDataService(null, mockShuttleRepo.Object, null);
            var timeParam = new TimeSpan(0, 0, 11);


            var result = sut.CheckForMatchingShuttle(timeParam);

            Assert.Null(result);

        }

        [Fact]
        public void CheckForMatchingShuttle_Returns_Null_When_Match_Found()
        {
            Mock<IRepository<Shuttle>> mockShuttleRepo = new Mock<IRepository<Shuttle>>();
            mockShuttleRepo.SetupGet(repo => repo.Data).Returns(() => TestShuttles);
            var sut = new YoyoDataService(null, mockShuttleRepo.Object, null);
            var timeParam = new TimeSpan(0, 0, 10);

            var result = sut.CheckForMatchingShuttle(timeParam);

            Assert.NotNull(result);
            Assert.Equal(result.StartTime, timeParam);
        }

        [Fact]
        public void GetShuttleResults_Returns_Result_Tuple_In_Order_By_ShuttleNo_Then_Speed()
        {
            Mock<IRepository<Shuttle>> mockShuttleRepo = new Mock<IRepository<Shuttle>>();
            mockShuttleRepo.SetupGet(repo => repo.Data).Returns(() => TestShuttles);

            var sut = new YoyoDataService(null, mockShuttleRepo.Object, null);

            var result = sut.GetShuttleResults();

            Assert.NotNull(result);
            Assert.Equal(9, result[0].Item1);
            Assert.Equal(1, result[0].Item2);
            Assert.Equal(10, result[1].Item1);
            Assert.Equal(2, result[1].Item2);

        }
    }
}
