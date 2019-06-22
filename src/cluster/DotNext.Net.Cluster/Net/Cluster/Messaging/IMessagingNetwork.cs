using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotNext.Net.Cluster.Messaging
{
    /// <summary>
    /// Specifies a cloud of nodes that can communicate with each other through the network.
    /// </summary>
    public interface IMessagingNetwork : ICluster
    {
        /// <summary>
        /// Gets the leader node.
        /// </summary>
        new IAddressee Leader { get; }

        /// <summary>
        /// Represents a collection of nodes in the network.
        /// </summary>
        new IReadOnlyCollection<IAddressee> Members { get; }

        /// <summary>
        /// Sends a message to the cluster leader.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="token">The token that can be used to cancel asynchronous operation.</param>
        /// <returns>The message representing response; or <see langword="null"/> if request message in one-way.</returns>
        /// <exception cref="InvalidOperationException">Leader node is not present in the cluster.</exception>
        Task<IMessage> SendMessageToLeaderAsync(IMessage message, CancellationToken token = default);

        /// <summary>
        /// Sends one-way message to the cluster leader.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="token">The token that can be used to cancel asynchronous operation.</param>
        /// <returns>The task representing execution of this method.</returns>
        /// <exception cref="InvalidOperationException">Leader node is not present in the cluster.</exception>
        Task SendSignalToLeaderAsync(IMessage message, CancellationToken token = default);
    }
}