using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using wslyvh.Core.Data.Entity;
using wslyvh.Core.Interfaces.Data.Entity;
using wslyvh.Core.Test.Mock;

namespace wslyvh.Core.Test.Data.Entity
{
    [TestClass]
    public class DbContextUnitOfWorkTest
    {
        private DbContext _context;
        private IDbContextUnitOfWork _unitOfWork;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new Mock<DbContext>().Object;
            _unitOfWork = new DbContextUnitOfWork(_context);
        }

        [TestMethod]
        public void DbContextUnitOfWorkConstructorTest()
        {
            Assert.AreEqual(_context, _unitOfWork.Context);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DbContextUnitOfWorkConstructorNullTest()
        {
            _unitOfWork = new DbContextUnitOfWork(null);
        }

        [TestMethod]
        public void CommitTest()
        {
            _unitOfWork.Commit();
            Assert.IsNotNull(_unitOfWork.Context);
        }

        [TestMethod]
        public void DisposeTest()
        {
            _unitOfWork.Dispose();
            Assert.IsNull(_unitOfWork.Context);
        }

        [TestMethod]
        public void UsingDisposeTest()
        {
            var context = new TestDbContext();
            DbContextUnitOfWork unitOfWork;
            using (unitOfWork = new DbContextUnitOfWork(context))
            {
                //Do something
            }

            Assert.IsNull(unitOfWork.Context);
        }
    }
}
