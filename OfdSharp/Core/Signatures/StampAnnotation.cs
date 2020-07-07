﻿using OfdSharp.Core.BaseType;
using System.Xml;

namespace OfdSharp.Core.Signatures
{
    /// <summary>
    /// 签名的外观
    /// 一个数字签名可以跟一个或多个外观描述关联，也可以不关联任何外观，
    /// </summary>
    public class StampAnnotation : OfdElement
    {
        public StampAnnotation(XmlDocument xmlDocument) : base(xmlDocument, "StampAnnot")
        {
        }

        /// <summary>
        /// 签章注释的标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 引用外观注释所在的页面的标识符
        /// </summary>
        public string PageRef { get; set; }

        /// <summary>
        /// 签章注释的外观边框位置
        /// </summary>
        public StBox Boundary { get; set; }

        /// <summary>
        /// 签章注释的外观裁剪设置
        /// </summary>
        public StBox Clip { get; set; }
    }
}