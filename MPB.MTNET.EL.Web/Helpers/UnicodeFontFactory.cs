﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MPB.MTNET.EL.Web.Helpers
{
    public class UnicodeFontFactory : FontFactoryImp
    {
        //下載PDF，未完成
        private static readonly string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
            "arialuni.ttf");//arial unicode MS是完整的unicode字型。
        private static readonly string kaiu = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),
          "KAIU.TTF");//標楷體


        public override Font GetFont(string fontname, string encoding, bool embedded, float size, int style, BaseColor color,
            bool cached)
        {
            //可用Arial或標楷體，自己選一個
            BaseFont baseFont = BaseFont.CreateFont(kaiu, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            return new Font(baseFont, size, style, color);
        }


    }
}