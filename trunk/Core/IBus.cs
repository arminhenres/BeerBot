using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary>
    /// Interface for Buses
    /// </summary>
    public interface IBus
    {
        /// <summary>
        /// event to be thrown when a message is received
        /// </summary>
        event EventHandler MessageReceived;

        bool ConnectionActive
        {
            get;
            set;
        }
        /// <summary>
        /// Method to initialize Bus
        /// </summary>
        /// <returns>state whether initialization was successful</returns>
        bool Init();

        /// <summary>
        /// Method to de-initialize Bus
        /// </summary>
        /// <returns>state whether de-initialization was successful</returns>
        bool Deinit();

        /// <summary>
        /// Sends given Message to configured destination
        /// </summary>
        /// <param name="message">message to send</param>
        void SendMessage(IMessage message);

        /// <summary>
        /// Builds Message out of given Data
        /// </summary>
        /// <param name="data">data to build message out</param>
        /// <returns>the ready-to-send message</returns>
        IMessage BuildMessage(string data);
    }
}
