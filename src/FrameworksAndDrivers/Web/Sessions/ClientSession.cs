using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using InterfaceAdapters.Interfaces;

namespace FrameworksAndDrivers.Web.Sessions
{
    public class ClientSession : IClientSession
    {
        private IHttpContextAccessor _context;
        private Dictionary<string, object> _session = new Dictionary<string, object>();

        public ClientSession(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetClientId()
        {
            if(!this._session.ContainsKey("ClientId"))
            {
                //TODO: Remove (Default value during testing = Life Planner)
                //this.SetClientId("A953DC88-EB1B-350C-E053-2C118C0A2285");
                this.SetClientId(_context.HttpContext.User.FindFirst("ClientId")?.Value);
            }
            return this._session["ClientId"].ToString();
        }

        public IClientSession SetClientId(string clientId)
        {
            this._session.Add("ClientId", string.IsNullOrEmpty(clientId) ? "A953DC88-EB1B-350C-E053-2C118C0A2285" : clientId);
            return this;
        }
    }
}