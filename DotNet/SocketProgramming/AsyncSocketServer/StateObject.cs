﻿using System.Net.Sockets;
using System.Text;

namespace AsyncSocketServer
{
    // State object for reading client data asynchronously 
    public class StateObject
    {
        // Size of receive buffer.  
        public const int BufferSize = 1024;

        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        // Received data string.
        public StringBuilder sb = new StringBuilder();

        // Client socket.
        public Socket workSocket = null;
    }
}
