using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools {
    public class CsvReader {
        public static List<List<string>> Csv2List (TextAsset binAsset) {
            List<List<string>> csvArry = new List<List<string>> ();
            string allText = binAsset.text;
            allText = allText.Replace ("\n", string.Empty);
            string[] lineText = allText.Split ("\r" [0]);
            int lineLength = lineText[0].Split (',').Length;

            for (int i = 0; i < lineText.Length; i++) {

                if (lineText[i] != string.Empty) {

                    string[] columnTest = lineText[i].Split (',');

                    if (string.Empty != columnTest[0]) {
                        List<string> lineList = new List<string> ();

                        for (int j = 0; j < lineLength; j++) {
                            if (columnTest.Length > j)
                                lineList.Add (columnTest[j]);
                            else
                                lineList.Add (string.Empty);
                        }
                        csvArry.Add (lineList);
                    }
                }
            }
            return csvArry;
        }

    }
}