using Org.BouncyCastle.Crypto;
using System;

namespace OfdSharp.Crypto
{
    /// <summary>
    /// ժҪ
    /// </summary>
    public abstract class SmDigest : IDigest
    {
        /// <summary>
        /// �����ֽ�����
        /// </summary>
        public byte[] Buffer { get; }

        /// <summary>
        /// �����ֽ��������λ��
        /// </summary>
        public int Offset { get; private set; }

        /// <summary>
        /// �ֽڳ���
        /// </summary>
        public long ByteCount { get; private set; }

        /// <summary>
        /// �㷨����
        /// </summary>
        public abstract string AlgorithmName { get; }

        /// <summary>
        /// �޲ι��캯��
        /// </summary>
        internal SmDigest()
        {
            Buffer = new byte[4];
        }

        /// <summary>
        /// �вι��캯��
        /// </summary>
        /// <param name="digest"></param>
        internal SmDigest(SmDigest digest)
        {
            Buffer = digest.Buffer;
            Array.Copy(digest.Buffer, 0, Buffer, 0, digest.Buffer.Length);
            Offset = digest.Offset;
            ByteCount = digest.ByteCount;
        }

        /// <summary>
        /// �����ֽڻ���
        /// </summary>
        /// <param name="input"></param>
        public void Update(byte input)
        {
            Buffer[Offset++] = input;
            if (Offset == Buffer.Length)
            {
                ProcessWord(Buffer, 0);
                Offset = 0;
            }
            ByteCount++;
        }

        /// <summary>
        /// ���¿�
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inOff"></param>
        /// <param name="length"></param>
        public void BlockUpdate(byte[] input, int inOff, int length)
        {
            while (Offset != 0 && length > 0)
            {
                Update(input[inOff]);
                inOff++;
                length--;
            }
            while (length > Buffer.Length)
            {
                ProcessWord(input, inOff);
                inOff += Buffer.Length;
                length -= Buffer.Length;
                ByteCount += Buffer.Length;
            }
            while (length > 0)
            {
                Update(input[inOff]);
                inOff++;
                length--;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        protected void Finish()
        {
            long bitLength = ByteCount << 3;
            Update(128);
            while (Offset != 0)
            {
                Update(0);
            }
            ProcessLength(bitLength);
            ProcessBlock();
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void Reset()
        {
            ByteCount = 0L;
            Offset = 0;
            Array.Clear(Buffer, 0, Buffer.Length);
        }

        /// <summary>
        /// �ֽڳ���
        /// </summary>
        /// <returns></returns>
        public int GetByteLength()
        {
            return 64;
        }

        /// <summary>
        /// ժҪ��С
        /// </summary>
        /// <returns></returns>
        public abstract int GetDigestSize();

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inOff"></param>
        protected abstract void ProcessWord(byte[] input, int inOff);

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="bitLength"></param>
        protected abstract void ProcessLength(long bitLength);

        /// <summary>
        /// �����ֽڿ�
        /// </summary>
        protected abstract void ProcessBlock();

        /// <summary>
        /// ��ɼ���
        /// </summary>
        /// <param name="output"></param>
        /// <param name="outOff"></param>
        /// <returns></returns>
        public abstract int DoFinal(byte[] output, int outOff);
    }
}
