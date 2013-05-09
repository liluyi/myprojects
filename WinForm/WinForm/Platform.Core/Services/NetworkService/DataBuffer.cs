using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Core.Services
{
    public class DataBuffer
    {
        //public const int bufferSize = 1048576;//2^20 = 1MB
        public const int bufferSize = 1024;//测试
        public byte[] bufferData = new byte[bufferSize];

        public int startFlag = 0;   //缓冲区有效数据起始位置
        public int endFlag = 0;     //缓冲区有效数据结束位置

        //供测试
        public void test() {
            byte[] t = new byte[12];
            for (int i = 0; i < 12;i++ )
            {
                t[i] = i%2==0?(byte)1:(byte)0;
            }
            writeBuffer(t,12);
            Console.WriteLine(CountBufferDataSize());
            t = new byte[12];
            readBuffer(t, 6);
            Console.WriteLine(CountBufferDataSize());
        }


        //计算缓冲区字节数　
        public int CountBufferDataSize() 
        {
            if (endFlag < startFlag)
            {
                return 2 * bufferSize - (startFlag - endFlag);
            }
            else 
            {
                return (endFlag - startFlag);
            }
        }

        //从缓冲区读取num个字节到数组outArray
        public Boolean readBuffer(byte[] outArray,int num)
        {
            if (CountBufferDataSize() < num)
            {
                Console.WriteLine("缓冲区数据不够");
            }
            else if(startFlag + num > bufferSize)
            {
                Array.Copy(bufferData, startFlag, outArray, 0, bufferSize-startFlag+1);
                startFlag = num - (bufferSize - startFlag + 1);
                Array.Copy(bufferData, 0, outArray, bufferSize - startFlag + 1, startFlag);
                return true;
            }
            else
            {
                Array.Copy(bufferData, startFlag, outArray, 0, num);
                startFlag += num;
                return true;
            }

            return false;
        }

        //向缓冲区中写入数据
        public Boolean writeBuffer(byte[] inArray,int num)
        {
            if (bufferSize - CountBufferDataSize() < num)
            {
                Console.WriteLine("缓冲区空间不够");
            }
            else if (bufferSize - endFlag < num)
            {
                Array.Copy(inArray, 0, bufferData, endFlag, bufferSize - endFlag + 1);
                endFlag = num - (bufferSize - endFlag + 1);
                Array.Copy(inArray, bufferSize - endFlag + 1, bufferData, 0, endFlag);
                return true;
            }
            else
            {
                Array.Copy(inArray,0,bufferData,endFlag,num);
                endFlag += num;
                return true;
            }

            return false;
        }

    }
}
