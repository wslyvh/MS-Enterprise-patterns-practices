using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wslyvh.Core
{
    public class ApplicationContextFactory<T>
    {
        [ThreadStatic]
        private static T _context;

        public ApplicationContextFactory(T context)
        {
            Guard.ArgumentIsNotNull(context, "context");

            _context = context;
        }

        public ApplicationContext Create()
        {
            return _context as ApplicationContext;
        }
    }
}
