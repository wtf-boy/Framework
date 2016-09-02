namespace WTF.Framework
{
    using System;
    using System.IO;
    using System.Messaging;
    using System.Runtime.InteropServices;

    public class MessageQueueHelper
    {
        private System.Messaging.MessageQueue _MessageQueue = null;
        private string _Path;

        public MessageQueueHelper(string path)
        {
            this._Path = path;
        }

        private static Message CreateMessage(string label, Stream bodyStream)
        {
            return new Message { Label = label, BodyStream = bodyStream, TimeToBeReceived = Message.InfiniteTimeout, UseDeadLetterQueue = true };
        }

        private static Message CreateMessage(string label, object body)
        {
            return new Message { Label = label, Body = body, TimeToBeReceived = Message.InfiniteTimeout, UseDeadLetterQueue = true };
        }

        public System.Messaging.MessageQueue CreateMessageQueue(string path)
        {
            System.Messaging.MessageQueue queue;
            if (System.Messaging.MessageQueue.Exists(path))
            {
                queue = new System.Messaging.MessageQueue(path);
            }
            else
            {
                queue = System.Messaging.MessageQueue.Create(path);
            }
            queue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
            queue.DefaultPropertiesToSend.AttachSenderId = false;
            queue.DefaultPropertiesToSend.UseAuthentication = false;
            queue.DefaultPropertiesToSend.UseEncryption = false;
            queue.DefaultPropertiesToSend.AcknowledgeType = AcknowledgeTypes.None;
            queue.DefaultPropertiesToSend.UseJournalQueue = false;
            return queue;
        }

        public T ReceiveObject<T>() where T: class
        {
            return this.ReceiveObject<T>(FormatterType.BinaryMessage);
        }

        public T ReceiveObject<T>(FormatterType formatterType) where T: class
        {
            string str;
            return this.ReceiveObject<T>(formatterType, out str);
        }

        public T ReceiveObject<T>(out string label) where T: class
        {
            return this.ReceiveObject<T>(FormatterType.BinaryMessage, out label);
        }

        public T ReceiveObject<T>(FormatterType formatterType, out string label) where T: class
        {
            try
            {
                Message message = this.MessageQueue.Receive();
                switch (formatterType)
                {
                    case FormatterType.BinaryMessage:
                        message.Formatter = new BinaryMessageFormatter();
                        break;

                    case FormatterType.XmlMessage:
                        message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                        break;
                }
                label = message.Label;
                return (T) message.Body;
            }
            catch
            {
                label = "";
                return default(T);
            }
        }

        public Stream ReceiveStream()
        {
            string str;
            return this.ReceiveStream(out str);
        }

        public Stream ReceiveStream(out string label)
        {
            try
            {
                Message message = this.MessageQueue.Receive();
                label = message.Label;
                return message.BodyStream;
            }
            catch
            {
                label = "";
                return null;
            }
        }

        public bool Send(Stream bodyStream)
        {
            return this.Send(bodyStream, "");
        }

        public bool Send(object body)
        {
            return this.Send(body, FormatterType.BinaryMessage);
        }

        public bool Send(Stream bodyStream, string label)
        {
            Message message = CreateMessage(label, bodyStream);
            this.MessageQueue.Send(message);
            return true;
        }

        public bool Send(object body, FormatterType formatterType)
        {
            return this.Send(body, formatterType, "");
        }

        public bool Send(object body, string label)
        {
            return this.Send(body, FormatterType.BinaryMessage, label);
        }

        public bool Send(object body, FormatterType formatterType, string label)
        {
            Message message = CreateMessage(label, body);
            switch (formatterType)
            {
                case FormatterType.BinaryMessage:
                    message.Formatter = new BinaryMessageFormatter();
                    break;

                case FormatterType.XmlMessage:
                    message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                    break;
            }
            this.MessageQueue.Send(message);
            return true;
        }

        public System.Messaging.MessageQueue MessageQueue
        {
            get
            {
                if (this._MessageQueue == null)
                {
                    this._MessageQueue = this.CreateMessageQueue(this._Path);
                }
                return this._MessageQueue;
            }
        }

        public enum FormatterType
        {
            BinaryMessage,
            XmlMessage
        }
    }
}

