using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;
using wslyvh.Core.Data.Entity;
using wslyvh.Core.Interfaces.Data;
using wslyvh.Core.Interfaces.Data.Entity;

namespace wslyvh.Core.Test.Data.Entity
{
    [TestClass]
    public class DbContextRepositoryTest
    {
        private DbContext _context;
        private IDbContextUnitOfWork _unitOfWork;
        private IGenericRepository<Mock<string>> _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _context = new Mock<DbContext>().Object;
            _unitOfWork = new DbContextUnitOfWork(_context);
            _repository = new DbContextUnitOfWorkRepository<Mock<string>>(_unitOfWork);
        }

        [TestMethod]
        public void DbContextRepositoryConstructorTest()
        {
            _repository = new DbContextUnitOfWorkRepository<Mock<string>>(_unitOfWork);
            
            Assert.IsNotNull(_repository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DbContextRepositoryConstructorNullTest()
        {
            _repository = new DbContextUnitOfWorkRepository<Mock<string>>(null);
        }

        [TestMethod]
        public void DbContextRepositoryAsQueryable()
        {
            var actual = _repository.AsQueryable();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void DbContextRepositoryFindAll()
        {
            var actual = _repository.FindAll();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DbContextRepositoryFindNull()
        {
            _repository.Find(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DbContextRepositoryFindInvalidPredicate()
        {
            var actual = _repository.Find(i => i.Object == null);

            Assert.IsNotNull(actual);
        }
    }
}
