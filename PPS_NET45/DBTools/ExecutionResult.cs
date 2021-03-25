using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBTools
{
    public class ExecutionResult
    {
        private object anythingField;
        private object[] argesField;
        private string messageField;
        private string msgCodeField;
        private bool statusField;

        public object Anything
        {
            get
            {
                return this.anythingField;
            }
            set
            {
                this.anythingField = value;
            }
        }

        public object[] Arges
        {
            get
            {
                return this.argesField;
            }
            set
            {
                this.argesField = value;
            }
        }

        public string Message
        {
            get
            {
                return this.messageField;
            }
            set
            {
                this.messageField = value;
            }
        }

        public string MSGCode
        {
            get
            {
                return this.msgCodeField;
            }
            set
            {
                this.msgCodeField = value;
            }
        }

        public bool Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
}
