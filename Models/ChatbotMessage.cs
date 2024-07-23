using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroKero.Models
{
    public class ChatbotMessage
    {
        private string _MessageContent;
        private bool _MessageIsInbound;

        public string MessageContent
        {
            get { return _MessageContent; }
            set { _MessageContent = value; }
        }

        public bool MessageIsInbound
        {
            get { return _MessageIsInbound; }
            set { _MessageIsInbound = value; }
        }
          
        public ChatbotMessage(string __MessageContent, bool __MessageIsInbound)
        {
            this._MessageContent = __MessageContent;
            this._MessageIsInbound = __MessageIsInbound;
        }

        public ChatbotMessage()
        {
            this.MessageContent = "Blank Message";
            this.MessageIsInbound = true;
        }

    }
}
