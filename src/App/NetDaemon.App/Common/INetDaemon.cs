﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoySoftware.HomeAssistant.NetDaemon.Common
{
    /// <summary>
    /// Interface that all NetDaemon apps needs to implement
    /// </summary>
    public interface INetDaemonApp
    {
        /// <summary>
        /// Start the application, normally implemented by the base class
        /// </summary>
        /// <param name="daemon"></param>
        Task StartUpAsync(INetDaemon daemon);

        /// <summary>
        /// Init the application, is called by the NetDaemon after startup
        /// </summary>
        /// <param name="daemon"></param>
        Task InitializeAsync();

        
    }

   
    public interface INetDaemon
    {
        ILogger Logger { get; }
        /// <summary>
        ///     Listen to statechange
        /// </summary>
        /// <param name="pattern">Match pattern, entity_id or domain</param>
        /// <param name="action">The func to call when matching</param>
        /// <remarks>
        ///     The callback function is
        ///         - EntityId
        ///         - newEvent
        ///         - oldEvent
        /// </remarks>
        void ListenState(string pattern,
            Func<string, EntityState?, EntityState?, Task> action);
        Task TurnOnAsync(string entityId, params (string name, object val)[] attributes);
        Task TurnOffAsync(string entityIds, params (string name, object val)[] attributes);
        Task ToggleAsync(string entityIds, params (string name, object val)[] attributes);
        EntityState? GetState(string entity);

        //IAction Action { get; }
        IEntity Entity(params string[] entityId);
        IEntity Entities(Func<IEntityProperties, bool> func);

        ILight Light(params string[] entity);

        IEnumerable<EntityState> State { get; }


    }
}