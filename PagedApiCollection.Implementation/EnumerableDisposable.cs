using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedApi;

namespace PagedApiCollection.Implementation
{
    public class EnumerableDisposable<TItem> : IEnumerableDisposable<TItem>
    {
        private IEnumerable<TItem> value;

        public EnumerableDisposable(IEnumerable<TItem> items) => this.value = items;

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator() => this.value.GetEnumerator();

        #region dispose
        bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

   
}
