using UnityEngine;
using System.Xml;

public class ReportLogs
{
    private string file_name;

    public ReportLogs(string out_file_name)
    {
        file_name = out_file_name;
    }

    public void GenerateLogFile(ReportController[] rc)
    {
        ErrorScriptable es;
        XmlWriter xml_writer = XmlWriter.Create(file_name);
        
        xml_writer.WriteStartDocument();
        xml_writer.WriteStartElement("simerror_results");
        xml_writer.WriteStartElement("items");
        for (int i = 0; i < rc.Length; i++)
        {
            es = rc[i].GetData();

            xml_writer.WriteStartElement("item");
            xml_writer.WriteAttributeString("id", es.Number.ToString());

            xml_writer.WriteStartElement("name");
            xml_writer.WriteString(es.Name);
            xml_writer.WriteEndElement(); // end Name

            xml_writer.WriteStartElement("wrong");
            xml_writer.WriteString(rc[i].GetErroneous().ToString());
            xml_writer.WriteEndElement(); // end Error

            xml_writer.WriteStartElement("group");
            xml_writer.WriteString(es.Group);
            xml_writer.WriteEndElement(); // end group

            xml_writer.WriteStartElement("success_level");
            int success_level = rc[i].GetSuccessLevel();
            string success_level_str;
            switch(success_level)
            {
                case 0:
                    if (rc[i].GetErroneous())
                        success_level_str = "Missed";
                    else
                        success_level_str = "Failed";
                    break;

                case 1:
                    success_level_str = "Quizz failed";
                    break;

                case 2:
                    success_level_str = "Correct";
                    break;

                default:
                    success_level_str = "Error";
                    break;
            }
            xml_writer.WriteString(success_level_str);
            xml_writer.WriteEndElement(); // end success_level

            if (rc[i].GetErroneous() == true)
            {
                int[] player_answers = rc[i].GetAnswers();
                
                xml_writer.WriteStartElement("quizz");
                for (int j = 0; j < es.Answers.Length; j++)
                {
                    xml_writer.WriteStartElement("answer");

                    xml_writer.WriteStartElement("question");
                    xml_writer.WriteString(es.Answers[j]);
                    xml_writer.WriteEndElement(); // end text

                    xml_writer.WriteStartElement("good_answer");
                    if (es.CorrectAnswers != null)
                        xml_writer.WriteString(ReportLogs.IsIntElementInTab(es.CorrectAnswers, j).ToString());
                    else
                        xml_writer.WriteString("False");
                    xml_writer.WriteEndElement(); // end good_answer

                    xml_writer.WriteStartElement("player_answer");
                    if (player_answers != null) // The program will crash otherwise
                        xml_writer.WriteString(ReportLogs.IsIntElementInTab(player_answers, j).ToString());
                    else
                        xml_writer.WriteString("False");
                    xml_writer.WriteEndElement(); // end player_answer

                    xml_writer.WriteEndElement(); // end answer
                }
                xml_writer.WriteEndElement(); // end answers
            }
            xml_writer.WriteEndElement(); // end item
        }
        xml_writer.WriteEndElement(); // end items
        xml_writer.WriteEndElement(); // end simerror_results
        xml_writer.WriteEndDocument();
        xml_writer.Close();
    }

    
    private static bool IsIntElementInTab(int[] tab, int val)
    {
        int i = 0;
        bool check = false;

        do
        {
            if (tab[i] == val)
                check = true;
            i++;
        } while ((i < tab.Length) && (check == false));

        return check;
    }

    public static string GenerateTimeStamp()
    {
        string tStamp = System.DateTime.Now.ToString();
        // Replace some characters
        tStamp = tStamp.Replace(" ", "_");
        tStamp = tStamp.Replace(":", "_");
        tStamp = tStamp.Replace("/", "_");
        return tStamp;
    }
}