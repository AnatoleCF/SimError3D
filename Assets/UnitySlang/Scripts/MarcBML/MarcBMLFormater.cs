using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Temporary MarcBML formater.
/// NOTE : Le MarcBML a des balises spécifiques. L'idéal serait de brancher sur une socket envoyant du BML 'simple' et utiliser une librairie pour le process...
/// Ca permettrait de se passer complètement du bloc MARC.
/// </summary>
public class MarcBMLFormater {
    
    public static string ProcessMarcBML(string message) {

        List<MarcFork> marcForks = RecoverMarcForks(message);
        string s = ConvertForks(marcForks);
        s = s.Replace("marc:", "");
        s = string.Concat("<bml>\n", s, "\n</bml>");

        return s;
    }

    private class MarcFork {
        public List<string> Lines;
        public float WaitDuration = 0;

        public string ToBML() {
            string bmlFork = "";
            foreach(string line in Lines) {
                if(line != "bml" && !line.StartsWith("marc:articulate"))
                    bmlFork += "\n" + LineToBML(line) + "\n";
            }
            return bmlFork;
        }

        private string LineToBML(string line) {
            string bmlLine = "";
            string[] split = line.Split(' ', '\t', '\n');

            string inTag = string.Concat('<', split[0], " ", split[1]);
            inTag = string.Concat(inTag, " start=\"", WaitDuration, "\">");

            string outTag = string.Concat("</", split[0], '>');

            bmlLine = inTag;

            for(int i = 2; i < split.Length; i++) {
                if(split[i].Contains('='))
                    bmlLine = string.Concat(bmlLine, '\n');
                bmlLine = string.Concat(bmlLine, split[i]);
            }
            bmlLine = string.Concat(bmlLine, '\n', outTag);

            split = bmlLine.Split('\n');
            string outString = split[0];

            for(int i = 1; i < split.Length - 1; i++) {
                outString += "\n" + TagLine(split[i]);
            }
            outString += "\n" + split.Last();
            return outString;
        }

        private string TagLine(string line) {
            string[] split = line.Split('=');
            if(split.Length <= 1)
                return line;
            string outString = string.Concat('<', split[0], '>');
            outString = string.Concat(outString, split[1]);
            outString = string.Concat(outString, "</", split[0], '>');
            return outString;
        }

        public MarcFork(string[] lines, float delay) {
            Lines = new List<string>(lines) ?? throw new ArgumentNullException(nameof(lines));

            string waitLine = Lines[0];
            if(waitLine.Contains("wait duration")) {
                // Remove "
                waitLine = waitLine.Split('=')[1].Replace("\"", "");
                // Replace . by ,
                waitLine = waitLine.Replace('.', ',');
                WaitDuration = float.Parse(waitLine) + delay;
                Lines.RemoveAt(0);
            } else {
                WaitDuration = delay;
            }
        }
    }

    private static List<MarcFork> RecoverMarcForks(string message, bool debug = false) {
        List<MarcFork> marcForks = new List<MarcFork>();

        string[] forkTag = new string[1] { "marc:fork" };
        string[] split = message.Split(forkTag, StringSplitOptions.RemoveEmptyEntries);
        float delay = 0;

        if(debug)
            Debug.Log("SPLIT COUNT = " + split.Length);

        for(int i = 0; i < split.Length; i++) {
            string fork = split[i].Substring(split[i].IndexOf(">") + 1);
            if(debug)
                Debug.Log("NEW FORK FOUND \n " + fork);

            string[] tmp = fork.Split(new char[3] { '<', '/', '>' }, StringSplitOptions.RemoveEmptyEntries).Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();
            if(tmp.Length == 0) {
                if(debug)
                    Debug.Log("     Ignoring empty fork.");
            } else if(tmp[0] == "marc:environment") {
                if(debug)
                    Debug.Log("     Ignoring Environment fork.");
            } else if(tmp[0] == "bml") {
                if(debug)
                    Debug.Log("     Ignoring closing bml tag.");
            } else {
                if(debug)
                    Debug.Log("     Adding new fork.");

                MarcFork marcFork = new MarcFork(tmp, delay);
                marcForks.Add(marcFork);
                delay += marcFork.WaitDuration;
            }
        }


        return marcForks;
    }

    private static string ConvertForks(List<MarcFork> forks) {

        string outString = "";

        foreach(MarcFork item in forks) {
            outString = string.Concat(outString, item.ToBML());
        }

        return outString;
    }
    
}
