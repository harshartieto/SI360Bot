using SI.Biz.Core.ClientAccess;
using SI.Biz.Core.Svc;
using SI.Linq.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SI.Biz.Core.Bot.InvokeWrapper
{
    public class BLInvoker
    {
        private MetaContext _context;
        private XInvoke _invoke;
        private string _user;



        #region Public Properties

        public EndpointAddress RemoteAddress { private set; get; }

        public MetaContext Context
        {
            get
            {
                if (_context == null)
                {
                    var linqProxy = new LinqReader(RemoteAddress);
                    _context = linqProxy.GetDefaultMetaContext();
                }

                return _context;
            }
        }

        public XInvoke Invoke
        {
            get
            {
                if (_invoke == null)
                {
                    var proxy = new ClientProxy<IManagerFactory>(RemoteAddress);
                    _invoke = new XInvoke(proxy);
                }

                return _invoke;
            }
        }

        #endregion

        #region Public methods

        public BLInvoker(string endPointUri, string user)
        {
            RemoteAddress = new EndpointAddress(endPointUri);
            _user = user;
        }

        public void Call(Expression<Action> methodCall)
        {
            Invoke.Call(methodCall, user: _user);
        }

        public T Call<T>(Expression<Func<T>> methodCall)
        {
            return Invoke.Call(methodCall, user: _user);
        }

        public T SurfaceCall<T>(Expression<Func<T>> methodCall)
        {
            return Invoke.Call(methodCall, "SI.Biz.Core.Surface", user: _user);
        }

        #endregion
    }
}
