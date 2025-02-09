using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Communication.Hubs
{

    /// <summary>
    /// Creates Hub connections
    /// </summary>
    public class HubFactory
    {
        private string _url;

        private string? _jwt = "";

        public HubFactory(string hubURL)
        {
            _url = hubURL;
        }

        /// <summary>
        /// Sets the jwt token associated with the hub
        /// </summary>
        /// <param name="jwt"></param>
        public void SetAccessToken(string? jwt)
        {
            _jwt = jwt;
        }

        /// <summary>
        /// Gets access to the character hub
        /// </summary>
        /// <returns></returns>
        public HubConnection GetCharacterHub()
        {
            var connection = new HubConnectionBuilder()
            .WithUrl(_url + "/characterhub", options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(_jwt);
            })
            .WithAutomaticReconnect()
            .Build();
            return connection;
        }
    }
}
