using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppUI.Objects
{
    internal class Message
    {
        private string senderName;
        private string message;
        
        public Message(string senderName, string message)
        {
            this.senderName = senderName;
            this.message = message;
        }
        public Message() { }

        public String getSender() {  return senderName; }
        public void setSender(String senderNamer) { this.senderName = senderNamer; }

        public String getMessage() { return message; }
        public void setMessage(String message) { this.message = message; }
    }
}
