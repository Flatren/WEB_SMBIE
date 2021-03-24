using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using BDSystemSMBIEContext;
using CsvHelper;
namespace WEB_SMBIE.ClassHelper
{
    enum ParserBib
    {
        Begin,
        WaitBeginContent,
        Type,
        Tag,
        WaitTag,
        Filder,
        WaitFilder,
        WaitEq,
        ContentBeg,
        Content,
        ContentEnd,
        ErrEnd,
        End
    }
    public class CBSLink
    {
        public BSLink bSLink;
        public string Id { get { return bSLink.Id.ToString(); } }
        public string Author { get { return GetFiled("author"); } }
        public string Tag { get { return GetFiled("tag"); } }
        public string Year { get { return GetFiled("year"); } }
        public string Title { get { return GetFiled("title"); } }
        Dictionary<string, string> fields;
        public string CheckedNorm()
        {
            TypeRecord typeRecord = (from re in Helper.BDSystem.TypeRecords where re.Id == bSLink.IdRecord select re).ToList()[0];
            bSLink.TypeRecord = typeRecord;
            StringBuilder builder = new StringBuilder();
            foreach (var item in typeRecord.Relationships)
            {
                if (item.Typefielder == 1)
                    if (string.IsNullOrWhiteSpace(GetFiled(item.RecordField.Sysname)))
                    {
                        builder.Append("---");
                        builder.AppendLine(item.RecordField.Sysname);
                    }
            }
            return builder.ToString();
        }
        public CBSLink(BSLink link)
        {
            bSLink = link;
            fields = new Dictionary<string, string>();
            var list = JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(link.DataJson);
            foreach (var item in list)
            {
                fields[item.Key] = item.Value;
            }
        }
        public bool SetTypeRecord(string type)
        {
            var types = (from re in Helper.BDSystem.TypeRecords where re.Sysname == type select re).ToList();
            if (types.Count == 0)
                return false;
            bSLink.TypeRecord = types[0];
            return true;
        }
        public CBSLink()
        {
            fields = new Dictionary<string, string>();
            bSLink = new BSLink();
        }
        public string GetFiled(string field)
        {
            if (fields.ContainsKey(field))
                return fields[field];
            return "";
        }
        public void Del()
        {
            bSLink.FolderFiles = null;
            bSLink.Document = null;
            bSLink.TypeRecord = null;
        }
        public void SetFiled(string field, string value)
        {
            //проверяем что это поле есть у данного типа бс
            var list = (from re in bSLink.TypeRecord.Relationships where re.RecordField.Sysname == field select re).ToList();
            if (list != null)
            {
                bool enable = list.Count == 1;
                if (enable)
                    fields[field] = value;
            }
        }
        public void Save()
        {
            var list = fields.ToList();
            bSLink.DataJson = JsonConvert.SerializeObject(list);
        }
        public StringBuilder ToBibTex()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append('@');
            stringBuilder.Append(bSLink.TypeRecord.Sysname);
            stringBuilder.Append('{');
            stringBuilder.AppendLine(GetFiled("tag") + ",");
            SetFiled("tag", "");
            foreach (var item in fields)
            {
                if (!string.IsNullOrWhiteSpace(item.Value))
                {
                    stringBuilder.Append(item.Key);
                    stringBuilder.Append('=');
                    stringBuilder.Append('{');
                    stringBuilder.Append(item.Value);
                    stringBuilder.Append('}');
                    stringBuilder.Append(',');
                    stringBuilder.AppendLine();
                }
            }
            stringBuilder.Append('}');
            int i = stringBuilder.Length - 1;
            for (; i >= 0 && stringBuilder[i] != ','; i--) ;
            stringBuilder.Remove(i, 1);
            return stringBuilder;
        }
        public void ToCVS(CsvWriter csv, bool header = false)
        {
            if (header)
            {
                var all_fields = ClassHelper.Helper.BDSystem.RecordFields;
                csv.WriteField("Type");
                foreach (RecordField item in all_fields)
                {
                    csv.WriteField(item.Sysname);
                }
                csv.NextRecord();
            }
            csv.WriteField(bSLink.TypeRecord.Sysname);
            foreach (RecordField item in ClassHelper.Helper.BDSystem.RecordFields)
            {
                csv.WriteField(GetFiled(item.Sysname));
            }
            csv.NextRecord();
        }


        public static List<CBSLink> GetListBsLink(string bib)
        {
            List<CBSLink> bSLinks = new List<CBSLink>();
            int ind = 0;
            ParserBib status = ParserBib.Begin;
            int skob = 0;
            string type = "";
            string tag = "";
            string filder = "";
            string content = "";
            CBSLink cBSLink=null;
            while (ind < bib.Length)
            {
                
                
                    switch (status)
                    {
                        case ParserBib.Begin:
                            if (bib[ind] == '@')
                            {
                                cBSLink = new CBSLink();
                                status = ParserBib.Type;
                            }
                            break;
                        case ParserBib.Type:
                            if (char.IsLetter(bib[ind]))
                            {
                                type += bib[ind];
                            }
                            else
                            {
                                if ('{' == bib[ind])
                                {
                                    cBSLink.SetTypeRecord(type);
                                    status = ParserBib.Tag;
                                }
                                else
                                {
                                    if (' ' == bib[ind])
                                        status = ParserBib.WaitBeginContent;
                                    else
                                        status = ParserBib.ErrEnd;
                                }
                            }
                            break;
                        case ParserBib.WaitBeginContent:
                            if ('{' == bib[ind])
                            {
                                cBSLink.SetTypeRecord(type);
                                status = ParserBib.Tag;
                            }
                            break;
                        case ParserBib.Tag:
                            if (char.IsLetterOrDigit(bib[ind]))
                            {
                                tag += bib[ind];
                            }
                            else
                            {
                                if (',' == bib[ind])
                                {
                                    cBSLink.SetFiled("tag", tag);
                                    status = ParserBib.WaitFilder;
                                }
                                else
                                {
                                    if (' ' != bib[ind])
                                        status = ParserBib.ErrEnd;
                                }
                            }
                            break;
                        case ParserBib.WaitFilder:
                            if (char.IsLetter(bib[ind]))
                            {
                                filder += bib[ind];
                                status = ParserBib.Filder;
                            }
                            else
                            {
                                if (!(' ' == bib[ind] || '\n' == bib[ind] || '\r' == bib[ind] || '\t'==bib[ind]))
                                {
                                    status = ParserBib.ErrEnd;
                                }

                            }
                            break;
                        case ParserBib.Filder:
                                if (char.IsLetter(bib[ind]))
                                {
                                    filder += bib[ind];
                                }
                                else
                                {
                                    if ('=' == bib[ind])
                                    {
                                        status = ParserBib.ContentBeg;
                                    }
                                    else
                                    {
                                        if (' ' == bib[ind])
                                                status = ParserBib.WaitEq;
                                            else
                                                status = ParserBib.ErrEnd;
                                    }
                                }
                                break;
                        case ParserBib.WaitEq:
                            if ('=' == bib[ind])
                            {
                                status = ParserBib.ContentBeg;
                            }
                            else
                            {
                                if (' ' != bib[ind])
                                    status = ParserBib.ErrEnd;
                            }
                            break;
                        case ParserBib.ContentBeg:
                                if (bib[ind]=='{')
                                {
                                    status = ParserBib.Content;
                                }
                                else
                                {
                                    if (' ' != bib[ind])
                                    {
                                        status = ParserBib.ErrEnd;
                                    }
                                }
                                break;
                        case ParserBib.Content:                           
                            if ('{' == bib[ind]) skob++;
                            if ('}' == bib[ind] && skob == 0)
                            {
                                status = ParserBib.ContentEnd;
                                break;
                            }
                            else
                                content += bib[ind];
                            if ('}' == bib[ind]) skob--;
                            break;
                        case ParserBib.ContentEnd:
                            if (',' == bib[ind] || '}' == bib[ind])
                            {
                                cBSLink.SetFiled(filder, content);
                                filder = "";
                                content = "";
                            }
                            if (',' == bib[ind])
                            {
                                status = ParserBib.WaitFilder;
                            }
                            else
                            if ('}' == bib[ind])
                            {
                                status = ParserBib.End;
                            }
                            
                            break;
                        case ParserBib.End:
                            bSLinks.Add(cBSLink);
                            cBSLink.Save();
                            type = "";
                            tag = "";
                            filder = "";
                            content = "";
                            skob = 0;
                            status = ParserBib.Begin;
                            break;
                        case ParserBib.ErrEnd:
                            cBSLink.Del();
                            status = ParserBib.Begin;
                            type = "";
                            tag = "";
                            filder = "";
                            content = "";
                            skob = 0;
                            break;
                        default:
                            break;
                    }
                
                ind++;
            }
            if (status == ParserBib.End)
            {
                bSLinks.Add(cBSLink);
                cBSLink.Save();
            }
            return bSLinks;
        }
    }
}