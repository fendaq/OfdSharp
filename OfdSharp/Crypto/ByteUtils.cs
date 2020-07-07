using System;
using System.Globalization;
using System.Text;

namespace OfdSharp.Crypto
{
    /// <summary>
    /// �ֽڲ�������
    /// </summary>
    public static class ByteUtils
    {
        /// <summary>
        /// ��Ascii�ַ�ת��Ϊ�ֽ�
        /// </summary>
        /// <param name="s">Ascii�ַ���</param>
        /// <returns></returns>
        internal static byte[] AsciiBytes(string s)
        {
            byte[] array = new byte[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                array[i] = (byte)s[i];
            }
            return array;
        }

        /// <summary>
        /// 16�����ַ���ת�ֽ�����
        /// </summary>
        /// <param name="hexString">16�����ַ���</param>
        /// <returns></returns>
        internal static byte[] HexToByteArray(this string hexString)
        {
            byte[] array = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string s = hexString.Substring(i, 2);
                array[i / 2] = byte.Parse(s, NumberStyles.HexNumber, null);
            }
            return array;
        }

        /// <summary>
        /// �ֽ�����ת16�����ַ���
        /// </summary>
        /// <param name="bytes">�ֽ�����</param>
        /// <returns></returns>
        internal static string ByteArrayToHex(this byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                stringBuilder.Append(b.ToString("X2"));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// �ֽ�����ת��Ϊָ�����ȵ�16�����ַ���
        /// </summary>
        /// <param name="bytes">�ֽ�����</param>
        /// <param name="len">����</param>
        /// <returns></returns>
        internal static string ByteArrayToHex(this byte[] bytes, int len)
        {
            return bytes.ByteArrayToHex().Substring(0, len * 2);
        }

        /// <summary>
        /// ȡ���ֽ�
        /// </summary>
        /// <param name="b"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static byte[] RepeatByte(byte b, int count)
        {
            byte[] array = new byte[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = b;
            }
            return array;
        }

        internal static byte[] SubBytes(this byte[] bytes, int startIndex, int length)
        {
            byte[] array = new byte[length];
            Array.Copy(bytes, startIndex, array, 0, length);
            return array;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static byte[] Xor(this byte[] value)
        {
            byte[] array = new byte[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                array[i] ^= value[i];
            }
            return array;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="valueA"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        internal static byte[] Xor(this byte[] valueA, byte[] valueB)
        {
            int num = valueA.Length;
            byte[] array = new byte[num];
            for (int i = 0; i < num; i++)
            {
                array[i] = (byte)(valueA[i] ^ valueB[i]);
            }
            return array;
        }

        /// <summary>
        /// ����λ����
        /// </summary>
        /// <param name="number"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static int RightShift(int number, int bits)
        {
            if (number >= 0)
            {
                return number >> bits;
            }
            return (number >> bits) + (2 << ~bits);
        }

        /// <summary>
        /// ����λ����
        /// </summary>
        /// <param name="number"></param>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static long RightShift(long number, int bits)
        {
            if (number >= 0)
            {
                return number >> bits;
            }
            return (number >> bits) + (2L << ~bits);
        }
    }
}
