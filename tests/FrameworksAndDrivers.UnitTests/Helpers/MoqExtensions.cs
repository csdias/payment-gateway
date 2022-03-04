using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FrameworksAndDrivers.UnitTests.Helpers
{
    public static class MoqExtensions2
    {
        public static Mock<DbSet<T>> DbSetMock<T>(IEnumerable<T> listDbSet) where T : class
        {
            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(listDbSet.AsQueryable().Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(listDbSet.AsQueryable().Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(listDbSet.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(listDbSet.AsQueryable().GetEnumerator());
            return dbSetMock;
        }
    }
}

namespace FrameworksAndDrivers.UnitTests.Helpers {
    public static class MoqExtensions
    {
        public static Mock<DbSet<T>> DbSetMock<T>(IEnumerable<T> dataToBeReturnedOnGet) where T : class
        {
            var mocks = dataToBeReturnedOnGet.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(mocks.Provider));
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(mocks.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(mocks.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(mocks.GetEnumerator());
            return mockSet;
        }

    }

    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Execute<TResult>(expression);
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestAsyncQueryProvider<T>(this); }
        }
        public IAsyncEnumerator<T> GetEnumerator()
        {
            return this.GetAsyncEnumerator();
        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return this.GetAsyncEnumerator();
        }
        
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public T Current
        {
            get { return _inner.Current; }
        }
        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(Task.FromResult(_inner.MoveNext()));
        }

        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.Run(() => {
                _inner.Dispose();
            }));
        }
    }
}