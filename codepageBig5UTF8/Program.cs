using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace codepageBig5UTF8
{
    class Program
    {
        static void Main(string[] args)
        {
          
            //string source = "⊃;nÅé&frac14;Ò⊃;Õ¤¤ªº¤@¯ë©Ê¿ù»~¡G&frac14;Ð·Ç GUI (⊃;Ï§Î¤Æ¥Î¤á¤¶­±)¡C";

            string input = System.IO.File.ReadAllText(@"C:\aa.txt",Encoding.GetEncoding(950));

            //checkCodePage(input);
            big5toutf8(input);
            //CodePageList();

        }

        static void checkCodePage(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var e1 in Encoding.GetEncodings())
            {
                foreach (var e2 in Encoding.GetEncodings())
                {
                    byte[] unknow = Encoding.GetEncoding(e1.CodePage).GetBytes(input);
                    string result = Encoding.GetEncoding(e2.CodePage).GetString(unknow);
                    sb.AppendLine(string.Format("{0} => {1} : {2}", e1.CodePage, e2.CodePage, result));
                }
            }
            File.WriteAllText(@"C:\Users\frankhuang\Downloads\bb.txt", sb.ToString());
        }
        static void big5toutf8(string input)
        {
            //1200 utf-16  65001 utf-8
            StringBuilder sb = new StringBuilder();
            byte[] unknow = Encoding.GetEncoding(1200).GetBytes(input);
            byte[] unknow2 = Encoding.GetEncoding(950).GetBytes(input);
            string result = Encoding.GetEncoding(65001).GetString(unknow);
            sb.AppendLine(result);

            File.WriteAllText(@"C:\bb.txt", sb.ToString());
        }
        static void CodePageList()
        {
            StringBuilder sb = new StringBuilder();
            foreach (EncodingInfo ei in Encoding.GetEncodings())
            {
                Encoding e = ei.GetEncoding();
                sb.AppendLine(string.Format ("Name:{0},CodePage:{1}", ei.Name, e.CodePage));
               // Console.WriteLine("Name:{0},CodePage:{1}", ei.Name, e.CodePage);               
            }
            File.WriteAllText(@"C:\cc.txt", sb.ToString());
        
        }
        
        static void big5streamToUTF8()
        {
            byte[] big5Bytes = null;
            //string fileName = @"C:\Users\frankhuang\Downloads\aa.txt";
            string fileName = @"C:\test\taiwan.json";
            string utf8Str;
            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
            {
                //note: this is a successful convert method unless method above has some unicode string problem
                //get big5 encoding bytes
                big5Bytes = new byte[fs.Length];
                fs.Read(big5Bytes, 0, (int)fs.Length);
                //big5 byte convert to utf8 bytes
                byte[] utf8Bytes = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding("BIG5"), System.Text.Encoding.UTF8, big5Bytes);
                //utf8 bytes to utf8 string
                System.Text.UTF8Encoding encUtf8 = new System.Text.UTF8Encoding();
                 utf8Str = encUtf8.GetString(utf8Bytes);
            }
            System.IO.File.WriteAllText(@"C:\test\taiwanBig5.json", utf8Str);
            
        }
        }

    }
}
