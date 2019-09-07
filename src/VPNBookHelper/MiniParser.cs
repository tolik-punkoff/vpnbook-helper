using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace VPNBookHelper
{
    public class MiniParser
    {
        public string FileName { get; set; }

        public string ErrorMessage { get; private set; }

        private string HTMLPage = "";

        public MiniParser(string filename)
        {
            FileName = filename;
        }

        public bool Load()
        {
            try
            {
                HTMLPage = File.ReadAllText(FileName);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }

            return true;
        }

        public List<string> ParseTags(string Tag, string Property)
        {
            List<string> listBuf = new List<string>();

            Regex reHref = new Regex(@"(?inx)
                                        <" + Tag + @" \s [^>]*" +
                                            Property + @"\s* = \s*"+
                                                @"(?<q> ['""] )"+
                                                    @"(?<url> [^""]+ )"+
                                                 @"\k<q>"+
                                         @"[^>]* >");

            foreach (Match match in reHref.Matches(HTMLPage))
            {
                listBuf.Add(match.Groups["url"].ToString());
            }

            return listBuf;
        }

        public List<string> Select(string Pattern, List<string> inputList)
        {
            List<string> selectList = new List<string>();

            foreach (string s in inputList)
            {
                if (s.Contains(Pattern))
                {
                    selectList.Add(s);
                }
            }

            return selectList;
        }        
    }
}
