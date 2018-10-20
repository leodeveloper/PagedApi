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


        public EnumerableDisposable(IEnumerable<TItem> items)
        {
            this.value = items;
        }

        public void Dispose()
        {
            this.value = null;
        }       

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return this.value.GetEnumerator();
        }
    }

   
}
