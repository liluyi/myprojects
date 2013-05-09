using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerBase
{
    class Message
    {
        public enum CommandHeader : byte
        {
            LoginAuth,
            LoginAck,

            SendXmlRequest,
            SendXmlAck,

            GetXmlRequest,
            GetXmlAck,

            SendXml,
            ReceivedXml,

            SendDocumentRequest,
            SendDocumentAck,

            GetDocumentRequest,
            GetDocumentAck,

            SendDocument,
            ReceivedDocument,

            Push,//针对Server
            Sync,//针对Client

            Hello,//服务器判断客户端isAlive
            Offline,//正常下线

            GetProjectVersionRequest,
            GetProjectVersionAck,

            Chat//供测试
        }

        //文件信息标志，表示数据包状态//文件件待考虑
        public enum MessageFlagHeader : byte
        {
            FileBegin,
            FileMiddle,
            FileEnd,
            Other
        }
        //消息长度
        public int MessageLength;

        //文件信息标志
        public MessageFlagHeader MessageFlag = MessageFlagHeader.Other;

        //若为文件，需要数据包的编号，防止失序读取,默认为0
        public int FilePacketNumber = 0;

        //命令
        public CommandHeader Command;

        //消息体
        public byte[] MessageBody;

        public Message()
        {

        }
        public Message(CommandHeader command, MessageFlagHeader flag, byte[] messageBody)
            : this()
        {
            this.Command = command;
            this.MessageFlag = flag;
            this.MessageBody = messageBody;
        }
        //将消息转化为字节数组
        public byte[] ToBytes()
        {
            this.MessageLength = 4 + 1 + 4 + 1 + this.MessageBody.Length;
            byte[] buffer = new byte[this.MessageLength];

            //先将长度的4个字节写入到数组中
            BitConverter.GetBytes(this.MessageLength).CopyTo(buffer, 0);

            //将一个字节的文件信息标志写入数组中
            buffer[4] = (byte)this.MessageFlag;

            //将文件数据包序号存入数组
            BitConverter.GetBytes(this.FilePacketNumber).CopyTo(buffer, 5);

            //将一个字节的CommandHeader写入到数组中
            buffer[9] = (byte)this.Command;

            //将消息体写入数组
            this.MessageBody.CopyTo(buffer, 10);

            return buffer;
        }

        public static Message Parse(NetworkStream connection){
            Message message = new Message();
            byte[] buffer = new byte[4];

            //先读出前4个字节，即Message的长度
            if(ReadMessagePartial(connection, buffer,4)){
                message.MessageLength = BitConverter.ToInt32(buffer, 0);
            }
            
            buffer = new byte[message.MessageLength - 4];

            //读出消息的其它字节
            if (ReadMessagePartial(connection, buffer, message.MessageLength-4)) {
                //读出一个字节的文件信息标志
                message.MessageFlag = (MessageFlagHeader)buffer[0];

                //读出数据包序号,4个字节
                message.FilePacketNumber = BitConverter.ToInt32(buffer, 1);

                //读出命令
                message.Command = (CommandHeader)buffer[5];

                //读出消息体
                int i = 0;
                message.MessageBody = new byte[message.MessageLength - 10];
                while (i < message.MessageLength - 10)
                {
                    message.MessageBody[i] = buffer[6 + i];
                    i++;
                }
            }
            return message;
        }     

        private static Boolean ReadMessagePartial(NetworkStream connection,byte[] buffer,int byteNumber)
        {
            //阻塞读取数据
            while(!connection.CanRead){
                Thread.Sleep(5);//5ms
            }
                        
            while (!connection.DataAvailable)
            {
                Thread.Sleep(5);//5ms
            }

            try
            {
                int count = connection.Read(buffer, 0, byteNumber);
                int temp = 0;
                while (count < byteNumber)
                {
                    Thread.Sleep(5);//5ms
                    temp = connection.Read(buffer, count, byteNumber - count);
                    count += temp;//已读取的字节数
                }
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine("ReadMessageLength: "+ex.Message);
                return false;
            }
        }
    }
}
